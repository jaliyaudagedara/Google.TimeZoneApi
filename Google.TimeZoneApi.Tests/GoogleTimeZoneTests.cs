using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Google.TimeZoneApi.Tests
{
    [TestClass]
    public class GoogleTimeZoneTests
    {
        [TestMethod]
        public void TestGetTimeZoneByAddress()
        {
            GoogleTimeZone googleTimeZone = new GoogleTimeZone("AIzaSyCVGJtR2LgD5arGM_61hToUE2iNlb_VBfc");

            DateTime dt = DateTime.Now;

            string address = "Quebec, Canada";

            GoogleTimeZoneResult result = googleTimeZone.GetTimeZoneByAddress(dt, address);

            Assert.IsTrue(result != null);

            Console.WriteLine("DateTime on the server : " + dt);
            Console.WriteLine("Server time in particular to : " + address);
            Console.WriteLine("TimeZone Id : " + result.TimeZoneId);
            Console.WriteLine("TimeZone Name : " + result.TimeZoneName);
            Console.WriteLine("Converted DateTime : " + result.DateTime);
        }
        [TestMethod]
        public void TestGetTimeZoneByLocation()
        {
            GoogleTimeZone googleTimeZone = new GoogleTimeZone("AIzaSyCVGJtR2LgD5arGM_61hToUE2iNlb_VBfc");

            DateTime dt = DateTime.Now;

            GeoLocation location = new GeoLocation();
            location.Latitude = 6.9216318;
            location.Longitude = 79.8212827;

            GoogleTimeZoneResult result = googleTimeZone.GetTimeZoneByLocation(dt, location);

            Assert.IsTrue(result != null);
            Console.WriteLine("DateTime on the server : " + dt);
            Console.WriteLine("Server time in particular to : " + location.Latitude + "," + location.Longitude );
            Console.WriteLine("TimeZone Id : " + result.TimeZoneId);
            Console.WriteLine("TimeZone Name : " + result.TimeZoneName);
            Console.WriteLine("Converted DateTime : " + result.DateTime);
        }
    }
}
