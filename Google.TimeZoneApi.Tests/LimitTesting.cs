using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Google.TimeZoneApi.Tests
{
    [TestClass]
    public class LimitTesting
    {
        [TestMethod]
        public void TestUpperLongitudeLimit()
        {
            GoogleTimeZone googleTimeZone = new GoogleTimeZone("AIzaSyCVGJtR2LgD5arGM_61hToUE2iNlb_VBfc");
            DateTime dt = DateTime.Now;
            GeoLocation location = new GeoLocation() //Fiji Island
            {
                Latitude = -17.847737, 
                Longitude = 177.904347
            };
            GoogleTimeZoneResult result = googleTimeZone.GetTimeZoneByLocation(dt, location);
            Assert.IsTrue(result != null, "Request Succeded with longitude of 180");

            location.Longitude = 179; //Somewhere in the ocean
            try
            {
                result = null;
                result = googleTimeZone.GetTimeZoneByLocation(dt, location);
            }
            catch(Exception e)
            {
                Assert.IsTrue(result == null,"Getting no result");
            }         

             location.Longitude = 181; //Out of scope
            try
            {
                result = null;
                result = googleTimeZone.GetTimeZoneByLocation(dt, location);
            }
            catch(Exception e)
            {
                Assert.IsTrue(result == null,"Bad Exception errors");
            }         
        }
    }
}
