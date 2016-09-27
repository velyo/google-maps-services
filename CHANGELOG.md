# Roadmap

Google Maps APIs
- [ ] Directions API
- [ ] Distance Matrix API
- [ ] Elevation API
- [x] Geocoding API
- [ ] Places API
- [ ] Roads API
- [ ] Time Zone API

Features
- [ ] Portable class library build
- [ ] .NET Core library build
- [ ] SQLCLR functions for Google.Maps.Client usage inside MSSQL

Features that have a checkmark are complete and available for
download in the
[NuGet package](https://www.nuget.org/packages/Google.Maps.Client/).

# Changelog

These are the changes to each version that has been released.

## v2.1.0

**2016-09-26**

- [x] MapsApiContext to hold settings to share between requests
- [x] MapsApi class with some helper methods
- [x] Improved synchronization on request delay
- [x] More unit tests

## v2.0

**2016-09-15**

Fist release after splitting out the code from [GoogleMap Control repo](https://googlemap.codeplex.com/). 

- [x] Async Request/Response for NET45 - async/await
- [x] Async Request/Response for NET35 & NET40 - Asynchronous Programming Model (APM)
- [x] Support for multiple .NET frameworks (NET35, NET40, NET45+)
