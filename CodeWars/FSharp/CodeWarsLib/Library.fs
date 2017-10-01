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