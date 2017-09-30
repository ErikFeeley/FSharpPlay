// Learn more about F# at http://fsharp.org

open System
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.Utils.Collections

let app =
  choose
    [ GET >=> choose
        [ path "/" >=> OK "index wat"
          path "/hello" >=> OK "Hello GET"
          path "/goodbye" >=> OK "Good bye GET"]
      POST >=> choose
        [ path "/hello" >=> OK "Hello POST"
          path "/goodbye" >=> OK "Good by POST"] ]

[<EntryPoint>]
let main argv =
  startWebServer defaultConfig app
  0