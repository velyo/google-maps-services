#r @"packages/FAKE/tools/FakeLib.dll"
open Fake
open System.IO

[<AutoOpen>]
module Settings =
    let buildDir = "./.build/"
    let deployDir = "./.deploy/"
    let testDir = "./.test/"
    let projects = !! "src/**/*.csproj" -- "src/**/*.Tests.csproj"
    let testProjects = !! "src/**/*.Tests.csproj"
    let packages = !! "./**/packages.config"

    let getOutputDir proj =
        let folderName = Directory.GetParent(proj).Name
        sprintf "%s%s/" buildDir folderName

    let build proj =
        let outputDir = proj |> getOutputDir
        MSBuildRelease outputDir "ResolveReferences;Build" [proj] |> ignore
    

[<AutoOpen>]
module Targets =
    Target "Clean" (fun() ->
        CleanDirs [buildDir; deployDir; testDir]
    )

    Target "Build" (fun() ->
        projects
        |> Seq.iter build
    )

    Target "BuildTest" (fun() ->
        testProjects
        |> MSBuildDebug testDir "Build"
        |> ignore
    )

    Target "Package" (fun _ ->
        NuGet (fun p -> 
            {p with
                Project = "Client"
                Version = "2.0"
                WorkingDir = "./src/Client/"
                OutputPath = "./.build/"
//                AccessKey = accessKey
                Publish = false}) 
            "./src/Client/Properties/Client.nuspec"
    )

    Target "Default" (fun _ ->
        ()
    )
   
"Clean"
==> "Build"
==> "BuildTest"
==> "Package"
==> "Default"

RunTargetOrDefault "Default"