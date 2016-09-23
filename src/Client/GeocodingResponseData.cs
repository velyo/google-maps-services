using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Velyo.Google.Services.Models;

namespace Velyo.Google.Services
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class GeocodingResponseData
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [DataMember(Name = "status")]
        public GeocodingResponseStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        [DataMember(Name = "results")]
        public JsonResult[] Results { get; set; }

        [ScriptIgnore]
        public bool IsOk => Status == GeocodingResponseStatus.OK;
        [ScriptIgnore]
        public bool IsInvalidRequest => Status == GeocodingResponseStatus.INVALID_REQUEST;
        [ScriptIgnore]
        public bool IsOverQueryLimit => Status == GeocodingResponseStatus.OVER_QUERY_LIMIT;
        [ScriptIgnore]
        public bool IsRequestDenied => Status == GeocodingResponseStatus.REQUEST_DENIED;
        [ScriptIgnore]
        public bool IsZeroResults => Status == GeocodingResponseStatus.ZERO_RESULTS;


        /// <summary>
        /// 
        /// </summary>
        [DataContract]
        public class JsonAddress
        {
            [DataMember(Name = "long_name")]
            public string LongName { get; set; }
            [DataMember(Name = "short_name")]
            public string ShortName { get; set; }
            [DataMember(Name = "types")]
            public string[] Types { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataContract]
        public class JsonBounds
        {
            [DataMember(Name = "northeast")]
            public JsonLocation NorthEast;
            [DataMember(Name = "southwest")]
            public JsonLocation SouthWest;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataContract]
        public class JsonGeometry
        {
            [DataMember(Name = "bounds")]
            public JsonBounds bounds { get; set; }
            [DataMember(Name = "location")]
            public JsonLocation location { get; set; }
            [DataMember(Name = "location_type")]
            public LocationType location_type { get; set; }
            [DataMember(Name = "viewport")]
            public JsonBounds viewport { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataContract]
        public class JsonLocation
        {
            [DataMember(Name = "lat")]
            public double lat;
            [DataMember(Name = "lng")]
            public double lng;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataContract]
        public class JsonResult
        {
            //[DataMember(Name = "address_components")]
            public JsonAddress[] address_components { get; set; }
            public string formatted_address { get; set; }
            public JsonGeometry geometry { get; set; }
            public string partial_match { get; set; }
            public string[] types { get; set; }
        }
    }
}
