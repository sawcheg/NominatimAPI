# NominatimAPI

Reworked project (Nominatim.API dll) for custom functions.
\
\
**How to use**

To register this service you need to add the following using statement and call to `AddNominatimServices` and `AddHttpClient` in your applications `Program.cs` file.

```csharp
using NoninatimAPI;
```
```csharp
builder.Services.AddHttpClient();
builder.Services.AddNominatimServices();
```

The code surface is very simple - use the `INominatimWebInterface` interface, here are the available methods:

```csharp
Task<AddressLookupResponse[]> Lookup(AddressSearchRequest req)
```

```csharp
Task<GeocodeResponse[]> Geocode(ForwardGeocodeRequest req)
```

```csharp
Task<GeocodeResponse> ReverseGeocode(ReverseGeocodeRequest req)
```

Also you can change the `BaseURL` and `ApiKey` through the corresponding properties.
