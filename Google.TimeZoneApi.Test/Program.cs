using System;

namespace Google.TimeZoneApi.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            GoogleTimeZone googleTimeZone = new GoogleTimeZone("AIzaSyCNijWGcrNwwlGAl2zUUsoo_Qb2C1Sx0vk");


            DateTime dt = DateTime.Now;
            string address = "Quebec, Canada";
            GeoLocation location = new GeoLocation()
            {
                Latitude = 6.9216318,
                Longitude = 79.8212827
            };
            GeoLocation location2 = new GeoLocation()
            {
                Latitude = -19.613200,
                Longitude = 133.641453
            };


            GoogleTimeZoneResult result = googleTimeZone.GetTimeZoneByAddress(dt, address);
            //Console.WriteLine("DateTime on the server : " + dt);
            //Console.WriteLine("Server time in particular to : " + address);
            //Console.WriteLine("TimeZone Id : " + result.TimeZoneId);
            //Console.WriteLine("TimeZone Name : " + result.TimeZoneName);
            //Console.WriteLine("Converted DateTime : " + result.DateTime);

            //result = googleTimeZone.GetTimeZoneByLocation(dt, location);
            //Console.WriteLine("DateTime on the server : " + dt);
            //Console.WriteLine("Server time in particular to : " + location.Latitude + "," + location.Longitude);
            //Console.WriteLine("TimeZone Id : " + result.TimeZoneId);
            //Console.WriteLine("TimeZone Name : " + result.TimeZoneName);
            //Console.WriteLine("Converted DateTime : " + result.DateTime);

            result = googleTimeZone.GetTimeZoneByLocation(dt, location2);
            Console.WriteLine("DateTime on the server : " + dt);
            Console.WriteLine("Server time in particular to : " + location2.Latitude + "," + location2.Longitude);
            Console.WriteLine("TimeZone Id : " + result.TimeZoneId);
            Console.WriteLine("TimeZone Name : " + result.TimeZoneName);
            Console.WriteLine("Converted DateTime : " + result.DateTime);

            Console.ReadKey();
        }
    }
}
