open System

let digPow (n: int) (p: int): int =
    n

let getDigitsSum number =
    let rec loop acc = function
    | number when number > 0 ->
        let newNumber, remainder = Math.DivRem(number, 10)
        loop (acc + remainder) newNumber
    | _ -> acc
    loop 0 number

let getDigits number =
    let rec loop acc = function
    | number when number > 0 ->
        let newNumber, remainder = Math.DivRem(number, 10)
        loop (acc + remainder) newNumber
    | _ -> acc
    loop 0 number