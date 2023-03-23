module FSImageTransformer.Description

let commandDescription = """The command language is formatted as a ordered, newline-delimited string of commands
Allowed commands are (case insensitive):

flipH
    - Flips an image horizontally, or across its vertical line of symmetry.
flipV
    - Flips an image vertically, or across its horizontal line of symmetry.
rotateRight
    - Rotates an image 90 degrees to the right
rotateLeft
    - Rotates an image 90 degrees to the left
rotate {degrees}
    - Rotates an image by {degrees} degrees to the right. {degrees} is an unbounded float.
greyscale
    - Recolors an image to a default greyscale. 
    - Also accepts grayscale with an a.
greyscale {percent}
    - Recolors an image to a specified greyscale level. {percent} must be a float between 0 and 100 inclusive. 
    - Also accepts grayscale with an a.
saturate {percent}
    - Alters the saturation level of an image. Values greater than 100 increase saturation, values less decrease. {percent} must be a nonnegative float.
resize {width} {height}
    - Resizes the image to the given width and height in pixels. {wdith} and {height} must be nonnegative integers.
    - If either width or height is 0, it is calculated relative to the other, preserving aspect ratio. Both cannot be zero.
resize {percent}
    - Resizes an image by the supplied scale. {percent} is a positive float.
thumbnail
    - Generates a thumbnail from the given image. The thumbnail is 177 pixels tall, preserving aspect ratio."""
