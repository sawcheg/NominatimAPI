using System.Collections.Generic;
using System.Threading.Tasks;
using NominatimAPI.Extensions;
using NominatimAPI.Interfaces;
using NominatimAPI.Models;

namespace NominatimAPI.Services
{
    /// <summary>
    ///     Class to enable forward geocoding (e.g.  address to latitude and longitude)
    /// </summary>
    internal class ForwardGeocoder : GeocoderBase, IForwardGeocoder
    {

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="nominatimWebInterface">Injected instance of INominatimWebInterface</param>
        public ForwardGeocoder(INominatimWebInterface nominatimWebInterface) : base(nominatimWebInterface) { }

        protected override string ApiMethod => "search";

        /// <summary>
        ///     Attempt to get coordinates for a specified query or address.
        /// </summary>
        /// <param name="req">Geocode request object</param>
        /// <returns>Array of geocode responses</returns>
        public async Task<GeocodeResponse[]> Geocode(ForwardGeocodeRequest req)
        {
            var result = await NominatimWeb.GetRequest<GeocodeResponse[]>(GetRequestUrl(), BuildQueryString(req)).ConfigureAwait(false);
            return result;
        }

        private Dictionary<string, string> BuildQueryString(ForwardGeocodeRequest r)
        {
            var c = new Dictionary<string, string>();
            // We only support JSON
            c.AddIfSet("format", format);
            c.AddIfSet("key", Key);
            if (r.queryString.HasValue())
            {
                c.Add("q", r.queryString);
            }
            else
            {
                c.AddIfSet("street", r.StreetAddress);
                c.AddIfSet("city", r.City);
                c.AddIfSet("county", r.County);
                c.AddIfSet("state", r.State);
                c.AddIfSet("country", r.Country);
                c.AddIfSet("postalcode", r.PostalCode);
            }
            if (r.ViewBox != null)
            {
                var v = r.ViewBox.Value;
                c.Add("viewbox", $"{v.minLongitude},{v.minLatitude},{v.maxLongitude},{v.maxLatitude}");
            }
            c.AddIfSet("bounded", r.ViewboxBoundedResults);
            c.AddIfSet("addressdetails", r.BreakdownAddressElements);
            c.AddIfSet("limit", r.LimitResults);
            c.AddIfSet("accept-language", r.PreferredLanguages);
            c.AddIfSet("countrycodes", r.CountryCodeSearch);
            c.AddIfSet("namedetails", r.ShowAlternativeNames);
            c.AddIfSet("dedupe", r.DedupeResults);
            c.AddIfSet("polygon_geojson", r.ShowGeoJSON);
            c.AddIfSet("polygon_kml", r.ShowKML);
            c.AddIfSet("polygon_svg", r.ShowSVG);
            c.AddIfSet("polygon_text", r.ShowPolygonText);
            c.AddIfSet("extratags", r.ShowExtraTags);
            return c;
        }
    }
}