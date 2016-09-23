using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using Velyo.Google.Services.Models;
using System.Diagnostics;
#if NET45
using System.Threading.Tasks;
#endif

namespace Velyo.Google.Services
{
    public partial class GeocodingRequest
    {
        private MapsApiContext _context;
#if NET35 || NET40
        private WebResponse _asyncResponse;
        private ManualResetEvent _asyncTrigger;
#endif


        /// <summary>
        /// Initializes a new instance of the <see cref="GeocodingRequest"/> class.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="context"></param>
        public GeocodingRequest(string address, MapsApiContext context = null)
        {
            _context = context ?? MapsApiContext.Default;
            Address = address;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeocodingRequest"/> class.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="context"></param>
        public GeocodingRequest(LatLng location, MapsApiContext context = null)
        {
            _context = context ?? MapsApiContext.Default;
            Location = location;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeocodingRequest"/> class.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="context"></param>
        public GeocodingRequest(double latitude, double longitude, MapsApiContext context)
        {
            _context = context ?? MapsApiContext.Default;
            Location = new LatLng(latitude, longitude);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeocodingRequest"/> class.
        /// </summary>
        /// <param name="context"></param>
        public GeocodingRequest(MapsApiContext context = null)
        {
            _context = context  ?? MapsApiContext.Default;
        }


        /// <summary>
        /// The address that you want to geocode.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// The bounding box of the viewport within which to bias geocode results more prominently. 
        /// This parameter will only influence, not fully restrict, results from the geocoder. 
        /// (For more information <see href="https://developers.google.com/maps/documentation/geocoding/intro#Viewports">Viewport Biasing </see>)
        /// </summary>
        public Bounds Bounds { get; set; }

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
        /// Executes <code>GeocodingRequest</code> against Google Maps Geocoding API web service 
        /// and returns the result as <seealso cref="GeocodingResponse"/>.
        /// </summary>
        /// <returns>the result GeocodingResponse</returns>
        public GeocodingResponse GetResponse()
        {
            Validate();

            string url = BuildRequestUrl();
            GeocodingResponseData data = Process(url);

            if (data.IsOverQueryLimit)
            {
                GeocodingResponseData retryData = _context.Retry(() => Process(url), d => !d.IsOverQueryLimit);
                if(retryData != null)
                {
                    data = retryData;
                }
            }

            return new GeocodingResponse().Parse(data);
        }

#if NET45
        public async Task<GeocodingResponse> GetResponseAsync()
        {
            Validate();

            string url = BuildRequestUrl();
            GeocodingResponseData data = await ProcessAsync(url);

            if (data.IsOverQueryLimit)
            {
                GeocodingResponseData retryData = await _context.RetryAsync(async () => await ProcessAsync(url), d => !d.IsOverQueryLimit);
                if (retryData != null)
                {
                    data = retryData;
                }
            }

            return new GeocodingResponse().Parse(data);
        }

        private async Task<GeocodingResponseData> ProcessAsync(string url)
        {
            var request = WebRequest.Create(url);

            using (var response = await request.GetResponseAsync())
            {
                return await GetDataAsync(response);
            }
        }

        private async Task<GeocodingResponseData> GetDataAsync(WebResponse response)
        {
            GeocodingResponseData jsonData = null;
            string responseData = null;

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseData = await reader.ReadToEndAsync();
            }

            if (responseData != null)
            {
                var serializer = new JavaScriptSerializer();
                jsonData = serializer.Deserialize<GeocodingResponseData>(responseData);
            }

            return jsonData;
        }
#endif

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
            GeocodingResponseData data = ProcessAsync(url);

            if (data.IsOverQueryLimit)
            {
                data = _context.Retry(() => ProcessAsync(url), d => !d.IsOverQueryLimit);
            }

            return new GeocodingResponse().Parse(data);
        }

        private void FinishGetResponseAsync(IAsyncResult result)
        {
            var request = result.AsyncState as WebRequest;

            if (request != null)
            {
                _asyncResponse = request.EndGetResponse(result);
                // Set the ManualResetEvent so the main thread can exit.
                _asyncTrigger.Set();
            }
        }

        private GeocodingResponseData ProcessAsync(string url)
        {
            var request = WebRequest.Create(url);

            using (_asyncTrigger = new ManualResetEvent(false))
            {
                request.BeginGetResponse(new AsyncCallback(FinishGetResponseAsync), request);
                // Wait until the ManualResetEvent is set so that the application does not exit until after the callback is called.
                // TODO add and handle proper timeout
                _asyncTrigger.WaitOne();
            }

            using (_asyncResponse)
            {
                return GetData(_asyncResponse);
            }
        }
#endif

        private string BuildRequestUrl()
        {
            var buffer = new StringBuilder(_context?.GeocodeApiUrl ?? MapsApiContext.DefaultGeocodeApiUrl);

            if (Address != null)
            {
                var address = HttpUtility.UrlEncode(Address);
                buffer.Append($"address={address}");
            }
            else
            { // reverse geocoding
                buffer.Append($"latlng={Location}");
            }

            if (_context?.ApiKey != null)
            {
                buffer.Append($"&key={_context.ApiKey}");
            }

            if (Bounds != null)
            {
                buffer.Append($"&bounds={Bounds}");
            }

            if (!string.IsNullOrEmpty(Language))
            {
                var language = HttpUtility.UrlEncode(Language);
                buffer.Append($"&language={language}");
            }

            if (!string.IsNullOrEmpty(Region))
            {
                var region = HttpUtility.UrlEncode(Region);
                buffer.Append($"&region={region}");
            }

            var sensor = IsSensor.ToString().ToLower();
            buffer.Append($"&sensor={sensor}");

            return buffer.ToString();
        }

        private GeocodingResponseData GetData(WebResponse response)
        {
            GeocodingResponseData jsonData = null;
            string responseData = null;

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseData = reader.ReadToEnd();
            }

            if (responseData != null)
            {
                var serializer = new JavaScriptSerializer();
                jsonData = serializer.Deserialize<GeocodingResponseData>(responseData);
            }

            return jsonData;
        }

        private GeocodingResponseData Process(string url)
        {
            var request = WebRequest.Create(url);

            using (var response = request.GetResponse())
            {
                return GetData(response);
            }
        }

        [DebuggerStepThrough]
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
