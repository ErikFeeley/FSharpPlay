
let valueOr someDefault perhaps =
    match perhaps with
    | Some v -> v
    | None -> someDefault

let valueOr2 someDefault perhaps = 
    match perhaps with
    | Some v -> v
    | None -> someDefault