let input = System.IO.File.ReadLines(__SOURCE_DIRECTORY__ + "/input.txt");;

let isSafeAdj x y =
    let diff = abs(x-y)
    diff <= 3 && diff >= 1;;

let rec isSafeInc xs =
    match xs with
    | [] -> true
    | [_] -> true
    | x::y::tail -> x<y && isSafeInc (y::tail);;

let rec isSafeDec xs =
    match xs with
    | [] -> true
    | [_] -> true
    | x::y::tail -> x>y && isSafeDec (y::tail);;

let rec isSafeReport xs =
    match xs with
    | [] -> true
    | [_] -> true
    | x::y::tail -> 
        (isSafeDec (x::y::tail) || isSafeInc (x::y::tail)) &&
        isSafeAdj x y &&
        isSafeReport (y::tail);;

let splitBySpaces (line: string) = line.Split(" ") |> Array.toList |> List.map int;;

let safeReports = List.ofSeq input 
                  |> List.map splitBySpaces
                  |> List.filter isSafeReport
                  |> List.length;;

printfn "Part 1 result: %d" safeReports;;

//insane strat incoming
let removeOneElement lst =
    [for i in 0 .. List.length lst - 1 -> List.take i lst @ List.skip (i + 1) lst]

let isLineSafe line =
    let numbers = splitBySpaces line
    let permutations = removeOneElement numbers
    List.exists isSafeReport (numbers :: permutations)

let safeReportsDampened = 
    input
    |> Seq.filter isLineSafe
    |> Seq.length

printfn "Part 2 result: %d" safeReportsDampened
//god forgive me
