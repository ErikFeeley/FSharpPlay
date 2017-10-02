module PlayingWithDigits

open System

let getDigits number =
    let rec loop acc = function
    | number when number > 0 ->
        let newNumber, remainder = Math.DivRem(number, 10)
        loop (remainder :: acc) newNumber
    | _ -> acc
    loop [] number


let digits =
    let rec f acc n =
        if n = 0
        then acc
        else f (n % 10 :: acc) (n / 10)

    fun n -> if n = 0 then [0] else f [] n

let digPow n p =
    digits n
    |> Seq.mapi (fun i d -> pown (int64 d) (p + i))
    |> Seq.sum
    |> fun x -> if x % (int64 n) = 0L then x / (int64 n) else -1L

let otherdigPow n p =
    let n' = int64 n
    let result =
        n
        |> string
        |> Seq.map string
        |> Seq.mapi (fun index element -> (float (element)) ** (float (p + index)))
        |> Seq.sum
        |> int64

    if result % n' = 0L then result / n' else -1L

module IKindaLikeThisOne =
    let toDigits (n:int) =
        let s = n.ToString()
        s
        |> Seq.map (fun x -> x.ToString() |> int64) 
        |> Seq.toList

    let getPow (n:int) (p:int) =
        let digits = toDigits n
    
        digits
        |> List.mapi (fun i x -> pown x (i+ p)) 
        |> List.sum

    let getk (p:int64) (n:int) =
        let bn = int64 n
        if p % bn = 0L then
            Some (p / bn)
        else
            None
        

    let digPow (n: int) (p: int) : int64 =
        let pw = getPow n p
        let o = getk pw n
        match o with
        | Some x -> x
        | None -> -1L


// dis mah fav
module AnotherGoodOne =
    let digPow number power =
        let sumResult =
            [for chr in string number -> int (string chr)]
            |> List.mapi (fun index num -> int (Math.Pow (float num, float (index + power))))
            |> List.sum
        if sumResult % number = 0 then int64 (sumResult / number)
        else -1L


module OhUnfold =
    let digPow number power =
        let total =
            Seq.unfold (fun s -> if s = 0 then None else Some(s % 10, s / 10)) number
                |> Seq.rev
                |> Seq.mapi (fun idx x -> (int)((double)x ** (double)(idx + power)))
                |> Seq.sum
        if (total % number) = 0 then (int64)(total/number) else -1L
