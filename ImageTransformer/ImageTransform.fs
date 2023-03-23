module FSImageTransformer.Transform

open FSImageTransformer.Commands
open FSImageTransformer.TransformMonoid
open FSImageTransformer.TransformMonoid.Utils
open FSImageTransformer.Errors
open FSImageTransformer.FormattedImage.Utils
open FSImageTransformer.ImageInterop
open FSImageTransformer.PostContent
open FSImageTransformer.StringInterop

let parseCommand commandString: Result<ImageSwitch, string> =

    let formatCommand =
        toLower
        >> trim
        >> split " "

    match commandString |> formatCommand with
    | [|"resize"; width; height|] -> ValidateMakeResizeXY commandString width height
    | [|"resize"; percent|] -> ValidateMakeResizePercent commandString percent
    | [|"rotateright"|] -> MakeRotate 90f |> Ok
    | [|"rotateleft"|] -> MakeRotate 270f |> Ok
    | [|"rotate"; degrees|] -> ValidateMakeRotate commandString degrees
    | [|"saturate"; percent|] -> ValidateMakeSaturate commandString percent
    | [|"greyscale"|]
    | [|"grayscale"|] -> MakeGreyscaleDefault |> Ok
    | [|"greyscale"; percent|]
    | [|"grayscale"; percent|] -> ValidateMakeGreyscale commandString percent
    | [|"fliph"|] -> MakeFlipHorizontal |> Ok
    | [|"flipv"|] -> MakeFlipVertical |> Ok
    | [|"thumbnail"|] -> MakeThumbnailGenerator |> Ok
    | _ -> commandString |> Error

let transformImage data =

    let makeTransform =
        commandsOf
        >> Array.map parseCommand
        >> Array.map TransformMonoid.Convert
        >> Array.sum
        >> GetResult

    let transformImage transform =
        imageOf
        >> imageFromBytes
        >> mapImageResult transform
        >> Result.bind toByteStream

    match data |> makeTransform with
    | Ok transform -> data |> transformImage transform
    | Error e -> e |> MalformedCommands |> Error
