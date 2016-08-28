using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Velyo.Google.Services.Tests
{
    [TestClass]
    public class GeoRequestTest
    {
        private TestContext testContextInstance;


        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        [TestMethod]
        public void GetResponse_Result()
        {
            GeoRequest request = new GeoRequest("plovdiv bulgaria");
            GeoResponse response = request.GetResponse();

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetResponse_With_Address()
        {
            GeoRequest request = new GeoRequest("plovdiv bulgaria");
            GeoResponse response = request.GetResponse();

            GeoLocation actual = response.Results[0].Geometry.Location;
            GeoLocation expected = new GeoLocation(42.1354079, 24.7452904);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_With_LatLng()
        {
            GeoRequest request = new GeoRequest(42.1354079, 24.7452904);
            GeoResponse response = request.GetResponse();

            string actual = response.Results[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_With_Location()
        {
            GeoRequest request = new GeoRequest(new GeoLocation(42.1354079, 24.7452904));
            GeoResponse response = request.GetResponse();

            string actual = response.Results[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_With_StreetAddress()
        {
            GeoRequest request = new GeoRequest("Alice Springs, Northern Territory, 0870, Australia﻿﻿﻿﻿");
            GeoResponse response = request.GetResponse();

            Assert.AreEqual(GeoStatus.Ok, response.Status);

            request.Address = "4 Cassia Ct, Alice Springs, Northern Territory, 0870, Australia";
            response = request.GetResponse();

            Assert.AreEqual(GeoStatus.Ok, response.Status);
        }

        [TestMethod]
        public void GetResponse_With_AddressAndLocation()
        {
            var request = new GeoRequest(42.1354079, 24.7452904);
            request.Address = "plovdiv bulgaria";

            try
            {
                var response = request.GetResponse();
                Assert.Fail();
            }
            catch(InvalidOperationException ex)
            {
                string expected = "Geocoding request must contain only one of 'Address' or 'Location'.";
                string actual = ex.Message;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void GetResponse_Without_AddressAndLocation()
        {
            var request = new GeoRequest();

            try
            {
                var response = request.GetResponse();
                Assert.Fail();
            }
            catch (InvalidOperationException ex)
            {
                string expected = "Geocoding request must contain at least one of 'Address' or 'Location'.";
                string actual = ex.Message;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void GetResponse_Status_OK()
        {
            GeoRequest target = new GeoRequest("plovdiv bulgaria");
            GeoResponse actual = target.GetResponse();

            GeoStatus expected = GeoStatus.Ok;

            Assert.AreEqual(expected, actual.Status);
        }
    }
}
