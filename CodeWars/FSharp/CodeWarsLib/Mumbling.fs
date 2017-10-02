module Mumbling

open System

let accum (s : string) : string =
  s.ToLower ()
  |> Seq.mapi (fun index char -> (Char.ToUpper char |> string) + (String.replicate index (string char)))
  |> String.concat "-"