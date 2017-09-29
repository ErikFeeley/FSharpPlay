// so can start at top
// top cube volume is 1 to the 3rd power
// each cube on the way down is num + 1 to the 3rd power
// keep going til n % current total cube volume = 0
// then thats the number of cubes needed... i think

let findNb (n: uint64): int =
    
