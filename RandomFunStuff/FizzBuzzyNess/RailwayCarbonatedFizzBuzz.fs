// imperative solution
module FizzBuzzMatch =
    let fizzBuzz i =
        match i with
        | _ when i % 15 = 0 ->
            printf "FizzBuzz"
        | _ when i % 3 = 0 ->
            printf "Fizz"
        | _ when i % 5 = 0 ->
            printf "Buzz"
        | _ ->
            printf "%i" i
        printf "; "
    [1..100] |> List.iter fizzBuzz

module FizzBuzzIfPrime =
    let fizzBuzz i =
        let mutable printed = false

        if i % 3 = 0 then
            printed <- true
            printf "Fizz"

        if i % 5 = 0 then
            printed <- true
            printf "Buzz"

        if not printed then
            printf "%i" i

        printf "; "

    [1..100] |> List.iter fizzBuzz

module FizzBuzzUsingFactorRules = 

    let fizzBuzz rules i  = 
        let mutable printed = false

        for factor,label in rules do
            if i % factor = 0 then
                printed <- true
                printf "%s" label

        if not printed then
            printf "%i" i
        
        printf "; "
    
    // do the fizzbuzz
    let rules = [ (3,"Fizz"); (5,"Buzz") ]
    [1..100] |> List.iter (fizzBuzz rules)
// done with imperative versions. sucks to have the mutable value

// lets try the pipeline version!

module FizzBuzzPipelineWithRecord =
    type Data = {i: int; label: string option}

    let carbonate factor label data =
        let {i = i; label = labelSoFar} = data
        if i % factor = 0 then
            let newLabel =
                match labelSoFar with
                | Some s -> s + label
                | None -> label
            {data with label = Some newLabel}
        else
            data

    let labelOrDefault data =
        let {i = i; label = labelSoFar} = data
        match labelSoFar with
        | Some s -> s
        | None -> sprintf "%i" i

    let fizzBuzzPipelineWithRecord i =
        {i = i; label = None}
        |> carbonate 3 "Fizz"
        |> carbonate 5 "Buzz"
        |> labelOrDefault
        |> printf "%s; "

    [1..100] |> List.iter fizzBuzzPipelineWithRecord

module FizzBuzzPipelineWithTuple =
    let carbonate factor label data =
        let (i, labelSoFar) = data
        if i % factor = 0 then
            let newLabel =
                labelSoFar
                |> Option.map (fun s -> s + label)
                |> defaultArg <| label
            (i, Some newLabel)
        else
            data

    let labelOrDefault data =
        let (i, labelSoFar) = data
        labelSoFar
        |> defaultArg <| sprintf "%i" i

    let fizzBuzzPipelineTuble i =
        (i, None)
        |> carbonate 3 "Fizz"
        |> carbonate 5 "Buzz"
        |> labelOrDefault
        |> printf "%s; " 

    [1..100] |> List.iter fizzBuzzPipelineTuble

module FizzBuzzPipelline_WithRules =
    let carbonate factor label data =
        let (i, labelSoFar) = data
        if i % factor = 0 then
            let newLabel =
                labelSoFar
                |> Option.map (fun s -> s + label)
                |> defaultArg <| label
            (i, Some newLabel)
        else
            data

    let labelOrDefault data =
        let (i , labelSoFar) = data
        labelSoFar
        |> defaultArg <| sprintf "%i" i

    let fizzBuzzPipelineRules rules i =
        let allRules =
            rules
            |> List.map (fun (factor, label) -> carbonate factor label)
            |> List.reduce (>>)

        (i, None)
        |> allRules
        |> labelOrDefault
        |> printf "%s; "

    let rules = [(3, "Fizz"); (5, "Buzz"); (7, "Baz")]
    [1..200] |> List.iter (fizzBuzzPipelineRules rules)

