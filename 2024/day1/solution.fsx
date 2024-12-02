let input = System.IO.File.ReadLines(__SOURCE_DIRECTORY__ + "/input.txt");;

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

printfn "Part 1 result: %d" sum

let rec simScoreH (x: int) (ys: int list) (acc: int) : int =
    match ys with
    | [] -> x*acc
    | y::tail when x=y -> simScoreH x tail (acc+1)
    | _::tail -> simScoreH x tail acc;;

let simScore x ys = simScoreH x ys 0;;

let rec calculateSim (xs: int list) (ys: int list): int =
    match xs with
    | [] -> 0
    | x::tail -> (simScore x ys) + calculateSim tail ys;;


let simSum pairList = 
    let first, second = List.unzip pairList
    let firstInt = List.map int first
    let secondInt = List.map int second
    calculateSim firstInt secondInt;;

let simSumRes = simSum sortedPairList;;
printfn "Part 2 result: %d" simSumRes;;
