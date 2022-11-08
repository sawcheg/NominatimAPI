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
            serviceCollection.AddNoninatimServices();
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
            Assert.IsTrue(result.Length == 3);
            Assert.IsTrue(result[0].PlaceID == 298269405);
            Assert.IsTrue(result[1].DisplayName.Contains("Berlin"));
            Assert.IsTrue(result[2].OSMType == "way");
        }
    }
}