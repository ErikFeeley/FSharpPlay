// Learn more about F# at http://fsharp.org

open System

type Message =
        string * AsyncReplyChannel<string>

[<EntryPoint>]
let main argv =

    let replyAgent =
        MailboxProcessor<Message>.Start(fun inbox ->
            let rec loop () =
                async {
                    let! (message, replyChannel) =
                        inbox.Receive()
                    replyChannel.Reply(
                        String.Format(
                            "Received message: {0}", message
                        )
                    )
                    do! loop()
                }
            loop ()
        )
    printfn "Hello World from F#!"
    0 // return an integer exit code
