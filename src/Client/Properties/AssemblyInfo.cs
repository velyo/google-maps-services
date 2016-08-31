using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyTitle("Velyo.Google.Client")]
[assembly: AssemblyDescription(".NET client library for Google Maps API Web Services")]
[assembly: AssemblyCompany("Velyo Ivanov (http://velyo.net/)")]
[assembly: AssemblyProduct("Google Maps Services Client (https://github.com/velyo/google-maps-services)")]
[assembly: AssemblyCopyright("Copyright © 2010 Velyo Ivanov")]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: Guid("41c1cf7e-c4f7-4ec5-8eef-7bab183344af")]

[assembly: AssemblyVersion("2.0.45.*")]

[assembly: InternalsVisibleTo("Client.Tests")]