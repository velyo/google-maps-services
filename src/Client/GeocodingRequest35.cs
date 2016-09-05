using System;
using System.Net;
using System.Threading;

namespace Velyo.Google.Services
{
    partial class GeocodingRequest
    {
        private WebResponse _asyncResponse;
        private ManualResetEvent _asyncTrigger;


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
            bool autoRetry = _context?.AutoRetry ?? false;
            int retryTimeout = _context?.RetryTimeout ?? 0;
            int retryTimes = _context?.RetryTimes ?? 1;

            for (int n = 1; n <= retryTimes; n++)
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
                    jsonData = GetData(_asyncResponse);
                }

                if (autoRetry && (jsonData.status == ResponseStatus.OVER_QUERY_LIMIT))
                {
                    Thread.Sleep(n * retryTimeout);
                    continue;
                }
                break;
            }

            return new GeocodingResponse(jsonData);
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
    }
}
