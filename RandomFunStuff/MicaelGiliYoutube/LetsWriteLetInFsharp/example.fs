let doSomething a b =
    let plusOne = a + 1
    let result = plusOne * b
    
    result

let doSomeThing2 a b =
    (fun plusOne ->
    (fun result ->
        result
    )(plusOne * b))(a + 1)

// so this let that we wrote here is really just
// the function application operator
let ``let`` value body = body value

let ``let 2`` = (|>)

let doSomething3 a b =
    a + 1 |> fun plusOne ->
    plusOne * b |> fun result ->

    result

// demostrate what let is really doing.
let a = 42
a

42 |> fun a ->
a

printfn "%d" (doSomething 20 2)
printfn "%d" (doSomeThing2 20 2)
printfn "%d" (doSomething3 20 2)