using System.Collections.Generic;
using System.Linq;
using Velyo.Google.Services.Models;
#if NET45
using System.Threading.Tasks;
#endif

namespace Velyo.Google.Services
{
    /// <summary>
    /// Set of helper Google Maps API methods.
    /// </summary>
    public static class MapsApi
    {
        public static GeocodingResponse Geocode(MapsApiContext context, string address)
        {
            return new GeocodingRequest(address, context).GetResponse();
        }

        public static GeocodingResponse Geocode(MapsApiContext context, LatLng location)
        {
            return new GeocodingRequest(location, context).GetResponse();
        }

        public static IEnumerable<GeocodingResponse> Geocode(MapsApiContext context, IEnumerable<string> addresses)
        {
            var request = new GeocodingRequest(context);

            foreach (var address in addresses)
            {
                request.Address = address;
                yield return request.GetResponse();
            }
        }

        public static IEnumerable<GeocodingResponse> Geocode(MapsApiContext context, IEnumerable<LatLng> locations)
        {
            foreach (var location in locations)
            {
                var request = new GeocodingRequest(location, context);
                yield return request.GetResponse();
            }
        }

        public static IEnumerable<GeocodingResponse> Geocode(MapsApiContext context, params string[] addresses)
        {
            return Geocode(context, addresses.AsEnumerable());
        }

        public static IEnumerable<GeocodingResponse> Geocode(MapsApiContext context, params LatLng[] locations)
        {
            return Geocode(context, locations.AsEnumerable());
        }

#if NET45 // async/await

        public static async Task<GeocodingResponse> GeocodeAsync(MapsApiContext context, string address)
        {
            return await new GeocodingRequest { Address = address }.GetResponseAsync();
        }

        public static async Task<GeocodingResponse> GeocodeAsync(MapsApiContext context, LatLng location)
        {
            return await new GeocodingRequest { Location = location }.GetResponseAsync();
        }

        public static async Task<IEnumerable<GeocodingResponse>> GeocodeAsync(MapsApiContext context, IEnumerable<string> addresses)
        {
            var result = new List<GeocodingResponse>();

            foreach (var address in addresses)
            {
                var request = new GeocodingRequest(address, context);
                //request.Address = address;
                result.Add(await request.GetResponseAsync());
            }

            return result;
        }

        public static async Task<IEnumerable<GeocodingResponse>> GeocodeAsync(MapsApiContext context, IEnumerable<LatLng> locations)
        {
            var result = new List<GeocodingResponse>();
            var request = new GeocodingRequest(context);

            foreach (var location in locations)
            {
                request.Location = location;
                result.Add(await request.GetResponseAsync());
            }

            return result;
        }

        public static async Task<IEnumerable<GeocodingResponse>> GeocodeAsync(MapsApiContext context, params string[] addresses)
        {
            return await GeocodeAsync(context, addresses.AsEnumerable());
        }

        public static async Task<IEnumerable<GeocodingResponse>> GeocodeAsync(MapsApiContext context, params LatLng[] locations)
        {
            return await GeocodeAsync(context, locations.AsEnumerable());
        }
#endif

#if NET35 || NET40 // APM

        public static GeocodingResponse GeocodeAsync(MapsApiContext context, string address)
        {
            return new GeocodingRequest(address, context).GetResponseAsync();
        }

        public static GeocodingResponse GeocodeAsync(MapsApiContext context, LatLng location)
        {
            return new GeocodingRequest(location, context).GetResponseAsync();
        }

        public static IEnumerable<GeocodingResponse> GeocodeAsync(MapsApiContext context, IEnumerable<string> addresses)
        {
            var request = new GeocodingRequest(context);

            foreach (var address in addresses)
            {
                request.Address = address;
                yield return request.GetResponseAsync();
            }
        }

        public static IEnumerable<GeocodingResponse> GeocodeAsync(MapsApiContext context, IEnumerable<LatLng> locations)
        {
            var request = new GeocodingRequest(context);

            foreach (var location in locations)
            {
                request.Location = location;
                yield return request.GetResponseAsync();
            }
        }

        public static IEnumerable<GeocodingResponse> GeocodeAsync(MapsApiContext context, params string[] addresses)
        {
            return GeocodeAsync(context, addresses.AsEnumerable());
        }

        public static IEnumerable<GeocodingResponse> GeocodeAsync(MapsApiContext context, params LatLng[] locations)
        {
            return GeocodeAsync(context, locations.AsEnumerable());
        }
#endif
    }
}