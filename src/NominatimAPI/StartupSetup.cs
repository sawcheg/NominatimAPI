using Microsoft.Extensions.DependencyInjection;
using NominatimAPI.Interfaces;
using NominatimAPI.Services;
using System.Net.Http;

namespace NominatimAPI
{
    public static class StartupSetup
    {
        public const string DefaultBaseUrl = @"https://nominatim.openstreetmap.org";
        /// <summary>
        /// Adds all Nominatim services and allows using through INominatimWebInterface
        /// </summary>
        /// <param name="services"></param>
        /// <param name="baseUrl"></param>
        /// <param name="apiKey"></param>
        public static void AddNominatimServices(this IServiceCollection services,
            string baseUrl = DefaultBaseUrl,
            string apiKey = null)
        {
            services.AddSingleton<INominatimWebInterface, NominatimWebInterface>(s
                 => new NominatimWebInterface(s.GetRequiredService<IHttpClientFactory>(), s, baseUrl, apiKey));
            services.AddSingleton<IAddressSearcher, AddressSearcher>();
            services.AddSingleton<IReverseGeocoder, ReverseGeocoder>();
            services.AddSingleton<IForwardGeocoder, ForwardGeocoder>();
        }
    }
}
