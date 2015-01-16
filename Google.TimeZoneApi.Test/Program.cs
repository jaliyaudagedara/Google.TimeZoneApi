using System;

namespace Google.TimeZoneApi.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            GoogleTimeZone googleTimeZone = new GoogleTimeZone("AIzaSyCVGJtR2LgD5arGM_61hToUE2iNlb_VBfc");


            DateTime dt = DateTime.Now;
            //DateTime dt = new DateTime(2015, 01, 01, 08, 00, 00, DateTimeKind.Local);
            //DateTime dt = new DateTime(2015, 01, 01, 08, 00, 00, DateTimeKind.Utc);

            //string timeString = "2015-01-01T08:00:00.000+05:30";
            //DateTime dt = DateTime.Parse(timeString);

            string location = "Colombo, Sri Lanka";
            //string location = "Perth, Australia";
            //string location = "Sydney, Australia";
            //string location = "11111111111111111";

            GoogleTimeZoneResult googleTimeZoneResult = googleTimeZone.GetConvertedDateTimeBasedOnAddress(location, dt);
            Console.WriteLine("DateTime on the server : " + dt);
            Console.WriteLine("Server time in particular to : " + location);
            Console.WriteLine("TimeZone Id : " + googleTimeZoneResult.TimeZoneId);
            Console.WriteLine("TimeZone Name : " + googleTimeZoneResult.TimeZoneName);
            Console.WriteLine("Converted DateTime : " + googleTimeZoneResult.DateTime);
        }
    }
}
