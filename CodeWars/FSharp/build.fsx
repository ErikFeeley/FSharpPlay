(* -- Fake Dependencies paket-inline
source https://api.nuget.org/v3/index.json

nuget Fake.Core.Target prerelease
nuget FSharp.Core prerelease
-- Fake Dependencies -- *)

#r @"packages/FAKE/tools/FakeLib.dll"

open Fake

Target "Clean" (fun _ -> trace "Cleaning stuff")

Target "Build" (fun _ -> trace "Building stuff")

Target "Deploy" (fun _ -> trace "Deploying stuff")

"Clean"
  ==> "Build"
  ==> "Deploy"

RunTargetOrDefault "Build"