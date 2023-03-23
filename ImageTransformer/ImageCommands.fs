module FSImageTransformer.Commands

open System

open FSImageTransformer.ImageInterop

let MakeResizeXY width height = resizeDefault width height

let MakeResizePercent percent = resizePercent (percent / 100f)

let MakeRotate degrees = rotate (degrees % 360f)

let MakeSaturate percent = saturate (percent / 100f)

let MakeGreyscalePercent percent = greyscalePercent (percent / 100f)

let MakeGreyscaleDefault = greyscale

let MakeFlipVertical = flipVertical

let MakeFlipHorizontal = flipHorizontal

let MakeThumbnailGenerator = makeThumbnail

let ValidateMakeResizeXY commandString (width: string) (height: string) =
    try
        match int width, int height with
        | w, h when w >= 0 && h >= 0 && w + h > 0 -> MakeResizeXY w h |> Ok
        | _, _ -> 
            $"Could not resolve command {commandString}: Bounds error on {width} and/or {height}: width and height must be nonnegative integers and at most one may be 0."
            |> Error
    with
    | :? FormatException -> $"Could not resolve command {commandString}: Parsing error on {width} and/or {height}" |> Error

let ValidateMakeResizePercent commandString (percent: string) =
    try
        match single percent with
        | x when 0f < x -> MakeResizePercent x |> Ok
        | x -> $"Could not resolve command {commandString}: {x} is not a valid percent, must be positive." |> Error
    with
    | :? FormatException -> $"Could not resolve command {commandString}: Parsing error on {percent}" |> Error

let ValidateMakeRotate commandString (degrees: string) =
    try
        MakeRotate (single degrees) |> Ok
    with
    | :? FormatException -> $"Could not resolve command {commandString}: Parsing error on {degrees}" |> Error

let ValidateMakeSaturate commandString (percent: string) =
    try
        match single percent with
        | x when 0f <= x -> MakeSaturate x |> Ok
        | x -> $"Could not resolve command {commandString}: {x} is not a valid percent, must be nonnegative." |> Error
    with
    | :? FormatException -> $"Could not resolve command {commandString}: Parsing error on {percent}" |> Error

let ValidateMakeGreyscale commandString (percent: string) =
    try
        match single percent with
        | x when 0f <= x && x <= 100f -> MakeGreyscalePercent x |> Ok
        | x -> $"Could not resolve command {commandString}: {x} is not a valid percent, must be between 0 and 100, inclusive." |> Error
    with
    | :? FormatException -> $"Could not resolve command {commandString}: Parsing error on {percent}" |> Error
