// Learn more about F# at http://fsharp.org

open System
open Expecto
open CodeWarsLib.CodeWars

let tests =
  testList "My Cool Tests" [
    testList "Tests to Test if Tests Work" [
      testCase "A simple Test" <| fun () -> Expect.equal (2 + 2) 4 "2 + 2 = 4"
      testCase "Should always fail" <| fun () -> Expect.equal (2 + 2) 42 "2 + 2 != 42"
    ]

    testList "Song Decoder Tests" [
      testCase "Finds A B C" <| fun _ ->
        let expected = "A B C"
        Expect.equal expected (songDecoder "AWUBWUBWUBBWUBWUBWUBC") "Should be equal"

      testCase "So Champion Much Friend" <| fun _ ->
        let expected = "WE ARE THE CHAMPIONS MY FRIEND"
        Expect.equal expected (songDecoder "WUBWEWUBAREWUBWUBTHEWUBCHAMPIONSWUBMYWUBFRIENDWUB") "Should be equal"
    ]

    testList "vowel count tests" [
      testCase "finds a three times" <| fun _ ->
        let expected = 3
        Expect.equal expected (getVowelCount "aaa123zx.,cmvzvnx") "aaa"
    ]
  ]

[<EntryPoint>]
let main argv =
  runTests defaultConfig tests
  |> ignore
  0 // return an integer exit code
