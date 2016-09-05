using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Velyo.Google.Services.Tests
{
    [TestClass]
    public class IssueTests
    {
        [TestMethod]
        public void Issue14376()
        {
            var request = new GeocodingRequest("BH5 1DP");
            var response = request.GetResponse();
            Assert.AreEqual(ResponseStatus.OK, response.Status);
        }

        [TestMethod]
        public void Issue13038()
        {
            GeocodingRequest request = new GeocodingRequest("Yonge and Finch Toronto Canada Ontario");
            GeocodingResponse response = request.GetResponse();
            Assert.AreEqual(ResponseStatus.OK, response.Status);
        }

        [TestMethod]
        public void Issue11898()
        {
            GeocodingRequest request = new GeocodingRequest("4 Cassia Ct, Alice Springs, Northern Territory, 0870, Australia");
            GeocodingResponse response = request.GetResponse();
            Assert.AreEqual(ResponseStatus.OK, response.Status);
        }

        [TestInitialize]
        public void BeforeTest()
        {
           Thread.Sleep(200);
        }
    }
}
