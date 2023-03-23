namespace FSImageTransformer

open System.ComponentModel.DataAnnotations

type PostContent =
    { [<Required>] commands: string array;
      [<Required>] image: byte array }
    
module PostContent =

    let commandsOf postContent =
        postContent.commands
    
    let imageOf postContent =
        postContent.image
