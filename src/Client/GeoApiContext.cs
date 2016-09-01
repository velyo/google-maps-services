using System.Configuration;
#if NET45
using System.Threading.Tasks;
#endif

namespace Velyo.Google.Services
{
    public class GeoApiContext
    {
        public static readonly GeoApiContext Default = new GeoApiContext(true);

        public string ApiKey { get; private set; }
        //public string ClientID { get; private set; }
        public bool AutoRetry { get; set; }
        public int RetryTimeout { get; set; }


        public GeoApiContext(string apiKey, bool autoRetry)
        {
            ApiKey = apiKey;
            AutoRetry = autoRetry;
        }

        public GeoApiContext(string apiKey) : this(apiKey, true) { }

        public GeoApiContext(bool autoRetry) : this(null, autoRetry) { }

        public GeoApiContext() : this(null, true) { }


        static public GeoApiContext LoadConfig()
        {
            var settings = ConfigurationManager.AppSettings;
            string apiKey = settings?["GoogleMaps.ApiKey"];
            bool autoRetry = true; 
            string autoRetryValue= settings?["GoogleMaps.AutoRetry"];

            bool.TryParse(autoRetryValue, out autoRetry);

            return new GeoApiContext(apiKey, autoRetry);
        }

        public bool Validate()
        {
            // TODO
            return true;
        }
    }
}
