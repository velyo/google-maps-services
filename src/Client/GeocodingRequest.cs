using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
#if NET45
using System.Threading.Tasks;
#endif
using System.Web;
using System.Web.Script.Serialization;

namespace Velyo.Google.Services
{
    public class GeocodingRequest
    {
        static readonly string RequestUrl = "http://maps.google.com/maps/api/geocode/json?";

#if NET35 || NET40
        private WebResponse _asyncResponse;
        private ManualResetEvent _asyncTrigger;
#endif


        /// <summary>
        /// Initializes a new instance of the <see cref="GeocodingRequest"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        public GeocodingRequest(string address) : this()
        {
            Address = address;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeocodingRequest"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        public GeocodingRequest(LatLng location) : this()
        {
            Location = location;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeocodingRequest"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public GeocodingRequest(double latitude, double longitude) : this()
        {
            Location = new LatLng(latitude, longitude);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeocodingRequest"/> class.
        /// </summary>
        public GeocodingRequest()
        {
            AutoRetry = true;
        }


        /// <summary>
        /// The address that you want to geocode.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets a value indicates whether or not the geocoding request comes from a device with a location sensor.
        /// </summary>
        /// <value><c>true</c> if this instance is sensor; otherwise, <c>false</c>.</value>
        public bool IsSensor { get; set; }

        /// <summary>
        /// The language in which to return results. See the supported list of domain languages. 
        /// Note that we often update supported languages so this list may not be exhaustive. 
        /// If language is not supplied, the geocoder will attempt to use the native language of the domain 
        /// from which the request is sent wherever possible. 
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        /// <summary>
        /// The latitude/longitude value for which you wish to obtain the closest, human-readable address.
        /// </summary>
        /// <value>The location.</value>
        public LatLng Location { get; set; }

        /// <summary>
        /// The region code, specified as a ccTLD ("top-level domain") two-character value.
        /// </summary>
        /// <value>The region.</value>
        public string Region { get; set; }

        /// <summary>
        /// Retry request on query limit reached. It's on by default.
        /// </summary>
        public bool AutoRetry { get; set; }


        /// <summary>
        /// Creates <code>GeocodingRequest</code> for the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        static public GeocodingRequest Create(string address)
        {
            return new GeocodingRequest(address);
        }

        /// <summary>
        /// Creates reverse <code>GeocodingRequest</code> for the specified latitude and longitude.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns></returns>
        static public GeocodingRequest Create(double latitude, double longitude)
        {
            return new GeocodingRequest(latitude, longitude);
        }

        /// <summary>
        /// Creates reverse <code>GeocodingRequest</code> for the specified latitude and longitude.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        static public GeocodingRequest Create(LatLng location)
        {
            return new GeocodingRequest(location);
        }

        /// <summary>
        /// Executes <code>GeocodingRequest</code> against Google Maps Geocoding API web service 
        /// and returns the result as <seealso cref="GeocodingResponse"/>.
        /// </summary>
        /// <returns>the result GeocodingResponse</returns>
        public GeocodingResponse GetResponse()
        {
            Validate();

            string url = BuildRequestUrl();
            GeocodingJsonData jsonData = null;

            for (int n = 1; n <= 3; n++)
            {
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                {
                    jsonData = GetData(response);
                }

                if (AutoRetry && (jsonData.status == ResponseStatus.OVER_QUERY_LIMIT))
                {
#if DEBUG
                    Debug.WriteLine("[GetResponse]: Auto retry #" + n);
#endif
                    Thread.Sleep(n * 100);
                    continue;
                }
                break;
            }

            return new GeocodingResponse(jsonData);
        }

#if NET35 || NET40

        /// <summary>
        /// Executes <code>GeocodingRequest</code> against Google Maps Geocoding API web service 
        /// and returns the result as <seealso cref="GeocodingResponse"/>, in asynchronous fashion.
        /// </summary>
        /// <returns></returns>
        public GeocodingResponse GetResponseAsync()
        {
            Validate();

            string url = BuildRequestUrl();
            GeocodingJsonData jsonData = null;

            for (int n = 1; n <= 3; n++)
            {
                var request = WebRequest.Create(url);

                using (_asyncTrigger = new ManualResetEvent(false))
                {
#if DEBUG
                    Debug.WriteLine("[GetResponseAsync]: Thread #" + Thread.CurrentThread.ManagedThreadId);
#endif
                    request.BeginGetResponse(new AsyncCallback(FinishGetResponseAsync), request);
                    // Wait until the ManualResetEvent is set so that the application does not exit until after the callback is called.
                    // TODO add and handle proper timeout
                    _asyncTrigger.WaitOne();
                }

                using (_asyncResponse)
                {
                    jsonData = GetData(_asyncResponse);
                }

                if (AutoRetry && (jsonData.status == ResponseStatus.OVER_QUERY_LIMIT))
                {
#if DEBUG
                    Debug.WriteLine("[GetResponse]: Auto retry #" + n);
#endif
                    Thread.Sleep(n * 100);
                    continue;
                }
                break;
            }

            return new GeocodingResponse(jsonData);
        }

        private void FinishGetResponseAsync(IAsyncResult result)
        {
#if DEBUG
            Debug.WriteLine("[FinishGetResponseAsync]: Thread #" + Thread.CurrentThread.ManagedThreadId);
#endif
            var request = result.AsyncState as WebRequest;

            if (request != null)
            {
                _asyncResponse = request.EndGetResponse(result);
                // Set the ManualResetEvent so the main thread can exit.
                _asyncTrigger.Set();
            }
        }
#endif

#if NET45
        public async Task<GeocodingResponse> GetResponseAsync()
        {
            Validate();

            string url = BuildRequestUrl();
            GeocodingJsonData jsonData = null;

            for (int n = 1; n <= 3; n++)
            {
                var request = WebRequest.Create(url);

                using (var response = await request.GetResponseAsync())
                {
                    jsonData = GetData(response);
                }

                if (AutoRetry && (jsonData.status == ResponseStatus.OVER_QUERY_LIMIT))
                {
#if DEBUG
                    Debug.WriteLine("[GetResponse]: Auto retry #" + n);
#endif
                    await Task.Delay(n * 100);
                    continue;
                }
                break;
            }

            return new GeocodingResponse(jsonData);
        }
#endif

        private string BuildRequestUrl()
        {
            var buffer = new StringBuilder(RequestUrl);

            if (Location != null)
            {
                buffer.AppendFormat("latlng={0}", Location.ToString());
            }
            else
            {
                buffer.AppendFormat("address={0}", HttpUtility.UrlEncode(Address));
            }

            buffer.AppendFormat("&sensor={0}", IsSensor.ToString().ToLower());

            if (!string.IsNullOrEmpty(Language))
            {
                buffer.AppendFormat("&language={0}", HttpUtility.UrlEncode(Language));
            }
            if (!string.IsNullOrEmpty(Region))
            {
                buffer.AppendFormat("&region={0}", HttpUtility.UrlEncode(Region));
            }

            return buffer.ToString();
        }

        private GeocodingJsonData GetData(WebResponse response)
        {
            GeocodingJsonData jsonData = null;
            string responseData = null;

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseData = reader.ReadToEnd();
            }

            if (responseData != null)
            {
                var serializer = new JavaScriptSerializer();
                jsonData = serializer.Deserialize<GeocodingJsonData>(responseData);
            }

            return jsonData;
        }

        private void Validate()
        {
            if ((Location != null) && (!string.IsNullOrEmpty(Address)))
            {
                throw new InvalidOperationException("Geocoding request must contain only one of 'Address' or 'Location'.");
            }

            if ((Location == null) && (string.IsNullOrEmpty(Address)))
            {
                throw new InvalidOperationException("Geocoding request must contain at least one of 'Address' or 'Location'.");
            }
        }
    }
}
