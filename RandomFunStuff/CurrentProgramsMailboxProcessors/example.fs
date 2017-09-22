open System

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

type Message =
    string * AsyncReplyChannel<string>

let replyAgent =
    MailboxProcessor<Message>.Start(fun inbox ->
    let rec loop () =
        async {
            let! (message, replyChannel) =
                inbox.Receive()
            replyChannel.Reply
                (String.Format
                    ("Received message: {0}", message))
            do! loop ()
        }
    loop ())

replyAgent.PostAndReply(fun rc -> "Hello", rc) |> ignore

let scanningAgent =
    MailboxProcessor.Start(fun inbox ->
        let rec loop () =
            async {
                do! inbox.Scan(fun hello ->
                    match hello with
                    | "Hello!" ->
                        Some(async {
                            printfn "This is a hello message!"
                        })
                    | _ -> None
                )
                let! msg = inbox.Receive()
                printfn "Got message '%s'" msg
                do! loop ()
            }
        loop ())

[ "1" ; "2" ; "3" ; "4" ; "5" ; "6" ; "7" ; "8" ; "9" ; "10" ; "Hello!" ;
"Hello!" ; "Hello!" ; "Hello!" ; "Hello!" ; "Hello!" ; "Hello!" ; "Hello!"
; "Hello!" ; "Hello!" ]
|> List.map scanningAgent.Post |> ignore

["1"; "2"; "3"] |> List.map scanningAgent.Post |> ignore

["Hello!"; "Hello!"; "Hello!"] |> List.map scanningAgent.Post |> ignore

type CoordinatorMessage =
    | Ready
    | RequestJob of AsyncReplyChannel<int>

// well i did like that blog post.. up til
// Coordinator was never deifned.

// doing the worker () has something to do with 
// instance memory sharing something or other
let Worker () =
    MailboxProcessor<bool>.Start(fun inbox ->
        let rec loop () =
            async {
                let! length =
                    Coordinator.PostAndAsyncReply
                        (fun reply -> RequestJob(reply))
                    do! Async.Sleep length
                return! loop ()
            }
        loop ())


let rec factorial n =
    match n with
    | 0 -> 1
    | _ -> n * factorial(n-1)

let rec greatestCommonFactor a b =
    match a with
    | a when a = 0 -> b
    | a when a < b -> greatestCommonFactor a (b - a)
    | _ -> greatestCommonFactor (a - b) b

// computes sum of a list of ints using recursion
let rec sumList xs =
    match xs with
    | [] -> 0
    | y::ys -> y + sumList ys

// makes sumlist tail recursive using helper function with result accumulator
let rec SumListTailRecHelper accumulator xs =
    match xs with
    | [] -> accumulator
    | y::ys -> SumListTailRecHelper (accumulator + y) ys

let sumListTailRecursive xs = SumListTailRecHelper 0 xs

printfn "sum of 1-100 is %d" (sumListTailRecursive [1..100])


// binary tree shenanigans
type BST<'T> =
    | Empty
    | Node of value:'T * list: BST<'T> * right: BST<'T>

let rec exists item bst =
    match bst with
    | Empty -> false
    | Node (x, left, right) ->
        match (x, left, right) with
        | x when x = item -> true
        | x when x > item -> (exists item left)
        | _ -> (exists item right)

let rec insert item bst =
    match bst with
    | Empty -> Node(item, Empty, Empty)
    | Node(x, left, right) as node ->
        match (x, left, right) with
        | x when x = item -> node
        | x when x > item -> Node(x, insert item left, right)
        | _ -> Node(x, left, insert item right)

let rec insertWithIf item bst =
    match bst with
    | Empty -> Node(item, Empty, Empty)
    | Node(x, left, right) as node ->
        if item = x then node
        elif item < x then Node(x, insert item left, right)
        else Node(x, left, insert item right)