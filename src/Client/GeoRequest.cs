using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Velyo.Google.Services
{
    public class GeoRequest
    {
        static readonly string RequestUrl = "http://maps.google.com/maps/api/geocode/json?";


        /// <summary>
        /// Initializes a new instance of the <see cref="GeoRequest"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        public GeoRequest(string address)
        {
            Address = address;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoRequest"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        public GeoRequest(GeoLocation location)
        {
            Location = location;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoRequest"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public GeoRequest(double latitude, double longitude)
        {
            Location = new GeoLocation(latitude, longitude);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoRequest"/> class.
        /// </summary>
        public GeoRequest() { }


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
        public GeoLocation Location { get; set; }

        /// <summary>
        /// The region code, specified as a ccTLD ("top-level domain") two-character value.
        /// </summary>
        /// <value>The region.</value>
        public string Region { get; set; }


        /// <summary>
        /// Creates the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        static public GeoRequest Create(string address)
        {
            return new GeoRequest(address);
        }

        /// <summary>
        /// Creates the reverse.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns></returns>
        static public GeoRequest CreateReverse(double latitude, double longitude)
        {
            return new GeoRequest(latitude, longitude);
        }

        /// <summary>
        /// Creates the reverse.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        static public GeoRequest CreateReverse(GeoLocation location)
        {
            return new GeoRequest(location);
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <returns></returns>
        public GeoResponse GetResponse()
        {
            Validate();

            string url = BuildRequestUrl();
            var request = WebRequest.Create(url);

            using (var response = request.GetResponse())
            {
                return BuildResponse(response);
            }
        }

        private string BuildRequestUrl()
        {
            var buffer = new StringBuilder(RequestUrl);

            if (Location != null)
            {
                buffer.AppendFormat("latlng={0}", Location.ToString());
            }
            else //if (!string.IsNullOrEmpty(Address))
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

        private GeoResponse BuildResponse(WebResponse response)
        {
            JsonGeoData jsonData = null;
            string responseData = null;

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseData = reader.ReadToEnd();
            }

            if (responseData != null)
            {
                var serializer = new JavaScriptSerializer();
                jsonData = serializer.Deserialize<JsonGeoData>(responseData);
            }
            else
            {
                jsonData = new JsonGeoData { status = GeoStatus.ZeroResults };
            }

            return new GeoResponse(jsonData);
        }

        private void Validate()
        {
            if((Location != null) && (!string.IsNullOrEmpty(Address)))
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
