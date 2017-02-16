using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
#if NET45
using System.Threading.Tasks;
#endif
using Velyo.Google.Services.Models;

namespace Velyo.Google.Services.Tests
{
    [TestFixture]
    public class MapsApiTests
    {
        private static MapsApiContext _context = MapsApiContext.Default;


        [Test]
        public void Geocode_Address()
        {
            GeocodingResponse response = MapsApi.Geocode(_context, "plovdiv bulgaria");

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            var expected = new LatLng(42.1354079, 24.7452904);
            var actual = response.Results?[0].Geometry.Location;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Geocode_Address_Many()
        {
            IEnumerable< GeocodingResponse> responses = 
                MapsApi.Geocode(_context, "sofia, bulgaria", "plovdiv bulgaria", "varna, bulgaria");

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
                Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

                LatLng actual = response.Results?[0].Geometry.Location;

                Assert.AreEqual(expected[i], actual);
            }
        }

        [Test]
        public void Geocode_Location()
        {
            GeocodingResponse response = MapsApi.Geocode(_context, new LatLng(42.1354079, 24.7452904));

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";
            string actual = response.Results?[0].FormattedAddress;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Geocode_Location_Many()
        {
            var locations = new LatLng[]
            {
                new LatLng(42.6977082, 23.3218675),
                new LatLng(42.1354079, 24.7452904),
                new LatLng(43.2140504, 27.9147333)
            };
            IEnumerable<GeocodingResponse> responses = MapsApi.Geocode(_context, locations);

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
                Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

                string actual = response.Results?[0].FormattedAddress;

                Assert.AreEqual(expected[i], actual);
            }
        }

#if NET45
        [Test]
        public async Task Geocode_Address_Async()
        {
            GeocodingResponse response = await MapsApi.GeocodeAsync(_context, "plovdiv bulgaria");

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            LatLng expected = new LatLng(42.1354079, 24.7452904);
            LatLng actual = response.Results
                .Select(x => x.Geometry)
                .Select(x => x.Location)
                .FirstOrDefault();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Geocode_Address_AsyncMany()
        {
            IEnumerable<GeocodingResponse> responses = 
                await MapsApi.GeocodeAsync(_context, "sofia, bulgaria", "plovdiv bulgaria", "varna, bulgaria");

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
                Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

                LatLng actual = response.Results
                    .Select(x => x.Geometry)
                    .Select(x => x.Location)
                    .FirstOrDefault();

                Assert.AreEqual(expected[i], actual);
            }
        }

        [Test]
        public async Task Geocode_LocationAsync()
        {
            GeocodingResponse response = await MapsApi.GeocodeAsync(_context, new LatLng(42.1354079, 24.7452904));

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";
            string actual = response.Results?[0].FormattedAddress;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Geocode_Location_ManyAsync()
        {
            var locations = new LatLng[]
            {
                new LatLng(42.6977082, 23.3218675),
                new LatLng(42.1354079, 24.7452904),
                new LatLng(43.2140504, 27.9147333)
            };
            IEnumerable<GeocodingResponse> responses = await MapsApi.GeocodeAsync(_context, locations);

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
                Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

                string actual = response.Results?[0].FormattedAddress;

                Assert.AreEqual(expected[i], actual);
            }
        }
#endif

#if NET35 || NET40
        [Test]
        public void Geocode_Address_Async()
        {
            GeocodingResponse response = MapsApi.GeocodeAsync(MapsApiContext.Default, "plovdiv bulgaria");

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            LatLng expected = new LatLng(42.1354079, 24.7452904);
            LatLng actual = response.Results
                .Select(x => x.Geometry)
                .Select(x => x.Location)
                .FirstOrDefault();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Geocode_Address_AsyncMany()
        {
            IEnumerable<GeocodingResponse> responses = MapsApi.GeocodeAsync(
                MapsApiContext.Default, "sofia, bulgaria", "plovdiv bulgaria", "varna, bulgaria");

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
                Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

                LatLng actual = response.Results
                    .Select(x => x.Geometry)
                    .Select(x => x.Location)
                    .FirstOrDefault();

                Assert.AreEqual(expected[i], actual);
            }
        }

        [Test]
        public void Geocode_LocationAsync()
        {
            GeocodingResponse response = MapsApi.GeocodeAsync(MapsApiContext.Default, new LatLng(42.1354079, 24.7452904));

            Assert.IsNotNull(response);
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            string expected = @"bul. ""Hristo Botev"" 56, 4000 Plovdiv, Bulgaria";
            string actual = response.Results?[0].FormattedAddress;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Geocode_Location_ManyAsync()
        {
            var locations = new LatLng[]
            {
                new LatLng(42.6977082, 23.3218675),
                new LatLng(42.1354079, 24.7452904),
                new LatLng(43.2140504, 27.9147333)
            };
            IEnumerable<GeocodingResponse> responses = MapsApi.GeocodeAsync(MapsApiContext.Default, locations);

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
                Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

                string actual = response.Results?[0].FormattedAddress;

                Assert.AreEqual(expected[i], actual);
            }
        }
#endif
    }
}