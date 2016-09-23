using System;
using System.Globalization;

namespace Velyo.Google.Services.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LatLng : IEquatable<LatLng>
    {
        private static readonly NumberFormatInfo NumberFormat = CultureInfo.GetCultureInfo("en-US").NumberFormat;


        /// <summary>
        /// Initializes a new instance of the <see cref="LatLng"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public LatLng(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LatLng"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public LatLng(LatLng source) : this(source.Latitude, source.Longitude) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LatLng"/> class.
        /// </summary>
        public LatLng() : this(0, 0) { }


        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }


        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(LatLng x, LatLng y) => ReferenceEquals(x, null) ? ReferenceEquals(y, null) : x.Equals(y);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(LatLng x, LatLng y) => !(x == y);

        /// <summary>
        /// Parses the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static LatLng Parse(string point)
        {
            if (point == null) throw new ArgumentNullException(nameof(point));

            if (!string.IsNullOrEmpty(point))
            {
                point = point.Trim('(', ')');
                string[] pair = point.Split(',');

                if (pair.Length >= 2)
                {
                    double lat = Convert.ToDouble(pair[0], NumberFormat);
                    double lng = Convert.ToDouble(pair[1], NumberFormat);

                    return new LatLng(lat, lng);
                }

            }

            throw new ArgumentException("Invalid GeoLocation string format.");

        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) => (obj is LatLng) ? Equals(obj as LatLng) : false;

        /// <summary>
        /// Check of current instance is equal to another one.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(LatLng other) => !ReferenceEquals(other, null) 
            ? ((Latitude == other.Latitude) && (Longitude == other.Longitude)) : false;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => Latitude.GetHashCode() ^ Longitude.GetHashCode();

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Latitude.ToString(NumberFormat)},{Longitude.ToString(NumberFormat)}";
    }
}
