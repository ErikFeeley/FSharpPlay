namespace CodeWarsLib

open System
open System.Text.RegularExpressions

module CodeWars =

  let songDecoder (song: string) =
    let replaced = ((Regex("(WUB)+")).Replace(song, " "))
    replaced.Trim()

  let isVowel v =
    match v with
    | 'a' | 'e' | 'i' | 'o' | 'u' -> 1
    | _ -> 0

  let rec countVowels x  =
    match x with
    | [] -> 0
    | head :: tail -> isVowel head + countVowels tail

  let getVowelCount (str: string) =
    str
    |> Seq.toList
    |> countVowels

  // senior is a member must be 55 years old handicap greater than 7
  // input: each inner list is info for one member which will be int: age; int: handicap
  // first crappy attempt
  let openOrSenior (xs: int list list) =
    xs
    |> Seq.map (fun x ->
      match x with
      | head :: tail -> if head > 54 && tail.[0] > 7 then "Senior" else "Open"
      | _ -> "Oh")
    |> List.ofSeq

  let openOrSeniorTwo = List.map <| function
    | [age; handicap] when age > 54 && handicap > 7 -> "Senior"
    | _ -> "Open"


  [[1; 2]; [3; 4]; [5; 6]]
  |> List.map (fun [a; b] ->
    match [a; b] with
    | [age; handicap] when age > 54 && handicap > 7 -> "Senior"
    | _ -> "Open")
  |> ignore

  let howMuchILoveYou nbPetals =
    match (nbPetals % 6) with
    | 0 -> "not at all"
    | 1 -> "I love you"
    | 2 -> "a little"
    | 3 -> "a lot"
    | 4 -> "passionately"
    | 5 -> "madly"
    | _ -> "NONONONO"

  