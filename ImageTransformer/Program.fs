namespace FSImageTransformer
#nowarn "20"
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.OpenApi.Models

module Program =
    open System
    let exitCode = 0

    let setupOpenApiInfo (info: OpenApiInfo) =
        info.Description <- $"Apply transformation operations to an image."
        info
    
    let setupSwaggerGen (options: Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions) =
        options.SwaggerDoc("v1", new OpenApiInfo() |> setupOpenApiInfo)
        options.IncludeXmlComments("obj\\Debug\\net6.0\\FSImageTransformer.xml")
        ()
    
    let setupSwaggerUI (options: Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIOptions) =
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1")
        options.RoutePrefix <- ""

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers()
        builder.Services.AddSwaggerGen(setupSwaggerGen)

        let app = builder.Build()

        match app.Environment.IsDevelopment() with
        | true ->
            app.UseSwagger() |> ignore
            app.UseSwaggerUI(setupSwaggerUI) |> ignore
        | false -> ()

        app.UseHttpsRedirection()

        app.UseAuthorization()
        app.MapControllers()

        app.Run()

        exitCode
