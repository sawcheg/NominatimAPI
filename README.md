# NominatimAPI

Reworked project (Nominatim.API dll) for custom functions.
\
\
**How to use**

To add a service call `.AddNoninatimServices()` to your `ServiceCollection`.

Finally, be sure to call `.AddHttpClient()` to your `ServiceCollection` for a vanilla `HttpClient` implementation.  Customize as needed for your respective DI library or use case.

The code surface is very simple - using the `INominatimWebInterface` interface, here are the available methods:

`Task<AddressLookupResponse[]> Lookup(AddressSearchRequest req)`

`Task<GeocodeResponse[]> Geocode(ForwardGeocodeRequest req)`

`Task<GeocodeResponse> ReverseGeocode(ReverseGeocodeRequest req)`

Also you can change the `BaseURL` and `ApiKey` through the corresponding properties.
