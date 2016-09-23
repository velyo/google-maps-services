# .NET Client for Google Maps Services

[![Build status](https://ci.appveyor.com/api/projects/status/ihwbnutdfeyb34pb?svg=true)](https://ci.appveyor.com/project/velyo/google-maps-services) 
[![Build status](https://ci.appveyor.com/api/projects/status/ihwbnutdfeyb34pb/branch/net35?svg=true&pendingText=net35&failingText=net35&passingText=net35)](https://ci.appveyor.com/project/velyo/google-maps-services/branch/net35) 
[![Build status](https://ci.appveyor.com/api/projects/status/ihwbnutdfeyb34pb/branch/net40?svg=true&pendingText=net40&failingText=net40&passingText=net40)](https://ci.appveyor.com/project/velyo/google-maps-services/branch/net40)  
[![NuGet](https://img.shields.io/nuget/v/Google.Maps.Client.svg?maxAge=2592000)](https://www.nuget.org/packages/Google.Maps.Client/)  
[![Stories in Ready](https://badge.waffle.io/velyo/google-maps-services.svg?label=ready&title=Ready)](http://waffle.io/velyo/google-maps-services) 
[![Gitter](https://badges.gitter.im/velyo/google-maps-services.svg)](https://gitter.im/velyo/google-maps-services?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge) 

.NET Client for Google Maps APIs Services is a .NET Client library for the following Google Maps APIs:

 - [Geocoding API](https://developers.google.com/maps/documentation/geocoding)

## Getting started

### Requirements

 - .NET 3.5 or later.
 - A Google Maps API key.

### API keys

Each Google Maps Web Service request requires an API key or client ID. API keys
are freely available with a Google Account at
https://developers.google.com/console. The type of API key you need is a 
**Server key**. 

To get an API key:

 1. Visit https://developers.google.com/console and log in with
    a Google Account.
 1. Select one of your existing projects, or create a new project.
 1. Enable the API(s) you want to use. The .NET Client for Google Maps Services
    accesses the following APIs:
    * Geocoding API
 1. Create a new **Server key**.
 1. If you'd like to restrict requests to a specific IP address, do so now.
 
For guided help, follow the instructions for the [Directions API][directions-key]. 
You only need one API key, but remember to enable all the APIs you need.
For even more information, see the guide to [API keys][apikey].

**Important:** This key should be kept secret on your server.

### Installation

To install Google Maps API Services Client Library, run the following command in the Package Manager Console

```
Install-Package Google.Maps.Client
```

## Features
 
### Geocoding

#### Address Geocoding Request

```csharp
GeoRequest request = new GeoRequest("plovdiv bulgaria");
GeoResponse response = request.GetResponse();
// GeoResponse response = request.GetResponseAsync();
GeoLocation location = response.Results[0].Geometry.Location;
double latitude = location.Latitude;
double longitude = location.Longitude;
// TODO use latitude/longitude values}}
```

#### Reverse Geocoding Request

```csharp
GeoRequest request = new GeoRequest(42.1438409, 24.7495615);
GeoResponse response = request.GetResponse();
string address = response.Results[0].FormattedAddress;
// TODO use address values
```

#### Async Request/Response

Builds for all .NET frameworks support async request/response now.

```csharp
// NET35 & NET40 async get request
GeoRequest request = new GeoRequest("plovdiv bulgaria");
GeoResponse response = request.GetResponseAsync();
```

```csharp
// NET45 async get request
GeoRequest request = new GeoRequest("plovdiv bulgaria");
GeoResponse response = await request.GetResponseAsync();
```

## Contribute

Check out the [contribution guidelines](https://github.com/velyo/google-maps-services/blob/master/CONTRIBUTING.md) if you want to contribute to this project.

## License

[MIT](https://github.com/velyo/google-maps-services/blob/master/LICENSE)