using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using NominatimAPI.Extensions;
using NominatimAPI.Interfaces;
using NominatimAPI.Models;

namespace NominatimAPI.Services
{
    /// <summary>
    ///     Class to enable reverse geocoding (e.g. latitude and longitude to address)
    /// </summary>
    internal class ReverseGeocoder : GeocoderBase, IReverseGeocoder
    {

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="nominatimWebInterface">Injected instance of INominatimWebInterface</param>
        public ReverseGeocoder(INominatimWebInterface nominatimWebInterface) : base(nominatimWebInterface) { }

        protected override string ApiMethod => "reverse";

        /// <summary>
        ///     Attempt to get an address or location from a set of coordinates
        /// </summary>
        /// <param name="req">Reverse geocode request object</param>
        /// <returns>A single reverse geocode response</returns>
        public async Task<GeocodeResponse> ReverseGeocode(ReverseGeocodeRequest req)
        {
            var result = await NominatimWeb.GetRequest<GeocodeResponse>(GetRequestUrl(), BuildQueryString(req)).ConfigureAwait(false);
            return result;
        }

        private Dictionary<string, string> BuildQueryString(ReverseGeocodeRequest r)
        {
            var c = new Dictionary<string, string>();
            // We only support JSON
            c.AddIfSet("format", format);
            c.AddIfSet("key", Key);
            c.AddIfSet("lat", r.Latitude?.ToString(CultureInfo.InvariantCulture.NumberFormat));
            c.AddIfSet("lon", r.Longitude?.ToString(CultureInfo.InvariantCulture.NumberFormat));
            c.AddIfSet("zoom", r.ZoomLevel);
            c.AddIfSet("addressdetails", r.BreakdownAddressElements);
            c.AddIfSet("namedetails", r.ShowAlternativeNames);
            c.AddIfSet("accept-language", r.PreferredLanguages);
            c.AddIfSet("countrycodes", r.CountryCodeSearch);
            c.AddIfSet("polygon_geojson", r.ShowGeoJSON);
            c.AddIfSet("polygon_kml", r.ShowKML);
            c.AddIfSet("polygon_svg", r.ShowSVG);
            c.AddIfSet("polygon_text", r.ShowPolygonText);
            c.AddIfSet("extratags", r.ShowExtraTags);
            return c;
        }
    }
}
