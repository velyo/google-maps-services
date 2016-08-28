namespace Velyo.Google.Services
{
    public static class GeoAddressType
    {
        /// <summary>
        /// indicates a precise street address
        /// </summary>
        public const string street_address = "street_address";

        /// <summary>
        /// indicates a named route (such as "US 101"). 
        /// </summary>
        public const string route = "route";

        /// <summary>
        /// indicates a major intersection, usually of two major roads. 
        /// </summary>
        /// 
        public const string intersection = "intersection";

        /// <summary>
        /// indicates a political entity. Usually, this type indicates a polygon of some civil administration. 
        /// </summary>
        public const string political = "political";

        /// <summary>
        /// indicates the national political entity, and is typically the highest order type returned by the Geocoder.
        /// </summary>
        public const string country = "country";

        /// <summary>
        /// indicates a first-order civil entity below the country level. Within the United States, these administrative levels are states. Not all nations exhibit these administrative levels. 
        /// </summary>
        public const string administrative_area_level_1 = "administrative_area_level_1";

        /// <summary>
        /// indicates a second-order civil entity below the country level. Within the United States, these administrative levels are counties. Not all nations exhibit these administrative levels. 
        /// </summary>
        public const string administrative_area_level_2 = "administrative_area_level_2";

        /// <summary>
        /// indicates a third-order civil entity below the country level. This type indicates a minor civil division. Not all nations exhibit these administrative levels. 
        /// </summary>
        public const string administrative_area_level_3 = "administrative_area_level_3";

        /// <summary>
        /// indicates a commonly-used alternative name for the entity. 
        /// </summary>
        public const string colloquial_area = "colloquial_area";

        /// <summary>
        /// indicates an incorporated city or town political entity. 
        /// </summary>
        public const string locality = "locality";

        /// <summary>
        /// indicates an first-order civil entity below a locality 
        /// </summary>
        public const string sublocality = "sublocality";

        /// <summary>
        /// indicates a named neighborhood 
        /// </summary>
        public const string neighborhood = "neighborhood";

        /// <summary>
        /// indicates a named location, usually a building or collection of buildings with a common name 
        /// </summary>
        public const string premise = "premise";

        /// <summary>
        /// indicates a first-order entity below a named location, usually a singular building within a collection of buildings with a common name 
        /// </summary>
        public const string subpremise = "subpremise";

        /// <summary>
        /// indicates a postal code as used to address postal mail within the country. 
        /// </summary>
        public const string postal_code = "postal_code";

        /// <summary>
        /// indicates a prominent natural feature. 
        /// </summary>
        public const string natural_feature = "natural_feature";

        /// <summary>
        /// indicates an airport. 
        /// </summary>
        public const string airport = "airport";

        /// <summary>
        /// indicates a named park. 
        /// </summary>
        public const string park = "park";

        /// <summary>
        /// indicates a named point of interest. Typically, these "POI"s are prominent local entities that don't easily fit in another category such as "Empire State Building" or "Statue of Liberty." 
        /// </summary>
        public const string point_of_interest = "point_of_interest";

        /// <summary>
        /// indicates a specific postal box. 
        /// </summary>
        public const string post_box = "post_box";

        /// <summary>
        /// indicates the precise street number. 
        /// </summary>
        public const string street_number = "street_number";

        /// <summary>
        /// indicates the floor of a building address. 
        /// </summary>
        public const string floor = "floor";

        /// <summary>
        /// indicates the room of a building address. 
        /// </summary>
        public const string room = "room";

        public const string postal_code_prefix = "postal_code_prefix";

        public const string establishment = "establishment";

    }
}
