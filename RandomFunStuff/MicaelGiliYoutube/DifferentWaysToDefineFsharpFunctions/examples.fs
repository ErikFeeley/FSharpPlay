// helper for unwrapping values
// most straightfoward function 
// definition in F#
let valueOr someDefault perhaps =
    match perhaps with
    | Some v -> v
    | None -> someDefault

// just making it more explicit that 
// perhaps is a function
let valueOr2 someDefault perhaps =
    fun perhaps ->
        match perhaps with
        | Some v -> v
        | None -> someDefault

// so the top example is kinda like
// the first without the "sugar" so to speak
let valueOr3 = fun someDefault -> fun perhaps ->
    match perhaps with
    | Some v -> v
    | None -> someDefault

(*
    fun v ->
        match v with
        | ..
        | ...
        | ...
    =====================
    function 
        | ...
        | ...
        | ...
*)


// wat
let valueOr4 someDefault = function
    | Some v -> v
    | None -> someDefault