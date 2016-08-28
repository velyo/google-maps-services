using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Velyo.Google.Services.Tests
{
    [TestClass]
    public class IssueTests
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
        public void Issue14376()
        {
            var request = new GeocodingRequest("BH5 1DP");
            var response = request.GetResponse();
            Assert.AreEqual(ResponseStatus.Ok, response.Status);
        }

        [TestMethod]
        public void Issue13038()
        {

            GeocodingRequest request = new GeocodingRequest("Yonge and Finch Toronto Canada Ontario");
            GeocodingResponse response = request.GetResponse();
            Assert.AreEqual(ResponseStatus.Ok, response.Status);
        }

        [TestMethod]
        public void Issue11898()
        {

            GeocodingRequest request = new GeocodingRequest("4 Cassia Ct, Alice Springs, Northern Territory, 0870, Australia");
            GeocodingResponse response = request.GetResponse();
            Assert.AreEqual(ResponseStatus.Ok, response.Status);
        }
    }
}
