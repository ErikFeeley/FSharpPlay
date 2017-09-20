type OriginalType = { id: int; name: string; hasAThing: bool; hasAOtherThing: bool }

// in order for newlist to work i needed this derived type definition.
// theres got to be a better way to do this tho... 
type DerivedType = { name: string; hasAOtherThing: bool }

let original1 = { id = 1; name = "bob"; hasAThing = true; hasAOtherThing = false }
let original2 = { id = 2; name = "matt"; hasAThing = false; hasAOtherThing = false }
let original3 = { id = 3; name = "dumbGuy"; hasAThing = true; hasAOtherThing = false }
let original4 = { id = 4; name = "foolishFool"; hasAThing = true; hasAOtherThing = true }
let original5 = { id = 5; name = "meh"; hasAThing = false; hasAOtherThing = true }

let originalList = [original1; original2; original3; original4; original5]

let newList =
    originalList |> Seq.map(fun original -> { name = original.name; hasAOtherThing = original.hasAOtherThing })