namespace FSImageTransformer.TransformMonoid

open FSImageTransformer.Errors
open FSImageTransformer.Errors.Print

type ImageSwitch = SixLabors.ImageSharp.Image -> Result<SixLabors.ImageSharp.Image, Error>

type ImageTwoTrack = Result<SixLabors.ImageSharp.Image, Error> -> Result<SixLabors.ImageSharp.Image, Error>

type TransformMonoid = { result: Result<ImageTwoTrack, string> } with
    
    static member (+) (cr1, cr2) = 
        match cr1.result, cr2.result with
        | Ok commands1, Ok commands2 -> { result = commands1 >> commands2 |> Ok }
        | Error errors, Ok _ -> { result = errors |> Error }
        | Ok _, Error errors -> { result = errors |> Error }
        | Error errors1, Error errors2 -> { result = errors1 + "\n" + errors2 |> Error }
    
    static member Zero =
        { result = Result.map id |> Ok }
    
    static member Convert command =
        match command with
        | Ok command -> { result = Result.bind command |> Ok }
        | Error error -> { result = error |> Error }

module Utils =

    let GetResult commandsMonoid = commandsMonoid.result
