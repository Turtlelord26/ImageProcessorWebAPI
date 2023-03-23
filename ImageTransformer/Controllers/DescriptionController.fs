namespace FSImageTransformer.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open FSImageTransformer.Description

[<ApiController>]
[<Route("Commands")>]
[<Produces("text/plain")>]
type DescriptionController (logger : ILogger<DescriptionController>) =
    inherit ControllerBase()
    
    /// <summary>
    /// Get the command syntax.
    /// </summary>
    /// <response code="200">Returns the command syntax as text.</response>
    [<HttpGet>]
    member _.Get () =
        commandDescription
