let sumTwoSmallestNumbers (numbers: int64[]) =
    numbers
    |> Array.sort
    |> Array.take 2
    |> Array.sum

