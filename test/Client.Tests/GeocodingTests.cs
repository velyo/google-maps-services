using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Velyo.Google.Services.Models;
#if NET45
using System.Threading.Tasks;
#endif

namespace Velyo.Google.Services.Tests
{
    [TestClass]
    public class GeoRequestTest
    {
        private static MapsApiContext _context = MapsApiContext.Default;


        [TestMethod]
        public void GetResponse_Result()
        {
            GeocodingRequest request = new GeocodingRequest("plovdiv bulgaria", _context);
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);
        }

#if NET45
        [TestMethod]
        public async Task GetResponse_Result_Async()
        {
            GeocodingRequest request = new GeocodingRequest("plovdiv bulgaria", _context);
            GeocodingResponse response = await request.GetResponseAsync();

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);
        }
#endif

#if NET35 || NET40
        [TestMethod]
        public void GetResponse_Result_Async()
        {
            GeocodingRequest request = new GeocodingRequest("plovdiv bulgaria");
            GeocodingResponse response = request.GetResponseAsync();

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);
        }
#endif

        [TestMethod]
        public void GetResponse_Result_ByAddress()
        {
            GeocodingRequest request = new GeocodingRequest("plovdiv bulgaria", _context);
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            LatLng actual = response.Results?[0].Geometry.Location;
            LatLng expected = new LatLng(42.1354079, 24.7452904);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_Result_ByLocation()
        {
            GeocodingRequest request = new GeocodingRequest(new LatLng(42.1354079, 24.7452904), _context);
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            string actual = response.Results[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_Result_ByLocationPair()
        {
            GeocodingRequest request = new GeocodingRequest(42.1354079, 24.7452904, _context);
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            string actual = response.Results?[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_Result_ByStreetAddress()
        {
            GeocodingRequest request = new GeocodingRequest("Alice Springs, Northern Territory, 0870, Australia﻿﻿﻿﻿", _context);
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            request.Address = "4 Cassia Ct, Alice Springs, Northern Territory, 0870, Australia";
            response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);
        }

        [TestMethod]
        public void GetResponse_With_IsSensor()
        {
            GeocodingRequest request = new GeocodingRequest(42.1354079, 24.7452904, _context) { IsSensor = true };
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            string actual = response.Results?[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_With_Language()
        {
            GeocodingRequest request = new GeocodingRequest(42.1354079, 24.7452904, _context) { Language = "bg-BG" };
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            string actual = response.Results?[0].FormattedAddress;
            string expected = "бул. „Христо Ботев“ 56, 4000 Пловдив, България";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetResponse_With_Region()
        {
            GeocodingRequest request = new GeocodingRequest(42.1354079, 24.7452904, _context) { Region = "bg" };
            GeocodingResponse response = request.GetResponse();

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            string actual = response.Results?[0].FormattedAddress;
            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetResponse_Fail_WithAddressAndLatLng()
        {
            var request = new GeocodingRequest(42.1354079, 24.7452904, _context);
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
    }
}
