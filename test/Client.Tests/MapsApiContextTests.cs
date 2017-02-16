using System;
using System.Collections.Specialized;
using NUnit.Framework;

namespace Velyo.Google.Services.Tests
{
    [TestFixture]
    public class MapsApiContextTests
    {
        [Test]
        public void MapsApi_Default()
        {
            var context = MapsApiContext.Default;

            Assert.IsNotNull(context);
            Assert.IsNull(context.ApiKey);
            Assert.AreEqual(MapsApiContext.DefaultAutoRetry, context.AutoRetry);
            Assert.AreEqual(MapsApiContext.DefaultGeocodeApiUrl, context.GeocodeApiUrl);
            Assert.AreEqual(MapsApiContext.DefaultRetryDelay, context.RetryDelay);
            Assert.AreEqual(MapsApiContext.DefaultRetryTimes, context.RetryTimes);
        }

        [Test]
        public void MapsApi_Load_Settings_Null()
        {
            Assert.Throws<ArgumentNullException>(() => MapsApiContext.Load(null));
        }

        [Test]
        public void MapsApi_Load_Settings_Empty()
        {
            var context = MapsApiContext.Load(new NameValueCollection());

            Assert.IsNotNull(context);
            Assert.IsNull(context.ApiKey);
            Assert.AreEqual(MapsApiContext.DefaultAutoRetry, context.AutoRetry);
            Assert.AreEqual(MapsApiContext.DefaultGeocodeApiUrl, context.GeocodeApiUrl);
            Assert.AreEqual(MapsApiContext.DefaultRetryDelay, context.RetryDelay);
            Assert.AreEqual(MapsApiContext.DefaultRetryTimes, context.RetryTimes);
        }

        [Test]
        public void MapsApi_Load_Settings_ApiKey()
        {
            var expected = "TEST_KEY";
            var settings = new NameValueCollection();

            settings.Add("GoogleMaps.ApiKey", expected);

            var context = MapsApiContext.Load(settings);

            Assert.IsNotNull(context);
            Assert.AreEqual(expected, context.ApiKey);
            Assert.AreEqual(MapsApiContext.DefaultAutoRetry, context.AutoRetry);
            Assert.AreEqual(MapsApiContext.DefaultGeocodeApiUrl, context.GeocodeApiUrl);
            Assert.AreEqual(MapsApiContext.DefaultRetryDelay, context.RetryDelay);
            Assert.AreEqual(MapsApiContext.DefaultRetryTimes, context.RetryTimes);
        }

        [Test]
        public void MapsApi_Load_Settings_AutoRetry()
        {
            var expected = false;
            var settings = new NameValueCollection();

            settings.Add("GoogleMaps.AutoRetry", expected.ToString().ToLower());

            var context = MapsApiContext.Load(settings);

            Assert.IsNotNull(context);
            Assert.IsNull(context.ApiKey);
            Assert.AreEqual(expected, context.AutoRetry);
            Assert.AreEqual(MapsApiContext.DefaultGeocodeApiUrl, context.GeocodeApiUrl);
            Assert.AreEqual(MapsApiContext.DefaultRetryDelay, context.RetryDelay);
            Assert.AreEqual(MapsApiContext.DefaultRetryTimes, context.RetryTimes);
        }

        [Test]
        public void MapsApi_Load_Settings_GeocodeApiUrl()
        {
            var expected = "TEST_URL";
            var settings = new NameValueCollection();

            settings.Add("GoogleMaps.GeocodeApiUrl", expected);

            var context = MapsApiContext.Load(settings);

            Assert.IsNotNull(context);
            Assert.IsNull(context.ApiKey);
            Assert.AreEqual(MapsApiContext.DefaultAutoRetry, context.AutoRetry);
            Assert.AreEqual(expected, context.GeocodeApiUrl);
            Assert.AreEqual(MapsApiContext.DefaultRetryDelay, context.RetryDelay);
            Assert.AreEqual(MapsApiContext.DefaultRetryTimes, context.RetryTimes);
        }

        [Test]
        public void MapsApi_Load_Settings_RetryDelay()
        {
            var expected = 300;
            var settings = new NameValueCollection();

            settings.Add("GoogleMaps.RetryDelay", expected.ToString());

            var context = MapsApiContext.Load(settings);

            Assert.IsNotNull(context);
            Assert.IsNull(context.ApiKey);
            Assert.AreEqual(MapsApiContext.DefaultAutoRetry, context.AutoRetry);
            Assert.AreEqual(MapsApiContext.DefaultGeocodeApiUrl, context.GeocodeApiUrl);
            Assert.AreEqual(expected, context.RetryDelay);
            Assert.AreEqual(MapsApiContext.DefaultRetryTimes, context.RetryTimes);
        }

        [Test]
        public void MapsApi_Load_Settings_RetryTimes()
        {
            var expected = 10;
            var settings = new NameValueCollection();

            settings.Add("GoogleMaps.RetryTimes", expected.ToString());

            var context = MapsApiContext.Load(settings);

            Assert.IsNotNull(context);
            Assert.IsNull(context.ApiKey);
            Assert.AreEqual(MapsApiContext.DefaultAutoRetry, context.AutoRetry);
            Assert.AreEqual(MapsApiContext.DefaultGeocodeApiUrl, context.GeocodeApiUrl);
            Assert.AreEqual(MapsApiContext.DefaultRetryDelay, context.RetryDelay);
            Assert.AreEqual(expected, context.RetryTimes);
        }
    }
}
