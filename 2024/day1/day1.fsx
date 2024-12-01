let input = System.IO.File.ReadLines("day1/input.txt");;

let splitBySpaces (line: string): string * string = 
    let splitLine = line.Split("   ") 
    (splitLine.[0], splitLine.[1]);;

let pairList = List.ofSeq input |> List.map splitBySpaces |> List.unzip;;

let sortedPairList =
    let firstList, secondList = pairList
    let sortedFirst = List.sortBy int firstList
    let sortedSecond = List.sortBy int secondList
    List.zip sortedFirst sortedSecond;;

let diff (pair: string * string): int =
    let first = int (fst pair)
    let second = int (snd pair)
    abs (first - second);;

let diffs = List.map diff sortedPairList;;

let sum = List.sum diffs;;

printfn "Result: %d" sum
