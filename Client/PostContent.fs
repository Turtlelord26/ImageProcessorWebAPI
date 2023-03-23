namespace Client.PostContent

type PostContent =
    {  commands: array<string>;
       image: array<byte>  }

module Utils =

    let makePostContent commands image =
        { commands = commands;
          image = image }