using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyTitle("Google.Maps.Client")]
[assembly: AssemblyCompany("Velyo Ivanov (http://velyo.net/)")]
[assembly: AssemblyProduct("Google Maps API Services Client (https://github.com/velyo/google-maps-services)")]
[assembly: AssemblyDescription(".NET Client Library for Google Maps APIs services")]
[assembly: AssemblyCopyright("Copyright © 2016 Velyo Ivanov")]
[assembly: AssemblyVersion("2.1.*")]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: Guid("41c1cf7e-c4f7-4ec5-8eef-7bab183344af")]

[assembly: InternalsVisibleTo("Velyo.Google.Client.Tests")]