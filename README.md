# .NET Client for Google Maps Services

[![Build status](https://ci.appveyor.com/api/projects/status/ihwbnutdfeyb34pb?svg=true)](https://ci.appveyor.com/project/velyo/google-maps-services) 
[![Build status](https://ci.appveyor.com/api/projects/status/ihwbnutdfeyb34pb/branch/net35?svg=true&pendingText=net35&failingText=net35&passingText=net35)](https://ci.appveyor.com/project/velyo/google-maps-services/branch/net35) 
[![Build status](https://ci.appveyor.com/api/projects/status/ihwbnutdfeyb34pb/branch/net40?svg=true&pendingText=net40&failingText=net40&passingText=net40)](https://ci.appveyor.com/project/velyo/google-maps-services/branch/net40) 
[![Stories in Ready](https://badge.waffle.io/velyo/google-maps-services.svg?label=ready&title=Ready)](http://waffle.io/velyo/google-maps-services) 
[![Gitter](https://badges.gitter.im/velyo/google-maps-services.svg)](https://gitter.im/velyo/google-maps-services?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

.NET Client Library for Google Maps APIs Services

## Features
* Support standard and reverse geocoding;

### Samples
* Geocoding Request
```csharp
GeoRequest request = new GeoRequest("plovdiv bulgaria");
GeoResponse response = request.GetResponse();
GeoLocation location = response.Results[0].Geometry.Location;
double latitude = location.Latitude;
double longitude = location.Longitude;
// TODO use latitude/longitude values}}
```

* Reverse Geocoding Request
```csharp
GeoRequest request = new GeoRequest(42.1438409, 24.7495615);
GeoResponse response = request.GetResponse();
string address = response.Results[0].FormattedAddress;
 TODO use address values
```
