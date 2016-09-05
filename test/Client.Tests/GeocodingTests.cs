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
        [TestMethod]
        public void GetResponse_Create()
        {
            GeocodingRequest request = GeocodingRequest.Create("plovdiv bulgaria");
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void GetResponse_CreateReverse()
        {
            GeocodingRequest request = GeocodingRequest.Create(new LatLng(42.1354079, 24.7452904));
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void GetResponse_CreateReversePair()
        {
            GeocodingRequest request = GeocodingRequest.Create(42.1354079, 24.7452904);
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void GetResponse_Result()
        {
            GeocodingRequest request = new GeocodingRequest("plovdiv bulgaria");
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);
        }

#if NET45
        [TestMethod]
        public async Task GetResponse_Result_Async()
        {
            GeocodingRequest request = new GeocodingRequest("plovdiv bulgaria");
            GeocodingResponse response = await request.GetResponseAsync();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);
        }
#endif

#if NET35 || NET40
        [TestMethod]
        public void GetResponse_Result_Async()
        {
            GeocodingRequest request = new GeocodingRequest("plovdiv bulgaria");
            GeocodingResponse response = request.GetResponseAsync();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);
        }
#endif

        [TestMethod]
        public void GetResponse_Result_ByAddress()
        {
            GeocodingRequest request = new GeocodingRequest("plovdiv bulgaria");
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            LatLng actual = response.Results?[0].Geometry.Location;
            LatLng expected = new LatLng(42.1354079, 24.7452904);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_Result_ByLocation()
        {
            GeocodingRequest request = new GeocodingRequest(new LatLng(42.1354079, 24.7452904));
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            string actual = response.Results[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_Result_ByLocationPair()
        {
            GeocodingRequest request = new GeocodingRequest(42.1354079, 24.7452904);
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            string actual = response.Results?[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_Result_ByStreetAddress()
        {
            GeocodingRequest request = new GeocodingRequest("Alice Springs, Northern Territory, 0870, Australia﻿﻿﻿﻿");
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            request.Address = "4 Cassia Ct, Alice Springs, Northern Territory, 0870, Australia";
            response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);
        }

        [TestMethod]
        public void GetResponse_With_IsSensor()
        {
            GeocodingRequest request = new GeocodingRequest(42.1354079, 24.7452904) { IsSensor = true };
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            string actual = response.Results?[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_With_Language()
        {
            GeocodingRequest request = new GeocodingRequest(42.1354079, 24.7452904) { Language = "bg-BG" };
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            string actual = response.Results?[0].FormattedAddress;
            string expected = "бул. „Христо Ботев“ 56, 4000 Пловдив, България";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_With_Region()
        {
            GeocodingRequest request = new GeocodingRequest(42.1354079, 24.7452904) { Region = "bg" };
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            string actual = response.Results?[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetResponse_Fail_WithAddressAndLatLng()
        {
            var request = new GeocodingRequest(42.1354079, 24.7452904);
            request.Address = "plovdiv bulgaria";
            
            var response = request.GetResponse();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetResponse_Fail_WithoutAddressAndLatLng()
        {
            var request = new GeocodingRequest();
            var response = request.GetResponse();
        }

        [TestInitialize]
        public void BeforeTest()
        {
            Thread.Sleep(200);
        }
    }
}
