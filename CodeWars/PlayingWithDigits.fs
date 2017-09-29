open System

let getDigits number =
    let rec loop acc = function
    | number when number > 0 ->
        let newNumber, remainder = Math.DivRem(number, 10)
        loop (remainder :: acc) newNumber
    | _ -> acc
    loop [] number

let digPow (n: int) (p: int): int =
    n


let toThePowers (numbers: int[]) (firstPower: int): int =
    firstPower