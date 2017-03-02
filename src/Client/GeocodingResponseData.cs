using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Velyo.Google.Services.Models;

namespace Velyo.Google.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class GeocodingResponseData
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public GeocodingResponseStatus status;

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public JsonResult[] results;

        [ScriptIgnore]
        public bool IsOk => status == GeocodingResponseStatus.OK;
        [ScriptIgnore]
        public bool IsInvalidRequest => status == GeocodingResponseStatus.INVALID_REQUEST;
        [ScriptIgnore]
        public bool IsOverQueryLimit => status == GeocodingResponseStatus.OVER_QUERY_LIMIT;
        [ScriptIgnore]
        public bool IsRequestDenied => status == GeocodingResponseStatus.REQUEST_DENIED;
        [ScriptIgnore]
        public bool IsZeroResults => status == GeocodingResponseStatus.ZERO_RESULTS;


        /// <summary>
        /// 
        /// </summary>
        [DataContract]
        public class JsonAddress
        {
            public string long_name;
            public string short_name;
            public string[] types;
        }

        [DataContract]
        public class JsonBounds
        {
            public JsonLocation northeast;
            public JsonLocation southwest;
        }

        [DataContract]
        public class JsonGeometry
        {
            public JsonBounds bounds;
            public JsonLocation location;
            public LocationType location_type;
            public JsonBounds viewport;
        }

        public class JsonLocation
        {
            public double lat;
            public double lng;
        }

        [DataContract]
        public class JsonResult
        {
            public JsonAddress[] address_components;
            public string formatted_address;
            public JsonGeometry geometry;
            public string partial_match;
            public string[] types;
        }
    }
}
