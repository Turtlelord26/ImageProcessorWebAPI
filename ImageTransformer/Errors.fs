module FSImageTransformer.Errors

type Error =
    | ImageContentInvalid of string
    | ImageFormatNotRecognized of string
    | ImageFormatNotSupported of string
    | ImageProcessingFailure of string
    | InteropNullPointer of string
    | MalformedCommands of string
    | ObjectDisposed of string

module Print =

    open System.Text

    let printError error =
        match error with
        | ImageContentInvalid message -> message
        | ImageFormatNotRecognized message -> message
        | ImageFormatNotSupported message -> message
        | ImageProcessingFailure message -> message
        | InteropNullPointer message -> message
        | MalformedCommands errors -> errors
        | ObjectDisposed message -> message
    
    let errorBytes error =
        Encoding.UTF8.GetBytes(printError error)