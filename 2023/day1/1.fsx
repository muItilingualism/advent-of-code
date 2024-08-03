open System.Text.RegularExpressions
let lines = System.IO.File.ReadLines("input.txt")

//part 1
let sum (line: string) =
    let first = line.[0] 
    let firstAsString = first.ToString()
    let len = String.length line
    let second = line.[len-1]
    let secondAsString = second.ToString()
    let total = (firstAsString + secondAsString)
    total

let parseLine line = 
    line |> Seq.filter(fun c -> System.Char.IsDigit c) 
         |> System.String.Concat

let sumLine line = 
    line |> parseLine
         |> sum

//part 2
let spelled str =
    match str with
    | "one" -> "on1e"
    | "two" -> "tw2o"
    | "three" -> "thre3e"
    | "four" -> "fou4r"
    | "five" -> "fiv5e"
    | "six" -> "si6x"
    | "seven" -> "seve7n"
    | "eight" -> "eigh8t"
    | "nine" -> "nin9e"
    | x -> x

let replaceSubstr (input: string) =
    let regex = new Regex("(one|two|three|four|five|six|seven|eight|nine)", RegexOptions.IgnoreCase)
    let mutable currentInput = input
    let mutable lastResult = ""

    while lastResult <> currentInput do
        lastResult <- currentInput
        let matches = regex.Matches(currentInput) |> Seq.cast<Match> 
                                                  |> Seq.toList 
                                                  |> List.rev //rev cause otherwise it fucks up replacement

        let sb = System.Text.StringBuilder(currentInput)
        matches |> List.map (fun m ->
            sb.Remove(m.Index, m.Length).Insert(m.Index, spelled m.Value)) |> ignore //compiler complaints
        currentInput <- sb.ToString()
    currentInput

let part1 = lines |> Seq.map sumLine |> Seq.map int |> Seq.sum
printfn "%d" part1

let part2 = lines |> Seq.map replaceSubstr |> Seq.map sumLine |> Seq.map int |> Seq.sum
printfn "%d" part2


