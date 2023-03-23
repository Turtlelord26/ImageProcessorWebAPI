module FSImageTransformer.StringInterop

let toLower (string: string) =
    string.ToLower()

let split (delimiter: string) (string: string) =
    string.Split(delimiter)

let trim (string: string) =
    string.Trim()
