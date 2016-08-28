using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
#if NET45
using System.Threading.Tasks;
#endif

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
        public void GetResponse_Create()
        {
            GeocodingRequest request = GeocodingRequest.Create("plovdiv bulgaria");
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void GetResponse_CreateReverse()
        {
            GeocodingRequest request = GeocodingRequest.Create(42.1354079, 24.7452904);
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void GetResponse_CreateReverseInstance()
        {
            GeocodingRequest request = GeocodingRequest.Create(new LatLng(42.1354079, 24.7452904));
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void GetResponse_Result()
        {
            GeocodingRequest request = new GeocodingRequest("plovdiv bulgaria");
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);

            LatLng actual = response.Results[0].Geometry.Location;
            LatLng expected = new LatLng(42.1354079, 24.7452904);

            Assert.AreEqual(expected, actual);
        }

#if NET35 || NET40
        [TestMethod]
        public void GetResponse_ResultAsync()
        {
            GeocodingRequest request = new GeocodingRequest("plovdiv bulgaria");
            GeocodingResponse response = request.GetResponseAsync();

            Assert.IsNotNull(response);

            LatLng actual = response.Results[0].Geometry.Location;
            LatLng expected = new LatLng(42.1354079, 24.7452904);

            Assert.AreEqual(expected, actual);
        }
#endif

#if NET45
        [TestMethod]
        public async Task GetResponse_ResultAsync()
        {
            GeocodingRequest request = new GeocodingRequest("plovdiv bulgaria");
            GeocodingResponse response = await request.GetResponseAsync();

            Assert.IsNotNull(response);

            LatLng actual = response.Results[0].Geometry.Location;
            LatLng expected = new LatLng(42.1354079, 24.7452904);

            Assert.AreEqual(expected, actual);
        }
#endif

        [TestMethod]
        public void GetResponse_With_Address()
        {
            GeocodingRequest request = new GeocodingRequest("plovdiv bulgaria");
            GeocodingResponse response = request.GetResponse();

            LatLng actual = response.Results[0].Geometry.Location;
            LatLng expected = new LatLng(42.1354079, 24.7452904);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_With_LatLng()
        {
            GeocodingRequest request = new GeocodingRequest(42.1354079, 24.7452904);
            GeocodingResponse response = request.GetResponse();

            string actual = response.Results[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_With_LatLngInstance()
        {
            GeocodingRequest request = new GeocodingRequest(new LatLng(42.1354079, 24.7452904));
            GeocodingResponse response = request.GetResponse();

            string actual = response.Results[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_With_StreetAddress()
        {
            GeocodingRequest request = new GeocodingRequest("Alice Springs, Northern Territory, 0870, Australia﻿﻿﻿﻿");
            GeocodingResponse response = request.GetResponse();

            Assert.AreEqual(ResponseStatus.Ok, response.Status);

            Thread.Sleep(100);

            request.Address = "4 Cassia Ct, Alice Springs, Northern Territory, 0870, Australia";
            response = request.GetResponse();

            Assert.AreEqual(ResponseStatus.Ok, response.Status);
        }

        [TestMethod]
        public void GetResponse_With_AddressAndLatLng()
        {
            var request = new GeocodingRequest(42.1354079, 24.7452904);
            request.Address = "plovdiv bulgaria";

            try
            {
                var response = request.GetResponse();
                Assert.Fail();
            }
            catch (InvalidOperationException ex)
            {
                string expected = "Geocoding request must contain only one of 'Address' or 'Location'.";
                string actual = ex.Message;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void GetResponse_Without_AddressAndLatLng()
        {
            var request = new GeocodingRequest();

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
        public void GetResponse_With_IsSensor()
        {
            GeocodingRequest request = new GeocodingRequest(42.1354079, 24.7452904) { IsSensor = true };
            GeocodingResponse response = request.GetResponse();

            string actual = response.Results[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_With_Language()
        {
            GeocodingRequest request = new GeocodingRequest(42.1354079, 24.7452904) { Language = "bg-BG" };
            GeocodingResponse response = request.GetResponse();

            string actual = response.Results[0].FormattedAddress;
            string expected = "бул. „Христо Ботев“ 56, 4000 Пловдив, България";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_With_Region()
        {
            GeocodingRequest request = new GeocodingRequest(42.1354079, 24.7452904) { Region = "bg" };
            GeocodingResponse response = request.GetResponse();

            string actual = response.Results[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_Status_OK()
        {
            GeocodingRequest target = new GeocodingRequest("plovdiv bulgaria");
            GeocodingResponse actual = target.GetResponse();

            ResponseStatus expected = ResponseStatus.Ok;

            Assert.AreEqual(expected, actual.Status);
        }

        [TestInitialize]
        private void BeforeTest()
        {
            // bypass free requests limit
            Thread.Sleep(100);
        }
    }
}
