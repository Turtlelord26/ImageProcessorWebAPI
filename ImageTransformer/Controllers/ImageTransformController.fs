namespace FSImageTransformer.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open FSImageTransformer.Errors.Print
open FSImageTransformer.Transform

[<ApiController>]
[<Route("ImageTransform")>]
[<Produces("application/json")>]
type ImageTransformController (logger : ILogger<ImageTransformController>) =
    inherit ControllerBase()
    
    /// <summary>
    /// Transforms an image according to the supplied commands.
    /// </summary>
    /// <remarks>
    /// Request Structure:
    /// (All elements required)
    ///
    ///     POST /ImageTransform
    ///     {
    ///        "commands": string array,
    ///        "image": byte array
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns the transformed image as a Json-wrapped byte array.</response>
    /// <response code="400">Returns any errors encountered with image processing or command parsing as a string.</response>
    [<HttpPost>]
    member _.Post postContent =

        let processPost: Async<ActionResult> =
            async { 
                return 
                    match transformImage postContent with
                    | Ok transformedImage -> new OkObjectResult(transformedImage.ToArray())
                    | Error e -> new BadRequestObjectResult(e |> printError)
            }
        
        processPost |> Async.RunSynchronously
