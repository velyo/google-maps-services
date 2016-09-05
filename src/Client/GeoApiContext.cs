using System.Configuration;
#if NET45
using System.Threading.Tasks;
#endif

namespace Velyo.Google.Services
{
    public class GeoApiContext
    {
        static public readonly GeoApiContext Default = new GeoApiContext(true);


        public GeoApiContext(string apiKey, bool autoRetry)
        {
            ApiKey = apiKey;
            AutoRetry = autoRetry;
            RetryTimeout = 100;
            RetryTimes = 10;
        }

        public GeoApiContext(string apiKey) : this(apiKey, true) { }

        public GeoApiContext(bool autoRetry) : this(null, autoRetry) { }

        public GeoApiContext() : this(null, true) { }


        public string ApiKey { get; private set; }
        //public string ClientID { get; private set; }
        public bool AutoRetry { get; set; }
        public int RetryTimeout { get; set; }
        public int RetryTimes { get; set; }


        static public GeoApiContext LoadConfig()
        {
            var settings = ConfigurationManager.AppSettings;
            string apiKey = settings?["GoogleMaps.ApiKey"];
            bool autoRetry = true; 
            string autoRetryValue= settings?["GoogleMaps.AutoRetry"];

            bool.TryParse(autoRetryValue, out autoRetry);

            return new GeoApiContext(apiKey, autoRetry);
        }
    }
}
