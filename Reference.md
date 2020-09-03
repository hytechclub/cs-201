# <span>C# 201</span> Reference
Use this guide to review the concepts from the C# 201 course.

## MonoGame
MonoGame (which is a re-implementation of XNA) has a lot of useful features.

### Screen Layout
It is critical to understand the screen layout of a MonoGame project. Check out [this guide](http://rbwhitaker.wikidot.com/monogame-introduction-to-2d-graphics) for more information.

The screen works like this:

![](https://i.imgur.com/SqJR5w9.png)

### Content Pipeline
To add any assets (images, fonts, sounds) to a game, it is important to understand how the MonoGame Content Pipeline works. Check out [this guide](http://rbwhitaker.wikidot.com/monogame-managing-content) for more information.

The general steps to add an asset are:

1. Place the asset file in the "Content" folder
1. Open the **Content.mgcb** file in the MonoGame Content Pipeline Tool
1. Click the "Add Existing Item" button, and select the asset file
1. Click the "Build" button to include the asset in the content pipeline

### `Texture2D`
A `Texture2D` object represents a two-dimensional grid of graphical units. Check out <a href="https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb199316(v=xnagamestudio.35)">this page</a> for more information.

For the purposes of this course, think of a `Texture2D` object as an image. To load an asset into a `Texture2D` variable, use the following code:

```cs
Texture2D myTexture = Content.Load<Texture2D>("AssetName");
```

Note that the asset must first be loaded into the content pipeline, and the name passed to the `Load` method should NOT contain the file extension.

### `SpriteBatch`
A `SpriteBatch` object allows developers to draw multiple sprites to the screen. Check out <a href="https://docs.microsoft.com/en-us/previous-versions/windows/xna/bb199034(v=xnagamestudio.42)">this page</a> for more information.

A `SpriteBatch` must `Begin` before drawing, and `End` after drawing:

```cs
spriteBatch.Begin();
// Drawing will happen here!
spriteBatch.End();
```

#### The `SpriteBatch.Draw` Method
There are a few different implementations of the `SpriteBatch.Draw` method. They can be viewed on <a href="https://docs.microsoft.com/en-us/previous-versions/windows/xna/bb196426(v=xnagamestudio.42)">this page</a>.

For the purposes of this course, the `SpriteBatch.Draw` method will be used as follows:

```cs
// Texture2D myTexture -> a Texture2D image
// Rectangle myDestinationRect -> The location where the image will appear on the screen
// Rectangle mySourceRect -> The part of the image to display
spriteBatch.Draw(myTexture, myDestinationRect, Color.White);
spriteBatch.Draw(myTexture, myDestinationRect, mySourceRect, Color.White);
```

### `Rectangle`
A `Rectangle` object in MonoGame represents a rectangle shape on the screen. For more information, check out <a href="https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb198628(v=xnagamestudio.35)">this page</a>. It has four `int` properties: an `X` and `Y` position (the coordinates of the top left corner on the screen), and a `Width` and `Height`.

`Rectangle` objects can be created as follows:

```cs
// int xPosition -> X coordinate of top left corner
// int yPosition -> Y coordinate of top left corner
// int rectWidth -> width of the rectangle
// int rectHeight -> height of the rectangle
Rectangle myRect = new Rectangle(xPosition, yPosition, rectWidth, rectHeight);
```

### `Vector2`
In MonoGame, the `Vector2` structure stores two `float` values: an `X` and a `Y`. For more information, look at the documentation <a href="https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb199660(v=xnagamestudio.35)">here</a>. `Vector2` objects are used to store a number of different values. Due to the fact that the world is two-dimensional, game components often require data in both the X and Y axis. These objects can represent coordinate positions on the screen, dimensions of shapes, two-dimensional speeds, and much more.

`Vector2` objects can be created as follows:

```cs
// float xValue -> a value pertaining to the X axis
// float yValue -> a value pertaining to the Y axis
Vector2 myVector = new Vector2(xValue, yValue);
```

## <span>C#</span> Language Features
There are some important parts of the C# language that will be useful to know.

### Properties
In C#, a **property** is a more robust version of a field. [This page](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties) contains more in-depth information about properties. Properties can be used as if they are public fields, but they are actually special methods called _accessors_. The benefit to this is that other code can access properties in a simple way.

Below is an example of a property in C#.

**Defining the `Width` property:**

```cs
public class Shape
{
    // Private backing field
    private float width;

    // Property definition
    public float Width
    {
        // Get accessor
        get
        {
            return width;
        }

        // Set accessor
        set
        {
            // The value is whatever is being set
            width = value;
        }
    }
}
```

**Using the `Width` property:**

```cs
// Create a new Shape object
Shape myShape = new Shape();

// This will call the `set` accessor
myShape.Width = 500.0f;

// This will call the `get` accessor
float myShapeWidth = myShape.Width;
```

Properties can also be calculated:

```cs
public class Shape
{
    // Private backing fields
    private float width;
    private float imgWidth;
    private float imgHeight;

    // Public property for Height - note that there is no height backing field
    public int Height
    {
        // Get accessor
        get
        {
            // Calculate a scale for the height
            float scale = width / imgWidth;

            // Return the properly scaled height
            return imgHeight * scale;
        }
    }
}
```

There are also _auto-implemented_ properties, that do not require a backing field:

```cs
public class Shape
{
    public int Width
    {
        // Empty accessors -> automatically get/set an invisible backing field
        get;
        set;
    }
}
```

Properties are used frequently in C#, so it is important to understand how they work!

### Casting: Explicit Conversions
In C#, **casting** is a way to change the type of a variable. For more information, check out [this page](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions). Specifically, the type of casting used for this course will be [explicit conversions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions#explicit-conversions).

In this course, explicit conversions will be used to switch between number types (i.e., converting `int` to `float` and vice-versa). It looks like this:

```cs
// Set float
float x = 10.0f;

// Set int based on float; will be 10
int y = (int)x;

// Set float based on int; will be 10.0f
float z = (float)y;
```