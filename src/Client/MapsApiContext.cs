using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;
#if NET45
using System.Threading.Tasks;
using MyToolkit.Utilities;
#endif

namespace Velyo.Google.Services
{
    /// <summary>
    /// A context object to share common settings between many Google Maps API requests.
    /// </summary>
    public class MapsApiContext
    {
        /// <summary>
        /// A default (test) singleton instance of context
        /// </summary>
        public static readonly MapsApiContext Default = Load();

        internal const string DefaultGeocodeApiUrl = "https://maps.google.com/maps/api/geocode/json?";

        internal const bool DefaultAutoRetry = true;
        internal const int DefaultRetryDelay = 100;// in milliseconds
        internal const int DefaultRetryTimes = 5;


        private readonly object _syncLock = new object();
#if NET45
        private readonly TaskSynchronizationScope _asyncLock = new TaskSynchronizationScope();
#endif


        public MapsApiContext(string apiKey)
        {
            ApiKey = apiKey;
        }

        public MapsApiContext() { }


        /// <summary>
        /// Google Maps API key to be used. You can issue one <see href="https://developers.google.com/maps/documentation/geocoding/get-api-key">here</see>
        /// </summary>
        public string ApiKey { get; private set; }

        /// <summary>
        /// Switch on/off auto retry on request problem. It is switched on by default.
        /// </summary>
        public bool AutoRetry { get; set; } = DefaultAutoRetry;

        /// <summary>
        /// Google Maps Geocode API web service URL.
        /// </summary>
        public string GeocodeApiUrl { get; set; } = DefaultGeocodeApiUrl;

        /// <summary>
        /// Delay between request retry, if retry is switch on. Default value is 100 milliseconds
        /// </summary>
        public int RetryDelay { get; set; } = DefaultRetryDelay;

        /// <summary>
        /// The number of times to retry Google Map API request on problem.
        /// </summary>
        public int RetryTimes { get; set; } = DefaultRetryTimes;


        /// <summary>
        /// Loads the context fro provided <see cref="NameValueCollection"/>.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static MapsApiContext Load(NameValueCollection settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            var prefix = "GoogleMaps";
            var context = new MapsApiContext
            {
                ApiKey = settings[$"{prefix}." + nameof(ApiKey)],
                GeocodeApiUrl = settings[$"{prefix}." + nameof(GeocodeApiUrl)] ?? DefaultGeocodeApiUrl
            };

            context.AutoRetry = settings.GetBool($"{prefix}." + nameof(AutoRetry), DefaultAutoRetry);
            context.RetryDelay = settings.GetInt($"{prefix}." + nameof(RetryDelay), DefaultRetryDelay);
            context.RetryTimes = settings.GetInt($"{prefix}." + nameof(RetryTimes), DefaultRetryTimes);

            return context;
        }

        /// <summary>
        /// Loads the context from configuration app settings. 
        /// </summary>
        /// <returns></returns>
        public static MapsApiContext Load()
        {
            return Load(ConfigurationManager.AppSettings);
        }

        /// <summary>
        /// Synchronized operation retry for current context on Google Maps API.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="process"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Retry<T>(Func<T> process, Func<T, bool> predicate)
        {
            T result = default(T);

            if (AutoRetry)
            {
                lock (_syncLock)
                {
                    for (int n = 0; n <= RetryTimes; n++)
                    {
                        Thread.Sleep((n + 1) * RetryDelay);
                        result = process();
                        if (predicate(result)) return result;
                    }
                }
            }

            return result;
        }

#if NET45
        /// <summary>
        /// Synchronized async operation retry for current context on Google Maps API.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="process"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<T> RetryAsync<T>(Func<Task<T>> process, Func<T, bool> predicate)
        {
            return _asyncLock.RunAsync(async () =>
            {
                T result = default(T);

                if (AutoRetry)
                {
                    for (int n = 0; n <= RetryTimes; n++)
                    {
                        await Task.Delay((n + 1) * RetryDelay);
                        result = await process();
                        if (predicate(result)) return result;
                    }
                }

                return result;
            });
        }
#endif
    }
}
