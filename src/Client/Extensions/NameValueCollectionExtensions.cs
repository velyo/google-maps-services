using System.Diagnostics;

namespace System.Collections.Specialized
{
    /// <summary>
    /// Extension methods for <see cref="NameValueCollection"/>.
    /// </summary>
    [DebuggerStepThrough]
    internal static class NameValueCollectionExtensions
    {
        public static bool GetBool(this NameValueCollection collection, string key, bool defaultValue = default(bool))
        {
            bool result = defaultValue;
            string value = collection[key];
            if (!string.IsNullOrEmpty(value))
            {
                bool.TryParse(value, out result);
            }
            return result;
        }

        public static DateTime GetDate(this NameValueCollection collection, string key, DateTime defaultValue = default(DateTime))
        {
            DateTime result = defaultValue;
            string value = collection[key];
            if (!string.IsNullOrEmpty(value))
            {
                DateTime.TryParse(value, out result);
            }
            return result;
        }

        public static decimal GetDecimal(this NameValueCollection collection, string key, decimal defaultValue = default(decimal)) 
        {
            decimal result = defaultValue;
            string value = collection[key];
            if (!string.IsNullOrEmpty(value))
            {
                decimal.TryParse(value, out result);
            }
            return result;
        }

        public static T GetEnum<T>(this NameValueCollection collection, string key, T defaultValue = default(T))
        {
            T result = defaultValue;
            string value = collection[key];
            if (!string.IsNullOrEmpty(value))
            {
                result = (T)Enum.Parse(typeof(T), value, true);
            }
            return result;
        }

        public static int GetInt(this NameValueCollection collection, string key, int defaultValue = default(int))
        {
            int result = defaultValue;
            string value = collection[key];
            if (!string.IsNullOrEmpty(value))
            {
                int.TryParse(value, out result);
            }
            return result;
        }

        public static string GetString(this NameValueCollection collection, string key, string defaultValue = default(string))
        {
            return collection[key] ?? defaultValue;
        }
    }
}
