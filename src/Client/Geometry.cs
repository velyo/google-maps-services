namespace Velyo.Google.Services
{
    public class Geometry
    {
        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        /// <value>The bounds.</value>
        public GeoBounds Bounds { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public GeoLocation Location { get; set; }

        /// <summary>
        /// Gets or sets the type of the location.
        /// </summary>
        /// <value>The type of the location.</value>
        public GeoLocationType LocationType { get; set; }

        /// <summary>
        /// Gets or sets the viewport.
        /// </summary>
        /// <value>The viewport.</value>
        public GeoBounds Viewport { get; set; }

    }
}
