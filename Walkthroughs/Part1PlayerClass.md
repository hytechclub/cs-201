# Part 1 - The `Player` Class
In this walkthrough, create a new `Player` class and use it to allow the player to control a character on the screen.

## Defining the `Player` Class
Define a `Player` class that will represent the game component that the user will control. 

### Setup
There is some essential setup necessary when defining a new class.

1. Create a new file named **Player.cs** in the "src" folder
1. At the top of the file, add two `using` statements that include the proper MonoGame tools:  
    ```cs
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    ```
1. Under the `using` statements, create a `namespace` wrapper to ensure that the `Player` class will be included in the same namespace as the `ArcadeFlyerGame` class:  
    ```cs
    namespace ArcadeFlyer2D { }
    ```
1. In the body of the `namespace` wrapper, define a new class named `Player`:  
    ```cs
    class Player { }
    ```

Now the `Player` has been defined! It does not do much of anything yet, but the basic setup is complete.

### Fields
The `Player` class should hold some sort of data so that a `Player` object can actually be displayed on the screen.

One thing the `Player` will need is a reference to the game that contains it. This can facilitate communication between the player and the overarching game structure. In the body of the `Player` class, add a field named `root` with a type of `ArcadeFlyerGame`:

```cs
private ArcadeFlyerGame root;
```

Another thing to track is the player's position on the screen. This can be stored in a `Vector2` structure in MonoGame. <a href="https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb199660(v=xnagamestudio.35)">This structure</a> simply holds two `float` values; an `X` and a `Y`. These can be used to represent the player's position. Add the field in the body of the `Player` class:

```cs
private Vector2 position;
```

Next, the player should have a `Texture2D` image. Add this field under the `position` field:

```cs
private Texture2D spriteImage;
```

Another important thing to store will be the width of the sprite as it appears on the screen. The sprite may need to resize the image to make it fit. Add this field under the `spriteImage` field:

```cs
private float spriteWidth;
```

Now the `Player` class actually has some data associate with it!

### Constructor and Content Loading
Next, add some initialization to the `Player` class that will build the player object.

1. Define a constructor for the `Player` class that takes in a `root` parameter of type `ArcadeFlyerGame`, and a `position` parameter of type `Vector2`:  
    ```cs
    public Player(ArcadeFlyerGame root, Vector2 position) { }
    ```
1. In the body of the constructor, initialize the `root` and `position` fields with `this.root` and `this.position`:  
    ```cs
    this.root = root;
    this.position = position;
    ```
1. Under those statements, set the `spriteWidth` field to `128.0f`:
    ```cs
    this.spriteWidth = 128.0f;
    ```
1. Under that, call a method named `LoadContent` that has yet to exist:
    ```cs
    LoadContent();
    ```
1. Time to define the method! In the `Player` class, define a new method named `LoadContent` with no parameters and a `void` return type:  
    ```cs
    public void LoadContent() { }
    ```
1. In the body of the `LoadContent` method, use the `root` field to load the `Texture2D` "MainChar" asset
1. Set the `spriteImage` property to the loaded `Texture2D`:  
    ```cs
    this.spriteImage = root.Content.Load<Texture2D>("MainChar");
    ```

Now, the class has a way to properly initialize a new `Player` object!

### Properties
Next, there are a couple of calculated [properties](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties) that will be necessary for the player. **Properties** in C# behave like fields, but they actually have special _accessor methods_ that can do some interesting things.

#### `SpriteHeight`
The `SpriteHeight` property will calculate the height of the sprite on the screen based on the height of the actual image asset, and the resizing scale from the width.

1. In the `Player` class, create a `float` property named `SpriteHeight`:  
    ```cs
    public float SpriteHeight { }
    ```
1. In the body of the `SpriteHeight` property, create a `get` accessor:  
    ```cs
    get { }
    ```
1. The code in the body of the `get` accessor will determine how the property returns its value; first, define a variable to calculate the scale:  
    ```cs
    float scale = spriteWidth / spriteImage.Width;
    ```
1. Return the adjusted height value:  
    ```cs
    return spriteImage.Height * scale;
    ```

That's it for the `SpriteHeight` property! This property will only be _gettable_; it will not be _settable_.

