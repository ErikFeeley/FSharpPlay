open System
open PacketAssembler

let tryThingy parser =
    Seq.choose(parser >> function Some v -> Some v | _ -> None)

let buildPacket (s: string): PacketGuy option =
    match s.Split [|' '|] with
    | [|a; b; c; d|] ->
        Some { MessageId = byte a; PacketId = byte b; TotalPackets = byte c; Message = d }
    | _ ->
        None

let getLinesFromStdin =
    Seq.initInfinite (fun _ -> stdin.ReadLine ())

[<EntryPoint>]
let main argv =
    // let ints = read Int32.TryParse
    // let anInt = ints |> Seq.take 3 |> List.ofSeq
    // let packetGuys = read buildPacket
    
    // let strings = read (Some) |> Seq.take 5 |> List.ofSeq
    // strings
    // |> List.iter (printfn "%s")

    // let results = getLinesFromStdin |> tryThingy buildPacket |> List.ofSeq
    0 // return an integer exit code
