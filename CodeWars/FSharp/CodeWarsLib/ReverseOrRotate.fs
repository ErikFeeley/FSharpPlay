module ReverseOrRotate

open System

let stringContainsOnlyNumbers inputString =
    inputString
    |> Seq.forall Char.IsDigit

let chunkBySizeWithMinLength (checkLength: int) (source: string) =
    Seq.chunkBySize checkLength source
    |> Seq.filter (fun x -> Array.length x = checkLength)

let inline charToInt c = int c - int '0'

let cube x = x * x * x

let testStringList = ["123"; "123asdf"; "asdf"; "123Q!@#"; "asdf!@#"]

let testResults =
    testStringList
    |> List.map stringContainsOnlyNumbers
    |> Seq.iter (printfn "result was: %b")

let testString = "22222222"
let testSize = 3

'3' |> charToInt

Seq.sumBy charToInt testString

Seq.rev (string 1234123) |> String.Concat

let chunkResult =
    Seq.chunkBySize testSize testString

let chunkResultWithMinWidth =
    chunkBySizeWithMinLength testSize testString

let checkInputs (str: string) (size: int) :bool =
    match str, size with
    | (str, size) when size <= 0 -> false
    | (str, size) when Seq.isEmpty str -> false
    | (str, size) when Seq.length str < size -> false
    | _ -> true

let checkChunk (chars: char[]) =
    let chunkNumber = Seq.sumBy charToInt chars |> cube
    match chunkNumber with
    | chunkNumber when chunkNumber % 2 = 0 -> 
        Seq.rev
        |> String.Concat
    | _ -> String.Concat chunkNumber
        |> Array.ofSeq
        |> String.Concat

let revrot (str: string) (sz: int) =
    match checkInputs str sz with
    | true ->
        chunkBySizeWithMinLength sz str
        |> Seq.map checkChunk
        |> String.Concat
    | false -> ""

revrot testString testSize