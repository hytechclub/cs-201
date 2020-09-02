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
A `Texture2D` object represents a two-dimensional grid of graphical units. Check out [this page](https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb199316(v=xnagamestudio.35)) for more information.

For the purposes of this course, think of a `Texture2D` object as an image. To load an asset into a `Texture2D` variable, use the following code:

```cs
Texture2D myTexture = Content.Load<Texture2D>("AssetName");
```

Note that the asset must first be loaded into the content pipeline, and the name passed to the `Load` method should NOT contain the file extension.

### `SpriteBatch`
A `SpriteBatch` object allows developers to draw multiple sprites to the screen. Check out [this page](https://docs.microsoft.com/en-us/previous-versions/windows/xna/bb199034(v=xnagamestudio.42)) for more information.

A `SpriteBatch` must `Begin` before drawing, and `End` after drawing:

```cs
spriteBatch.Begin();
// Drawing will happen here!
spriteBatch.End();
```

#### The `SpriteBatch.Draw` Method
There are a few different implementations of the `SpriteBatch.Draw` method. They can be viewed on [this page](https://docs.microsoft.com/en-us/previous-versions/windows/xna/bb196426(v=xnagamestudio.42)).

For the purposes of this course, the `SpriteBatch.Draw` method will be used as follows:

```cs
// Texture2D myTexture -> a Texture2D image
// Rectangle myDestinationRect -> The location where the image will appear on the screen
// Rectangle mySourceRect -> The part of the image to display
spriteBatch.Draw(myTexture, myDestinationRect, Color.White);
spriteBatch.Draw(myTexture, myDestinationRect, mySourceRect, Color.White);
```

### `Rectangle`
A `Rectangle` object in MonoGame represents a rectangle shape on the screen. For more information, check out [this page](https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb198628(v=xnagamestudio.35)). It has four `int` properties: an `X` and `Y` position (the coordinates of the top left corner on the screen), and a `Width` and `Height`.

`Rectangle` objects can be created as follows:

```cs
// int xPosition -> X coordinate of top left corner
// int yPosition -> Y coordinate of top left corner
// int rectWidth -> width of the rectangle
// int rectHeight -> height of the rectangle
Rectangle myRect = new Rectangle(xPosition, yPosition, rectWidth, rectHeight);
```

## <span>C#</span> Language Features
Coming Soon!