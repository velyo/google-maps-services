using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Velyo.Google.Services
{
    partial class GeocodingRequest
    {
        public async Task<GeocodingResponse> GetResponseAsync()
        {
            Validate();

            string url = BuildRequestUrl();
            GeocodingJsonData jsonData = null;
            bool autoRetry = _context?.AutoRetry ?? false;
            int retryTimeout = _context?.RetryTimeout ?? 0;
            int retryTimes = _context?.RetryTimes ?? 1;

            for (int n = 1; n <= retryTimes; n++)
            {
                var request = WebRequest.Create(url);

                using (var response = await request.GetResponseAsync())
                {
                    jsonData = GetData(response);
                }

                if (autoRetry && (jsonData.status == ResponseStatus.OVER_QUERY_LIMIT))
                {
                    await Task.Delay(n * retryTimeout);
                    continue;
                }
                break;
            }

            return new GeocodingResponse(jsonData);
        }
    }
}
