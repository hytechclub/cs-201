# Part 0 - Hello World
In this walkthrough, update the starter game so that a sprite appears on the screen.

## The MonoGame Framework
Open up the **ArcadeFlyerGame.cs** file to take a look at the structure of the game so far.

### The `Game` Class
The `ArcadeFlyerGame` class _inherits_ from the MonoGame `Game` class. See the code for the `Game` class [here](https://github.com/MonoGame/MonoGame/blob/develop/MonoGame.Framework/Game.cs). The overarching `Game` class is the entry point for most games. It handles setting up a window and graphics, and runs a game loop.

There are some fields and methods that come with pretty much any MonoGame game. Here are some methods of note:

- `ArcadeFlyerGame` constructor: This builds the game and sets some basic configuration (like the screen dimensions). It is called at creation.
- `LoadContent`: This method loads up the `spriteBatch` field, along with any assets. It is called once, after the game is created.
- `Update`: This method is called whenever the game should update (once per frame)
- `Draw`: This method is called when the game elements are meant to be drawn to the screen (once per frame)

Don't worry about understanding exactly how that works! The important thing to know is that the `ArcadeFlyerGame` class already has some built-in features that do not need to be defined.

### Note: The 2D Video Game World
The screen in a [two-dimensional video game](http://rbwhitaker.wikidot.com/monogame-introduction-to-2d-graphics) behaves very similarly to a cartesian plane. The big difference is that the Y axis _increases_ as it goes down, and the origin (0, 0) is in the top left corner. It looks like this:

![](https://i.imgur.com/SqJR5w9.png)

Understanding the layout of the screen is critical to developing a game, so keep this in mind throughout the course.

## Changing the Color
Before jumping into anything too complex, start with the game development version of a "Hello World" program: change the background color on the screen!

In the **ArcadeFlyerGame.cs** file, find the `Draw` method. There should be one statement in the body of the method:

```cs
GraphicsDevice.Clear(Color.CornflowerBlue);
```

Change `CornflowerBlue` to another color! Try removing the `.CornflowerBlue` text first, and then type the `.` again. That should trigger Visual Studio Code's intellisense, which will show all the available colors. It should end up looking something like this:

```cs
GraphicsDevice.Clear(Color.White);
```

Run the program, and verify that the new color appears!

## Updating the Content Build Pipeline
Now, it's time to add an image! There should be a picture of a crystal ball in the "Content" folder, with a filename of **MainChar.png**. The goal is to make that image appear in the game. To do that, it is necessary to update the [MonoGame Content Pipeline](http://rbwhitaker.wikidot.com/monogame-managing-content) so that it properly handles the image file.

First, install the [Open](https://marketplace.visualstudio.com/items?itemName=sandcastle.vscode-open) extension in Visual Studio Code. This makes it easy to open a non-code file in its default application.

Once the **Open** extension has been installed, right click on the **Content.mgcb** file from the **Content** folder, and select "Open with default application" from the menu:

![](https://i.imgur.com/LSJrAW9.png)

This will open the MonoGame Pipeline Tool. This tool allows developers to decide which assets to bring into the game. Click the "Add Existing Item" button to add a new item to the pipeline:

![](https://i.imgur.com/EtJFEUO.png)

In the file selector that opens, find the **MainChar.png** file. Double click it to open it:

![](https://i.imgur.com/JC7T9cA.png)

Next, click the "Build" button to build the actual content file (which will now include the **MainChar.png** file):

![](https://i.imgur.com/uqslNhh.png)

After that, the **Content.mgcb** file should be updated to include something like this:

```
#begin MainChar.png
/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=True
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:MakeSquare=False
/processorParam:TextureFormat=Color
/build:MainChar.png
```

Now, the **MainChar** image can be loaded into the game!

## Loading the Main Character Image
The **MainChar** asset is in the pipeline, but it still has to be loaded into the game. This will happen in the **ArcadeFlyerGame.cs** file (in the "src" folder).

The asset will be loaded and stored within a <a href="https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb199316(v=xnagamestudio.35)">Texture2D</a> field on the `ArcadeFlyerGame` class. `Texture2D` objects hold two dimensional graphical data, like images.

1. Add a new `Texture2D` field on the `ArcadeFlyerGame` class named `playerImage`  
    ```cs
    private Texture2D playerImage;
    ```
1. Find the `LoadContent` method in the `ArcadeFlyerGame` class
1. In the body of the `LoadContent` method, load in the texture using `Content.Load<Texture2D>`
    - Pass in the filename WITHOUT the extension: `"MainChar"`
1. Assign the loaded `Texture2D` object to the `playerImage` field  
    ```cs
    playerImage = Content.Load<Texture2D>("MainChar");
    ```

At this point, the image will NOT appear in the game. However, the image _has_ been loaded into the `playerImage` field, so it is now possible to use it!

## Displaying the Player Sprite
Now that the image has been loaded, it's time to actually display it.

### Using the Sprite Batch
The <a href="https://docs.microsoft.com/en-us/previous-versions/windows/xna/bb199034(v=xnagamestudio.42)">Sprite Batch</a> allows developers to draw multiple sprites to the screen. To do this, all the drawing must happen in between a `Begin` and `End` statement.

Use the existing `spriteBatch` field to begin and end the drawing:

```cs
spriteBatch.Begin();

// Drawing will happen here!

spriteBatch.End();
```

It is always necessary to end every sprite batch that has been started.

### Creating the Rectangle
In MonoGame, a lot of things can be represented using the <a href="https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb198628(v=xnagamestudio.35)">Rectangle</a> structure. A `Rectangle` has an `X` position, `Y` position, `Width`, and `Height` (all `int`s). The `X` and `Y` represent the coordinates of the top left corner of the rectangle.

In order to use the <a href="https://docs.microsoft.com/en-us/previous-versions/windows/xna/bb196413(v=xnagamestudio.10)">`SpriteBatch.Draw`</a> method, it is necessary to create a destination rectangle defining the location of the `Texture2D` object. To place the image in the top left corner, at its full width and height, use the following `Rectangle`:

```cs
Rectangle playerDestinationRect = new Rectangle(0, 0, playerImage.Width, playerImage.Height);
```

Make sure to place the code _after_ the `spriteBatch.Begin` statement and _before_ the `spriteBatch.End` statement.

### Drawing the Sprite
Now that the `Rectangle` has been created, use it to draw the sprite. The <a href="https://docs.microsoft.com/en-us/previous-versions/windows/xna/bb196413(v=xnagamestudio.10)">`SpriteBatch.Draw`</a> method takes in a `Texture2D` (the image), a `Rectangle` (the destination rectangle), and a `Color` (a tint modulator). Use the following command right above the `spriteBatch.End` statement to properly draw the sprite with no tint modulation:

```cs
spriteBatch.Draw(playerImage, playerDestinationRect, Color.White);
```

Run the program, and verify that the crystal ball appears on the screen!

The final code for this walkthrough is available on [GitHub](https://github.com/hylandtechoutreach/ArcadeFlyer/tree/Part1Start).