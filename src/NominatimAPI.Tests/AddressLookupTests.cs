using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace NominatimAPI.Tests
{
    public class AddressLookupTests
    {
        private IServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddNominatimServices();
            serviceCollection.AddHttpClient();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Test]
        public async Task TestSuccessfulAddressLookup()
        {
            var addressSearcher = _serviceProvider.GetService<INominatimWebInterface>();
            var result = await addressSearcher.Lookup(new AddressSearchRequest
            {
                OSMIDs = new List<string>(new[] { "R146656", "R62422", "W50637691" }),
                BreakdownAddressElements = true,
                ShowAlternativeNames = true,
                ShowExtraTags = true
            });
            Assert.IsTrue(result.Length == 3, "Wrong result count - {0}!={1}", new object[] { result?.Length, 3 });
            Assert.IsTrue(result[0].Latitude == 53.4794892, "Wrong Latitude in result 1 - {0}!={1}", new object[] { result[0].Latitude == 53.4794892 });
            Assert.IsTrue(result[1].DisplayName.Contains("Berlin"), "Wrong result 2 - '{1}' not found in '{0}'", new object[] { result[1].DisplayName, "Berlin" });
            Assert.IsTrue(result[2].OSMType == "way", "Wrong OSMType in result 3 - {0}!={1}", new object[] { result[2].OSMType, "way" });
        }
    }
}