(* -- Fake Dependencies paket.dependencies
file ./paket.dependencies
group netcorebuild
-- Fake Dependencies -- *)

#load "./.fake/build.fsx/intellisense.fsx"
#r "./packages/netcorebuild/FAKE.Core.Target/lib/netstandard2.0/Fake.Core.Target.dll"
#r "./packages/netcorebuild/FAKE.DotNet.Cli/lib/netstandard2.0/Fake.DotNet.Cli.dll"

open Fake.Core
open Fake.DotNet.Cli

Target.Create "MyBuild" (fun _ ->
  printfn "message from mybuild target")

Target.RunOrDefault "MyBuild"