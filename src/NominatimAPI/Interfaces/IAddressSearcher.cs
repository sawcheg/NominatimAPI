﻿using System.Threading.Tasks;
using NominatimAPI.Models;

namespace NominatimAPI.Interfaces {
    public interface IAddressSearcher {
        /// <summary>
        /// Lookup the address of one or multiple OSM objects like node, way or relation.
        /// </summary>
        /// <param name="req"></param>
        Task<AddressLookupResponse[]> Lookup(AddressSearchRequest req);
    }
}