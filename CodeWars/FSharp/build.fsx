(* -- Fake Dependencies paket-inline
source https://nuget.org/api/v2

nuget Fake.Core.Targets prerelease
group Main
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