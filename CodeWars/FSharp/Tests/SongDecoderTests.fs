module SongDecoderTests

open Expecto
open CodeWarsLib.CodeWars

let songDecoderTests =
  testList "Song Decoder Tests" [
    testCase "Finds A B C" <| fun _ ->
      let expected = "A B C"
      Expect.equal expected (songDecoder "AWUBWUBWUBBWUBWUBWUBC") "Should be equal"

    testCase "So Champion Much Friend" <| fun _ ->
      let expected = "WE ARE THE CHAMPIONS MY FRIEND"
      Expect.equal expected (songDecoder "WUBWEWUBAREWUBWUBTHEWUBCHAMPIONSWUBMYWUBFRIENDWUB") "Should be equal"
  ]