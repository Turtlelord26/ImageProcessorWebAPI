module Client.Main

open System.IO
open System.Net
open System.Net.Http
open System.Net.Http.Json

open Client.PostContent
open Client.PostContent.Utils

let usage = "Usage: <program>.fs <PathToImage> <PathToCommandsFile> <PathForResult>"

let serviceURL = "https://localhost:7261/imagetransform"

let run (argv: string array) =
    let image = File.ReadAllBytes(argv[0])
    let commands = File.ReadAllLines(argv[1])
    use httpClient = new HttpClient()
    let postContent = JsonContent.Create<PostContent>(makePostContent commands image)
    let message = httpClient.PostAsync(serviceURL, postContent).Result
    printfn "%s" $"Reponse: Status {message.StatusCode}"
    match message.StatusCode with
    | HttpStatusCode.OK -> 
        let body = Async.RunSynchronously (Async.AwaitTask (message.Content.ReadFromJsonAsync<array<byte>>()))
        File.WriteAllBytes(argv[2], body)
    | HttpStatusCode.BadRequest -> 
        Async.RunSynchronously (Async.AwaitTask (message.Content.ReadAsStringAsync()))
        |> printfn "%s"
    | status -> printfn "%s" $"Unexpected Status Code: {status.ToString()}"

[<EntryPoint>]
let main argv =
    match argv.Length with
    | i when i < 3 -> usage |> printfn "%s"; 1 
    | _ -> run argv; 0