module RailwayCombinatorModule =
    let (|Success|Failure|) =
        function
        | Choice1Of2 s -> Success s
        | Choice2Of2 f -> Failure f

    // convert single value to two track result
    let succeed x = Choice1Of2 x

    // convert single value to two track result
    let fail x = Choice2Of2 x

    // apply either a success function or failure function
    let either successFunc failureFunc twoTrackInput =
        match twoTrackInput with
        | Success s -> successFunc s
        | Failure f -> failureFunc f

    // convert a switch function into a two track function
    let bind f =
        either f fail

    let carbonate factor label i=
        if i % factor = 0 then
            succeed label
        else
            fail i

    let connect f =
        function
        | Success x -> succeed x
        | Failure i -> f i

    let connect' f =
        either succeed f

    let fizzBuzzTwoTrackPipeline =
        carbonate 15 "FizzBuzz"
        >> connect' (carbonate 3 "Fizz")
        >> connect' (carbonate 5 "Buzz")
        >> either (printf "%s; ") (printf "%i; ")

    [1..100] |> List.iter fizzBuzzTwoTrackPipeline

module FizzBuzzRailwayOrientedCarbonationIsFailure =
    open RailwayCombinatorModule

    // carbonate a value
    // carb is failure because it causes us to skip 
    // the rest of the functions, not because it failed the condition
    // failure and success in this case pertain to the tracks that are followed

    let carbonate factor label i =
        if i % factor = 0 then
            fail label
        else
            succeed i

    let fizzBuzz =
        carbonate 15 "FizzBuzz"
        >> bind (carbonate 3 "Fizz")
        >> bind (carbonate 5 "Buzz")
        >> either (printf "%i; ") (printf "%s; ")

    [1..100] |> List.iter fizzBuzz

module FizzBuzzRailwayOrientedUsingCustomChoice =
    open RailwayCombinatorModule

    let (|Uncarbonated|Carbonated|) =
        function
        | Choice1Of2 u -> Uncarbonated u
        | Choice2Of2 c -> Carbonated c

    // convert a single value into a two track result
    let uncarbonated x = Choice1Of2 x
    let carbonated x = Choice2Of2 x

    let carbonate factor label i =
        if i % factor = 0 then
            carbonated label
        else
            uncarbonated i

    let connect f =
        function
        | Uncarbonated i -> f i
        | Carbonated x -> carbonated x

    let connect' f =
        either f carbonated

    let fizzBuzz =
        carbonate 15 "FizzBuzz"
        >> connect' (carbonate 3 "Fizz")
        >> connect' (carbonate 5 "Buzz")
        >> either (printf "%i; ") (printf "%s; ")

    //test
    [1..100] |> List.iter fizzBuzz

module FizzBuzzRailwayOrientedUsingAppend =
    open RailwayCombinatorModule

    let (|Uncarbonated|Carbonated|) =
        function 
        | Choice1Of2 u -> Uncarbonated u
        | Choice2Of2 c -> Carbonated c

    /// convert a single value into a two-track result
    let uncarbonated x = Choice1Of2 x
    let carbonated x = Choice2Of2 x

    let (<+>) switch1 switch2 x =
        match (switch1 x), (switch2 x) with
        | Carbonated s1, Carbonated s2 -> carbonated (s1 + s2)
        | Uncarbonated f1, Carbonated s2 -> carbonated s2
        | Carbonated s1, Uncarbonated f2 -> carbonated s1
        | Uncarbonated f1, Uncarbonated f2 -> uncarbonated f1

    // carbonate a value
    let carbonate factor label i =
        if i % factor = 0 then
            carbonated label
        else
            uncarbonated i

    let fizzBuzz =
        let carbonateAll =
            carbonate 3 "Fizz" <+> carbonate 5 "Buzz"

        carbonateAll
        >> either (printf "%i; ") (printf "%s; ")

    let fizzBuzzPrimes rules =
        let carbonateAll =
            rules
            |> List.map (fun (factor, label) -> carbonate factor label)
            |> List.reduce (<+>)
        carbonateAll
        >> either (printf "%i; ") (printf "%s; ")

    let rules = [(3, "Fizz"); (5, "Buzz"); (7, "Baz")]

    // test
    [1..200] |> List.iter (fizzBuzzPrimes rules)