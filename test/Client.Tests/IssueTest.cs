using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Velyo.Google.Services.Tests
{
    [TestClass]
    public class IssueTest
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
            var request = new GeoRequest("BH5 1DP");
            var response = request.GetResponse();
            Assert.AreEqual(GeoStatus.Ok, response.Status);
        }

        [TestMethod]
        public void Issue13038()
        {

            GeoRequest request = new GeoRequest("Yonge and Finch Toronto Canada Ontario");
            GeoResponse response = request.GetResponse();
            Assert.AreEqual(GeoStatus.Ok, response.Status);
        }

        [TestMethod]
        public void Issue11898()
        {

            GeoRequest request = new GeoRequest("4 Cassia Ct, Alice Springs, Northern Territory, 0870, Australia");
            GeoResponse response = request.GetResponse();
            Assert.AreEqual(GeoStatus.Ok, response.Status);
        }
    }
}
