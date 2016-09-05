using System.Collections.Generic;
using System.Linq;
using System.Threading;
#if NET45
using System.Threading.Tasks;
#endif
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Velyo.Google.Services.Tests
{
    [TestClass]
    public class GeocodingApiTests
    {
        [TestMethod]
        public void Create_Request_ByAddress()
        {
            GeocodingRequest request = GeocodingApi.CreateRequest(GeoApiContext.Default, "plovdiv bulgaria");

            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void Create_Request_ByLocation()
        {
            GeocodingRequest request = GeocodingApi.CreateRequest(GeoApiContext.Default, "plovdiv bulgaria");

            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void Geocode_Address()
        {
            GeocodingResponse response = GeocodingApi.Geocode(GeoApiContext.Default, "plovdiv bulgaria");

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            LatLng expected = new LatLng(42.1354079, 24.7452904);
            LatLng actual = response.Results?[0].Geometry.Location;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Geocode_Address_Many()
        {
            IEnumerable<GeocodingResponse> responses = GeocodingApi.Geocode(
                GeoApiContext.Default, "sofia, bulgaria", "plovdiv bulgaria", "varna, bulgaria");

            Assert.IsNotNull(responses);
            Assert.AreEqual(3, responses.Count());

            var expected = new LatLng[]
            {
                new LatLng(42.6977082, 23.3218675),
                new LatLng(42.1354079, 24.7452904),
                new LatLng(43.2140504, 27.9147333)
            };

            for (int i = 0; i < 3; i++)
            {
                GeocodingResponse response = responses.Skip(i).Take(1).FirstOrDefault();
                Assert.AreEqual(ResponseStatus.OK, response.Status);

                LatLng actual = response.Results?[0].Geometry.Location;

                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public void Geocode_Location()
        {
            GeocodingResponse response = GeocodingApi.Geocode(GeoApiContext.Default, new LatLng(42.1354079, 24.7452904));

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";
            string actual = response.Results?[0].FormattedAddress;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Geocode_Location_Many()
        {
            var locations = new LatLng[]
            {
                new LatLng(42.6977082, 23.3218675),
                new LatLng(42.1354079, 24.7452904),
                new LatLng(43.2140504, 27.9147333)
            };
            IEnumerable<GeocodingResponse> responses = GeocodingApi.Geocode(GeoApiContext.Default, locations);

            Assert.IsNotNull(responses);
            Assert.AreEqual(3, responses.Count());

            var expected = new string[]
            {
                @"pl. ""Nezavisimost"", 1000 Sofia, Bulgaria",
                @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria",
                @"bul. ""Tsar Osvoboditel"" 83, 9000 Varna, Bulgaria"
            };

            for (int i = 0; i < 3; i++)
            {
                GeocodingResponse response = responses.Skip(i).Take(1).FirstOrDefault();
                Assert.AreEqual(ResponseStatus.OK, response.Status);

                string actual = response.Results?[0].FormattedAddress;

                Assert.AreEqual(expected[i], actual);
            }
        }

#if NET45
        [TestMethod]
        public async Task Geocode_Address_Async()
        {
            GeocodingRequest request = GeocodingApi.CreateRequest(GeoApiContext.Default, "plovdiv bulgaria");

            Assert.IsNotNull(request);

            GeocodingResponse response = await request.GetResponseAsync();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            LatLng expected = new LatLng(42.1354079, 24.7452904);
            LatLng actual = response.Results
                .Select(x => x.Geometry)
                .Select(x => x.Location)
                .FirstOrDefault();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Geocode_Address_AsyncMany()
        {
            IEnumerable<GeocodingResponse> responses = await GeocodingApi.GeocodeAsync(
                GeoApiContext.Default, "sofia, bulgaria", "plovdiv bulgaria", "varna, bulgaria");

            Assert.IsNotNull(responses);
            Assert.AreEqual(3, responses.Count());

            var expected = new LatLng[]
            {
                new LatLng(42.6977082, 23.3218675),
                new LatLng(42.1354079, 24.7452904),
                new LatLng(43.2140504, 27.9147333)
            };

            for (int i = 0; i < 3; i++)
            {
                GeocodingResponse response = responses.Skip(i).Take(1).FirstOrDefault();
                Assert.AreEqual(ResponseStatus.OK, response.Status);

                LatLng actual = response.Results
                    .Select(x => x.Geometry)
                    .Select(x => x.Location)
                    .FirstOrDefault();

                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public async Task Geocode_LocationAsync()
        {
            GeocodingResponse response = await GeocodingApi.GeocodeAsync(GeoApiContext.Default, new LatLng(42.1354079, 24.7452904));

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";
            string actual = response.Results?[0].FormattedAddress;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Geocode_Location_ManyAsync()
        {
            var locations = new LatLng[]
            {
                new LatLng(42.6977082, 23.3218675),
                new LatLng(42.1354079, 24.7452904),
                new LatLng(43.2140504, 27.9147333)
            };
            IEnumerable<GeocodingResponse> responses = await GeocodingApi.GeocodeAsync(GeoApiContext.Default, locations);

            Assert.IsNotNull(responses);
            Assert.AreEqual(3, responses.Count());

            var expected = new string[]
            {
                @"pl. ""Nezavisimost"", 1000 Sofia, Bulgaria",
                @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria",
                @"bul. ""Tsar Osvoboditel"" 83, 9000 Varna, Bulgaria"
            };

            for (int i = 0; i < 3; i++)
            {
                GeocodingResponse response = responses.Skip(i).Take(1).FirstOrDefault();
                Assert.AreEqual(ResponseStatus.OK, response.Status);

                string actual = response.Results?[0].FormattedAddress;

                Assert.AreEqual(expected[i], actual);
            }
        }
#endif

#if NET35 || NET40
        [TestMethod]
        public void Geocode_Address_Async()
        {
            GeocodingRequest request = GeocodingApi.CreateRequest(GeoApiContext.Default, "plovdiv bulgaria");

            Assert.IsNotNull(request);

            GeocodingResponse response = request.GetResponseAsync();

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            LatLng expected = new LatLng(42.1354079, 24.7452904);
            LatLng actual = response.Results
                .Select(x => x.Geometry)
                .Select(x => x.Location)
                .FirstOrDefault();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Geocode_Address_AsyncMany()
        {
            IEnumerable<GeocodingResponse> responses = GeocodingApi.GeocodeAsync(
                GeoApiContext.Default, "sofia, bulgaria", "plovdiv bulgaria", "varna, bulgaria");

            Assert.IsNotNull(responses);
            Assert.AreEqual(3, responses.Count());

            var expected = new LatLng[]
            {
                new LatLng(42.6977082, 23.3218675),
                new LatLng(42.1354079, 24.7452904),
                new LatLng(43.2140504, 27.9147333)
            };

            for (int i = 0; i < 3; i++)
            {
                GeocodingResponse response = responses.Skip(i).Take(1).FirstOrDefault();
                Assert.AreEqual(ResponseStatus.OK, response.Status);

                LatLng actual = response.Results
                    .Select(x => x.Geometry)
                    .Select(x => x.Location)
                    .FirstOrDefault();

                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public void Geocode_LocationAsync()
        {
            GeocodingResponse response = GeocodingApi.GeocodeAsync(GeoApiContext.Default, new LatLng(42.1354079, 24.7452904));

            Assert.IsNotNull(response);
            Assert.AreEqual(ResponseStatus.OK, response.Status);

            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";
            string actual = response.Results?[0].FormattedAddress;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Geocode_Location_ManyAsync()
        {
            var locations = new LatLng[]
            {
                new LatLng(42.6977082, 23.3218675),
                new LatLng(42.1354079, 24.7452904),
                new LatLng(43.2140504, 27.9147333)
            };
            IEnumerable<GeocodingResponse> responses = GeocodingApi.GeocodeAsync(GeoApiContext.Default, locations);

            Assert.IsNotNull(responses);
            Assert.AreEqual(3, responses.Count());

            var expected = new string[]
            {
                @"pl. ""Nezavisimost"", 1000 Sofia, Bulgaria",
                @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria",
                @"bul. ""Tsar Osvoboditel"" 83, 9000 Varna, Bulgaria"
            };

            for (int i = 0; i < 3; i++)
            {
                GeocodingResponse response = responses.Skip(i).Take(1).FirstOrDefault();
                Assert.AreEqual(ResponseStatus.OK, response.Status);

                string actual = response.Results?[0].FormattedAddress;

                Assert.AreEqual(expected[i], actual);
            }
        }
#endif

        [TestInitialize]
        private void BeforeTest()
        {
            Thread.Sleep(200);
        }
    }
}