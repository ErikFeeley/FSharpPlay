 module Monopoly =
    open System

    type Player =
    | Boot
    | Battleship
    | Iron
    // Etc.

    type Color =
    | Brown
    | LightBlue
    | Magenta
    // Etc.

    type Development =
    | Empty
    | Houses of count: int // can be more than one house
    | Hotel

    type Price = int

    type Location =
    | Street of 
        name: string *
        cost: Price *
        color: Color *
        development: Development *
        rentals: Price[] *
        owner: Player option
    | Utility of 
        name: string *
        cost: Price *
        owner: Player option
    | Station of
        name: string *
        cost: Price *
        owner: Player option
    | Tax of name: string * amount: Price
    | GoToJail
    | Go
    | CommunityChest
    | Chance
    | Jail
    | FreeParking

    let ownsLocation player location =
        match location with
        | Street(_, _, _, _, _, Some(owner))
        | Utility(_, _, (Some(owner)))
        | Station(_, _, (Some(owner))) ->
            player = owner
        | _ -> false

    let streetsOfColor color board =
        board
        |> Seq.filter (fun l ->
            match l with
            | Street(_, _, c, _, _, _) ->
                color = c
            | _ -> false)

    let ownsStreetSet player color board =
        board
        |> streetsOfColor color
        |> Seq.exists (fun loc -> 
            not (ownsLocation player loc))
        |> not

    let charge (player: Player) (location: Location) board =
        match location with
        | Street(_, _, color, development, rentals, owner) ->
            match owner with
            | None ->
                None
            | Some o when o <> player ->
                match development with
                | Empty ->
                    let factor = if ownsStreetSet o color board then 2 else 1
                    rentals.[0] * factor |> Some
                | Houses c ->
                    rentals.[c] |> Some
                | Hotel ->
                    Some rentals.[5]
            | Some _ ->
                None
        | Utility(name, rentals, owner) -> 
            raise <| NotImplementedException()
        | _ ->
            raise <| NotImplementedException()