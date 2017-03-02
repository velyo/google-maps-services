using NUnit.Framework;

namespace Velyo.Google.Services.Tests
{
    [TestFixture]
    public class IssueTests
    {
        [Test]
        public void Issues_GitHub_Issue10()
        {
            GeocodingRequest request = new GeocodingRequest("1 Microsoft Way, Redmond, CA");
            GeocodingResponse response = request.GetResponse();

            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);

            foreach (var r in response.Results)
            {
                Assert.NotNull(r.Geometry); 
                Assert.NotNull(r.Geometry.Location);
                Assert.NotNull(r.FormattedAddress);

                Assert.AreEqual(47.6393225, r.Geometry.Location.Latitude);
                Assert.AreEqual(-122.1283833, r.Geometry.Location.Longitude);

                foreach (var ac in r.AddressComponents)
                {
                    Assert.NotNull(ac.ShortName);
                    Assert.NotNull(ac.LongName);
                }
            }
        }

        [Test]
        public void Issues_Issue14376()
        {
            var request = new GeocodingRequest("BH5 1DP");
            var response = request.GetResponse();
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);
        }

        [Test]
        public void Issues_Issue13038()
        {
            GeocodingRequest request = new GeocodingRequest("Yonge and Finch Toronto Canada Ontario");
            GeocodingResponse response = request.GetResponse();
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);
        }

        [Test]
        public void Issues_Issue11898()
        {
            GeocodingRequest request = new GeocodingRequest("4 Cassia Ct, Alice Springs, Northern Territory, 0870, Australia");
            GeocodingResponse response = request.GetResponse();
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);
        }
    }
}
