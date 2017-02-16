using NUnit.Framework;

namespace Velyo.Google.Services.Tests
{
    [TestFixture]
    public class IssueTests
    {
        [Test]
        public void Issue14376()
        {
            var request = new GeocodingRequest("BH5 1DP");
            var response = request.GetResponse();
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);
        }

        [Test]
        public void Issue13038()
        {
            GeocodingRequest request = new GeocodingRequest("Yonge and Finch Toronto Canada Ontario");
            GeocodingResponse response = request.GetResponse();
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);
        }

        [Test]
        public void Issue11898()
        {
            GeocodingRequest request = new GeocodingRequest("4 Cassia Ct, Alice Springs, Northern Territory, 0870, Australia");
            GeocodingResponse response = request.GetResponse();
            Assert.AreEqual(GeocodingResponseStatus.OK, response.Status);
        }
    }
}
