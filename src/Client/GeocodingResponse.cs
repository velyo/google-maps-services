using System.Collections.Generic;

namespace Velyo.Google.Services
{
    public partial class GeocodingResponse : IEnumerable<GeocodingResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeocodingResponse"/> class.
        /// </summary>
        internal GeocodingResponse(GeocodingJsonData json)
        {

            Status = json.status;
            List<GeocodingResult> results = new List<GeocodingResult>();

            if (Status == ResponseStatus.OK)
            {
                foreach (var r in json.results)
                {

                    List<AddressComponent> addressComponents = new List<AddressComponent>(r.address_components.Length);
                    foreach (var ac in r.address_components)
                    {
                        addressComponents.Add(new AddressComponent
                        {
                            LongName = ac.long_name,
                            ShortName = ac.short_name,
                            Types = ac.types
                        });
                    }

                    Geometry geometry = new Geometry() { LocationType = r.geometry.location_type };

                    if (r.geometry.bounds != null)
                    {
                        geometry.Bounds = new Bounds
                        {
                            NorthEast = new LatLng
                            {
                                Latitude = r.geometry.bounds.northeast.lat,
                                Longitude = r.geometry.bounds.northeast.lng
                            },
                            SouthWest = new LatLng
                            {
                                Latitude = r.geometry.bounds.southwest.lat,
                                Longitude = r.geometry.bounds.southwest.lng
                            }
                        };
                    }

                    if (r.geometry.location != null)
                    {
                        geometry.Location = new LatLng
                        {
                            Latitude = r.geometry.location.lat,
                            Longitude = r.geometry.location.lng
                        };
                    }

                    if (r.geometry.viewport != null)
                    {
                        geometry.Viewport = new Bounds
                        {
                            NorthEast = new LatLng
                            {
                                Latitude = r.geometry.viewport.northeast.lat,
                                Longitude = r.geometry.viewport.northeast.lng
                            },
                            SouthWest = new LatLng
                            {
                                Latitude = r.geometry.viewport.southwest.lat,
                                Longitude = r.geometry.viewport.southwest.lng
                            }
                        };
                    }

                    results.Add(new GeocodingResult
                    {
                        AddressComponents = addressComponents,
                        FormattedAddress = r.formatted_address,
                        Geometry = geometry,
                        PartialMatch = r.partial_match,
                        Types = r.types
                    });
                }
            }

            Results = results;
        }


        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public IList<GeocodingResult> Results { get; protected internal set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public ResponseStatus Status { get; protected internal set; }


        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<GeocodingResult> GetEnumerator()
        {
            return Results.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Results.GetEnumerator();
        }
    }
}
