// without memoization
// so like fibs 50 takes FOREVER
let rec fibs n =
    match n with
    | 1L -> 1L
    | 2L -> 1L
    | n -> fibs (n - 1L) + fibs (n - 2L)

let rec fibsWithResult (n: int64) (prevResult: int64): int64 =
    match n with
    | 1L -> 1L
    | 2L -> 1L
    | n -> fibsWithResult (n - 1L) prevResult + fibsWithResult (n - 2L) prevResult

// memo table
// breaking pure function a bit here
// since we are using ref
let memo = ref Map.empty

// goal is to kinda remember last answers
// and look up those past answers to speed
// up the function.. i think.
// wow that sactually really fast
let rec memoizedFibs n =
    if Map.containsKey n !memo
    then Map.find n !memo
    else
        let result =
            match n with
            | 1L -> 1L
            | 2L -> 2L
            | n -> memoizedFibs (n - 1L) + memoizedFibs (n - 2L)
        
        memo := Map.add n result !memo

        result


// higher order function that will memoize
// a given function
let memoize f =
    let memo = ref Map.empty

    fun arg -> 
        if Map.containsKey arg !memo
        then Map.find arg !memo
        else
            let result = f arg

            memo := Map.add arg result !memo

            result

// final function that does the extra little
// memoization bit
let rec finalFibs =
    memoize <| fun n ->
        match n with
        | n when n <= 2L -> 1L
        | n -> finalFibs (n - 1L) + finalFibs (n - 2L)

[1L; 2L; 3L; 4L; 5L; 100L]
|> List.map finalFibs