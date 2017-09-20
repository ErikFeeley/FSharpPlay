// can specify generic type param on the MbP using <>
// non function whilte true flavor
let myFirstAgent =
    MailboxProcessor<string>.Start(fun inbox ->
        async { while true do
                let! msg = inbox.Receive()
                printfn "got message '%s'" msg})

// more i guess.. idiomatic? version using recursion for the loop control
// NOTE TO SELF: clarify what the ! aka bangs do. i think it relates to making that expression eval immediately.
let agentWithRecursion =
    MailboxProcessor<string>.Start(fun inbox ->
        // actually have to do messageLoop() and not messageLoop here need to make sure it knows it a function
        let rec messageLoop() = async {
            let! msg = inbox.Receive()
            printfn "message is: %s" msg
            return! messageLoop() // recurse the loop
            }
        messageLoop() // start the loop
    )

["ey"; "wat"; "cool"; "sammich"] |> List.map myFirstAgent.Post |> ignore

["will"; "this"; "be"; "in"; "order"; "i"; "think"; "it"; "might"] |> List.map agentWithRecursion.Post |> ignore