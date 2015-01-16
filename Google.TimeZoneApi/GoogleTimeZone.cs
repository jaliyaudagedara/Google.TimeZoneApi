using System;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;

namespace Google.TimeZoneApi
{
    public class GoogleTimeZone
    {
        private string apiKey;
        private GeoLocation location;
        private string previousAddress = string.Empty;

        public GoogleTimeZone(string apiKey)
        {
            this.apiKey = apiKey;
        }

        /// <summary>
        /// Gets the converted date time based on address.
        /// </summary>
        /// <param name="address">The destination address.</param>
        /// <param name="dateTime">The date and time to convert.</param>
        /// <returns>A <see cref="GoogleTimeZoneResult"/>.</returns>
        public GoogleTimeZoneResult GetConvertedDateTimeBasedOnAddress(string address, DateTime dateTime)
        {
            long timestamp = GetUnixTimeStampFromDateTime(TimeZoneInfo.ConvertTimeToUtc(dateTime));

            if (previousAddress != address)
            {
                this.location = GetCoordinatesByLocationName(address);

                previousAddress = address;

                if (this.location == null)
                {
                    return null;
                }
            }

            return GetConvertedDateTimeBasedOnAddress(this.location, timestamp);
        }

        private GoogleTimeZoneResult GetConvertedDateTimeBasedOnAddress(GeoLocation location, long timestamp)
        {
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/timezone/xml?location={0},{1}&timestamp={2}&key={3}", location.Latitude, location.Longitude, timestamp, this.apiKey);

            XDocument xdoc = GetXmlResponse(requestUri);

            XElement result = xdoc.Element("TimeZoneResponse");
            XElement rawOffset = result.Element("raw_offset");
            XElement dstOfset = result.Element("dst_offset");
            XElement timeZoneId = result.Element("time_zone_id");
            XElement timeZoneName = result.Element("time_zone_name");

            return new GoogleTimeZoneResult()
            {
                DateTime = GetDateTimeFromUnixTimeStamp(Convert.ToDouble(timestamp) + Convert.ToDouble(rawOffset.Value) + Convert.ToDouble(dstOfset.Value)),
                TimeZoneId = timeZoneId.Value,
                TimeZoneName = timeZoneName.Value
            };
        }

        private GeoLocation GetCoordinatesByLocationName(string address)
        {
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&key={1}", Uri.EscapeDataString(address), this.apiKey);

            XDocument xdoc = GetXmlResponse(requestUri);

            XElement status = xdoc.Element("GeocodeResponse").Element("status");

            if (status.Value == "OK")
            {
                XElement result = xdoc.Element("GeocodeResponse").Element("result");
                XElement locationElement = result.Element("geometry").Element("location");
                XElement lat = locationElement.Element("lat");
                XElement lng = locationElement.Element("lng");

                return new GeoLocation() { Latitude = Convert.ToDouble(lat.Value), Longitude = Convert.ToDouble(lng.Value) };
            }
            else if (status.Value == "ZERO_RESULTS")
            {
                Debug.Write(string.Format("No location found matching the address : {0}. Skipping the time zone lookup.", address));
            }
            else if (status.Value == "REQUEST_DENIED")
            {
                XElement errorMessage = xdoc.Element("GeocodeResponse").Element("error_message");
                Debug.Write(errorMessage.Value);
            }
            return null;
        }

        private XDocument GetXmlResponse(string requestUri)
        {
            try
            {
                WebRequest request = WebRequest.Create(requestUri);
                WebResponse response = request.GetResponse();
                return XDocument.Load(response.GetResponseStream());
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private long GetUnixTimeStampFromDateTime(DateTime dt)
        {
            DateTime epochDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan ts = dt - epochDate;
            return (int)ts.TotalSeconds;
        }

        private DateTime GetDateTimeFromUnixTimeStamp(double unixTimeStamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(unixTimeStamp);
            return dt;
        }
    }
}
