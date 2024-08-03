open System
open System.Text.RegularExpressions
let lines = System.IO.File.ReadLines("input.txt")

//part 1

let parseGame (line: string) = 
    let lineWithoutPrefix = Regex.Replace(line, @"^Game [1-9][0-9]?[0-9]?: ", "")
    lineWithoutPrefix.Split(';')
    |> Seq.map (fun round -> 
        round.Trim().Split(',')
        |> Seq.map (fun item ->
            let parts = item.Trim().Split(' ')
            if parts.Length >= 2 then
                let number = int(parts.[0])
                let char = parts.[1].[0]
                Some(number, char)
            else
                None
            )
        |> Seq.choose id)
    |> Seq.toList


let getMaxCount color game = 
    game 
    |> List.map (fun round -> 
        round
        |> Seq.map (fun (count, rgb) -> 
            if rgb = color then count else 0
        )
        |> Seq.max
    )
    |> List.max

let isGameValid id game =
    let maxR = getMaxCount 'r' game
    let maxG = getMaxCount 'g' game
    let maxB = getMaxCount 'b' game
    if (maxR <= 12 && maxG <= 13 && maxB <= 14) then id else 0



let solve lines =
    lines 
    |> Seq.map parseGame
    |> Seq.map (fun game -> printfn "Parsed game: %A" game; game)
    |> Seq.mapi (fun i game -> 
        let valid = isGameValid (i+1) game
        printfn "Game %d validity: %A" (i+1) valid
        valid)
    |> Seq.filter (fun game -> game <> 0)
    |> Seq.map (fun game -> printfn "Filtered game: %A" game; game)
    |> Seq.sum


printfn "Valid games count: %d" (solve lines)
