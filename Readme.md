#### Image Processor ASP.NET Web API

Built for Seattle University Course **SW Architecture and Design** (CPSC 5200), Spring 2022, Professor M. Miller.

Detailed design and usage documentation can be found in `DesignDocumentation.pdf`

ImageProcessor is an ASP.NET Web API written in F#. It knows how to accept three kinds of HTTP messages:
- HTTP POST `/imagetransform` messages containing image data and transform instructions: ImageProcessor applies the instructions in order and responds with an HTTP Ok message with the resulting image, in the original format. If there is a problem with the request, it sends an HTTP BadRequest with error messages instead.
- HTTP GET `/`: Displays a fully-functional Swagger UI with usage instructions
- HTTP GET `/commands`: Displays the image transformation commands keywords and usage in plaintext.

ImageProcessor can accept any image format known to the SixLabors.ImageSharp libary. At time of writing, this includes .bmp, .gif, .jpg, .pmb, .png, .tga, .tiff, and .webp

ImageProcessor depends on the SixLabors.ImageSharp and Swashbuckle.AspNetCore packages. At time of writing, ImageSharp was version 2.1.1, Swashbuckle was version 6.3.1, and FSharp.Core was version 6.0.2.

Client is a simple CLI HTTP client that communicates with the ImageProcessor endpoint, by default on localhost port 7261.
To start up the ImageProcessor locally, do `dotnet run` in the ImageProcessor directory before running the client.
Client Usage: `<program>.fs <PathToImage> <PathToCommandsFile> <PathForResult>`.
The Client directory also contains an example image, set of commands, and transformed image.

The commands language specification may be found in the design documentation and in `ImageTransformer/Controllers/DescriptionController.fs`.
