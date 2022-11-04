using System.Threading.Tasks;
using NominatimAPI.Models;

namespace NominatimAPI.Interfaces {
    public interface IForwardGeocoder {
        /// <summary>
        ///     Attempt to get coordinates for a specified query or address.
        /// </summary>
        /// <param name="req">Geocode request object</param>
        /// <returns>Array of geocode responses</returns>
        Task<GeocodeResponse[]> Geocode(ForwardGeocodeRequest req);
    }
}