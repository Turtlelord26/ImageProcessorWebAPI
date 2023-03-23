namespace FSImageTransformer.FormattedImage

type FormattedImage =
    { image: SixLabors.ImageSharp.Image
      format: SixLabors.ImageSharp.Formats.IImageFormat }

module Utils =
    
    let makeFormattedImage format image =
        { image = image
          format = format }
    
    let getImage fimage = fimage.image

    let getFormat fimage = fimage.format
    
    let mapImageResult mapper fimageResult =
        match fimageResult with
        | Ok fimage -> 
            Result.map (makeFormattedImage fimage.format) (fimage.image |> Ok |> mapper)
        | Error e -> Error e
