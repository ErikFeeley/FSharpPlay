// Learn more about F# at http://fsharp.org
module Program

open System
open Expecto
open CodeWarsLib.CodeWars
open SongDecoderTests

let tests =
  testList "My Cool Tests" [
    testList "Tests to Test if Tests Work" [
      testCase "A simple Test" <| fun () -> Expect.equal (2 + 2) 4 "2 + 2 = 4"
      // testCase "Should always fail" <| fun () -> Expect.equal (2 + 2) 42 "2 + 2 != 42"
    ]
    songDecoderTests

    testList "vowel count tests" [
      testCase "finds a three times" <| fun _ ->
        let expected = 3
        Expect.equal expected (getVowelCount "aaa123zx.,cmvzvnx") "aaa"
    ]

    testList "Open Or Senior Test" [
      testCase "Test 1" <| fun _ ->
        let expected = ["Open"; "Senior"]
        Expect.equal expected (openOrSenior [[10; 3]; [75; 8]]) "test 1"

      testCase "Test 2" <| fun _ ->
        let expected = ["Open"; "Open"]
        Expect.equal expected (openOrSenior [[20; 4]; [23; 6]]) "test 2"
    ]
  ]

[<EntryPoint>]
let main argv =
  runTests defaultConfig tests
  |> ignore
  0 // return an integer exit code
