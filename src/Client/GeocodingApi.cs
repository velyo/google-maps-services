using System.Collections.Generic;
#if NET45
using System.Threading.Tasks;
#endif

namespace Velyo.Google.Services
{
    public static class GeocodingApi
    {
        static public GeocodingRequest CreateRequest(GeoApiContext context)
        {
            return new GeocodingRequest(context);
        }

        static public GeocodingRequest CreateRequest(GeoApiContext context, string address)
        {
            return new GeocodingRequest(context, address);
        }

        static public GeocodingRequest CreateRequest(GeoApiContext context, LatLng location)
        {
            return new GeocodingRequest(context, location);
        }

        static public GeocodingResponse Geocode(GeoApiContext context, string address)
        {
            var request = CreateRequest(context, address);
            return request.GetResponse();
        }

        static public GeocodingResponse Geocode(GeoApiContext context, LatLng location)
        {
            var request = CreateRequest(context, location);
            return request.GetResponse();
        }

        static public IEnumerable<GeocodingResponse> Geocode(GeoApiContext context, IEnumerable<string> addresses)
        {
            foreach (var address in addresses)
            {
                var request = CreateRequest(context, address);
                yield return request.GetResponse();
            }
        }

        static public IEnumerable<GeocodingResponse> Geocode(GeoApiContext context, IEnumerable<LatLng> locations)
        {
            foreach (var location in locations)
            {
                var request = CreateRequest(context, location);
                yield return request.GetResponse();
            }
        }

#if NET45 // async/await

        static public async Task<GeocodingResponse> GeocodeAsync(GeoApiContext context, string address)
        {
            var request = new GeocodingRequest { Address = address };
            return await request.GetResponseAsync();
        }

        static public async Task<GeocodingResponse> GeocodeAsync(GeoApiContext context, LatLng location)
        {
            var request = new GeocodingRequest { Location = location };
            return await request.GetResponseAsync();
        }

         static public async Task<IEnumerable<GeocodingResponse>> GeocodeAsync(GeoApiContext context, IEnumerable<string> addresses)
        {
            var result = new List<GeocodingResponse>();
            foreach (var address in addresses)
            {
                var request = CreateRequest(context, address);
                result.Add(await request.GetResponseAsync());
            }
            return result;
        }

        static public async Task<IEnumerable<GeocodingResponse>> GeocodeAsync(GeoApiContext context, IEnumerable<LatLng> locations)
        {
            var result = new List<GeocodingResponse>();
            foreach (var location in locations)
            {
                var request = CreateRequest(context, location);
                result.Add(await request.GetResponseAsync());
            }
            return result;
        }
#endif

#if NET35 || NET40 // APM

        static public GeocodingResponse GeocodeAsync(GeoApiContext context, string address)
        {
            var request = new GeocodingRequest { Address = address };
            return request.GetResponseAsync();
        }

        static public GeocodingResponse GeocodeAsync(GeoApiContext context, LatLng location)
        {
            var request = new GeocodingRequest { Location = location };
            return request.GetResponseAsync();
        }

        static public IEnumerable<GeocodingResponse> GeocodeAsync(GeoApiContext context, IEnumerable<string> addresses)
        {
            foreach (var address in addresses)
            {
                var request = CreateRequest(context, address);
                yield return request.GetResponseAsync();
            }
        }

        static public IEnumerable<GeocodingResponse> GeocodeAsync(GeoApiContext context, IEnumerable<LatLng> locations)
        {
            foreach (var location in locations)
            {
                var request = CreateRequest(context, location);
                yield return request.GetResponseAsync();
            }
        }
#endif
    }
}