#### `PositionRectangle`
The position rectangle for the player represents the rectangle where they will appear on the screen. This can also be calculated based on the `position` field, the `spriteWidth` field, and the `SpriteHeight` property. Note that the `Rectangle` object must be created using `int` values, so the values from the `Player` class will have to be [casted](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions#explicit-conversions).

1. In the `Player` class, create a `Rectangle` property named `PositionRectangle`:  
    ```cs
    public Rectangle PositionRectangle { }
    ```
1. In the body of the `PositionRectangle` property, create a `get` accessor:
    ```cs
    get { }
    ```
1. In the body of the `get` accessor, return a new `Rectangle` with the properly casted x-coordinate, y-coordinate, width, and height (based on the `Player` values):  
    ```cs
    return new Rectangle((int)position.X, (int)position.Y, (int)spriteWidth, (int)SpriteHeight);
    ```

That's it for the `PositionRectangle` property! Like the `SpriteHeight` property, this property will only be _gettable_; it will not be _settable_.

### Updating and Drawing
Now the `Player` class has everything it needs to exist in the game. It's time to add functionality to make that happen!

1. In the `Player` class, define a new method named `Update` with a `void` return type and a `GameTime` parameter:  
    ```cs
    public void Update(GameTime gameTime) { }
    ```
1. Keep the body of the `Update` method empty for now
1. Under the `Update` method, define a new method named `Draw` with a `void` return type, a `GameTime` parameter, and a `SpriteBatch` parameter:  
    ```cs
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    ```
1. In the body of the `Draw` method, use the `spriteBatch` parameter to draw the `spriteImage` with the `PositionRectangle` and a white color mask:  
    ```cs
    spriteBatch.Draw(spriteImage, PositionRectangle, Color.White);
    ```

That's it! The `Player` class can now update and draw a player.

### Player Code
At this point, the code in the **Player.cs** file should look like this:

```cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Player
    {
        private ArcadeFlyerGame root;
        private Vector2 position;
        private Texture2D spriteImage;
        private float spriteWidth;

        public float SpriteHeight
        {
            get
            {
                float scale = spriteWidth / spriteImage.Width;
                return spriteImage.Height * scale;
            }
        }
        
        public Rectangle PositionRectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)spriteWidth, (int)SpriteHeight);
            }
        }

        public Player(ArcadeFlyerGame root, Vector2 position)
        {
            this.root = root;
            this.spriteWidth = 128.0f;
            this.position = position;

            LoadContent();
        }

        public void LoadContent()
        {
            this.spriteImage = root.Content.Load<Texture2D>("MainChar");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteImage, PositionRectangle, Color.White);
        }
    }
}
```

## Using the `Player` Class
Now that the `Player` class has been defined, it's time to start using it in the **ArcadeFlyerGame.cs** file!

1. Remove the `Texture2D playerImage` field from the `ArcadeFlyerGame` class, and add a `Player` field:  
    ```cs
    private Player player;
    ```
1. At the end of the `ArcadeFlyerGame` constructor, initialize the `player` field. Set it to a new `Player` object, passing in `this` for the root, and a `(0, 0)` vector for the position:  
    ```cs
    player = new Player(this, new Vector2(0.0f, 0.0f));
    ```
1. In the `LoadContent` method, remove the statement that loads the `"MainChar"` image. This happens in the `Player` class now, so it is not necessary here
1. In the `ArcadeFlyerGame` class `Update` method, call the `Player` class `Update` method:  
    ```cs
    player.Update(gameTime);
    ```
1. In the `ArcadeFlyerGame` class `Draw` method, remove the current drawing code between `spriteBatch.Begin();` and `spriteBatch.End();`
1. Replace the previous drawing code with a call to the `Player` class `Draw` method, passing in the `gameTime` and `spriteBatch`:  
    ```cs
    player.Draw(gameTime, spriteBatch);
    ```

Run the game to verify that the player sprite appears on the screen! It may seem like this was a lot of work for very little payoff, but creating the `Player` class will help tremendously with the organization of the code as development continues.

## Handling Input
Currently, the `Update` method on the `Player` class does nothing. It's time fix that and make the `Player` object controllable!

Use the `KeyboardState` structure to make this happen. This allows developers to detect which keys are pressed. For more information, check out <a href="https://docs.microsoft.com/en-us/previous-versions/windows/xna/bb197763(v=xnagamestudio.42)">this page</a>. Make the changes in the **Player.cs** file. 

1. Add a new `float` property to the `Player` class named `movementSpeed`, and set it equal to `4.0f`:  
    ```cs
    private float movementSpeed = 4.0f;
    ```
1. Define a new method on the `Player` class. The method should have:  
    - An access modifier of `private`
    - A return type of `void`
    - A name of `HandleInput`
    - A parameter named `currentKeyboardState` of type `KeyboardState`
    ```cs
    private void HandleInput(KeyboardState currentKeyboardState) { }
    ```
1. In the body of the `HandleInput` method, use the `currentKeyboardState.IsKeyDown` method to check if the `Keys.Up` key is pressed. Store the result in a `bool` variable:  
    ```cs
    bool upKeyPressed = currentKeyboardState.IsKeyDown(Keys.Up);
    ```
1. Create an `if` statement with `upKeyPressed` as the condition:  
    ```cs
    if (upKeyPressed) { }
    ```
1. In the body of the `if` statement, since the up key is pressed, decrement the `Y` value of the position to make the player move up:  
    ```cs
    position.Y -= movementSpeed;
    ```
1. Repeat the steps above for the Down, Left, and Right keys. Make sure the player moves in the proper direction for each key press
1. In the body of the `Update` method, use `Keyboard.GetState()` to get the current `KeyboardState` for the game and store it in a variable:  
    ```cs
    KeyboardState currentKeyboardState = Keyboard.GetState();
    ```
1. Under that line, call the `HandleInput` method, passing in the `currentKeyboardState` variable:  
    ```cs
    HandleInput(currentKeyboardState);
    ```

### The `HandleInput` Method
The code for the `HandleInput` method should look something like this:

```cs
private void HandleInput(KeyboardState currentKeyboardState)
{
    bool upKeyPressed = currentKeyboardState.IsKeyDown(Keys.Up);
    bool downKeyPressed = currentKeyboardState.IsKeyDown(Keys.Down);
    bool leftKeyPressed = currentKeyboardState.IsKeyDown(Keys.Left);
    bool rightKeyPressed = currentKeyboardState.IsKeyDown(Keys.Right);

    if (upKeyPressed)
    {
        position.Y -= movementSpeed;
    }
    
    if (downKeyPressed)
    {
        position.Y += movementSpeed;
    }
    
    if (leftKeyPressed)
    {
        position.X -= movementSpeed;
    }
    
    if (rightKeyPressed)
    {
        position.X += movementSpeed;
    }
}
```

The `Update` method should look something like this:

```cs
public void Update(GameTime gameTime)
{
    KeyboardState currentKeyboardState = Keyboard.GetState();
    HandleInput(currentKeyboardState);
}
```

Run the program and test out the game! The main character should move based on they keyboard state!

## Final Code
The final code for this walkthrough is available on [GitHub](https://github.com/hylandtechoutreach/ArcadeFlyer/tree/Part2Start).