module FSImageTransformer.ImageInterop

open System
open System.IO

open FSImageTransformer.Errors
open FSImageTransformer.FormattedImage.Utils

open SixLabors.ImageSharp
open SixLabors.ImageSharp.Processing

let imageFromBytes (bytes: byte array) =
    try
        let image = Image.Load(bytes)
        let format = Image.DetectFormat(bytes)
        makeFormattedImage format image |> Ok
    with
    | :? ArgumentNullException as e-> e.Message |> InteropNullPointer |> Error
    | :? UnknownImageFormatException as e -> e.Message |> ImageFormatNotRecognized |> Error
    | :? InvalidImageContentException as e -> e.Message |> ImageContentInvalid |> Error
    | :? NotSupportedException as e -> e.Message |> ImageFormatNotSupported |> Error

let toByteStream fimage =
    try
        let image = fimage |> getImage
        let format = fimage |> getFormat
        let stream = new MemoryStream()
        image.Save(stream, format)
        stream |> Ok
    with
    | :? ArgumentNullException as e-> e.Message |> InteropNullPointer |> Error

let mutate mutator (image: Image) =
    try
        image.Mutate(mutator)
        image |> Ok
    with
    | :? ArgumentNullException as e -> e.Message |> InteropNullPointer |> Error
    | :? ObjectDisposedException as e -> e.Message |> ObjectDisposed |> Error
    | :? ImageProcessingException as e -> e.Message |> ImageProcessingFailure |> Error

let widthOf (image: Image) = image.Size().Width

let resize resampler width height image =
    let mutator = fun (context: IImageProcessingContext) -> context.Resize(width, height, resampler, false) |> ignore
    mutate mutator image

let resizeDefault width height image =
    resize KnownResamplers.Bicubic width height image

let resizePercent ratio image =
    let width = (image |> widthOf |> single) * ratio |> int
    resizeDefault width 0 image

let makeThumbnail image =
    resize KnownResamplers.Lanczos3 0 177 image

let flip flipMode =
    let mutator = fun (context: IImageProcessingContext) -> context.Flip(flipMode) |> ignore
    mutate mutator

let flipHorizontal image =
    flip FlipMode.Horizontal image

let flipVertical image =
    flip FlipMode.Vertical image

let rotate (degrees: single) =
    let mutator = fun (context: IImageProcessingContext) -> context.Rotate(degrees) |> ignore
    mutate mutator

let greyscale image =
    let mutator = fun (context: IImageProcessingContext) -> context.Grayscale() |> ignore
    mutate mutator image

let greyscalePercent (level: single) =
    let mutator = fun (context: IImageProcessingContext) -> context.Grayscale(level) |> ignore
    mutate mutator

let saturate saturation =
    let mutator = fun (context: IImageProcessingContext) -> context.Saturate(saturation) |> ignore
    mutate mutator
