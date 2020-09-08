# Part 2 - The `Enemy` Class
In this walkthrough, create a new `Enemy` class and use it to display a moving enemy on the screen.

## Adding `ScreenHeight` and `ScreenWidth` Properties to the `ArcadeFlyerGame` Class
Before creating an enemy, it will be necessary to add two properties to the `ArcadeFlyerGame` class: `ScreenHeight` and `ScreenWidth`. This will allow the `Enemy` objects to know where the screen starts and ends. A simple way to create these properties is to use the `propfull` [code snippet](https://docs.microsoft.com/en-us/visualstudio/ide/visual-csharp-code-snippets?view=vs-2019).

1. Open the **ArcadeFlyerGame.cs** file
1. Make a new line in the body of the `ArcadeFlyerGame` class, under the existing private fields
1. If using VS Code with the C# extension, type in "propfull" and press `Enter`. The following code should appear automatically:  
    ```cs
    private int myVar;
    public int MyProperty
    {
        get { return myVar; }
        set { myVar = value; }
    }
    ```
1. Press the `Tab` key to tab through and update the property values; `myVar` should be `screenWidth`, and `MyProperty` should be `ScreenWidth`:
    ```cs
    private int screenWidth;
    public int ScreenWidth
    {
        get { return screenWidth; }
        set { screenWidth = value; }
    }
    ```
1. Set the initial field value to be `1600`, the preferred width of the screen
1. Add the `private` keyword in front of the `set` accessor, so that only the `ArcadeFlyerGame` class can set the screen width
1. Repeat the steps above for the `ScreenHeight` property, using `900` for the preferred height
1. In the `ArcadeFlyerGame` constructor, replace the hard-coded `1600` and `900` with the field values

### Code
`ArcadeFlyerGame` class:
```cs
private int screenWidth = 1600;
public int ScreenWidth
{
    get { return screenWidth; }
    private set { screenWidth = value; }
}

private int screenHeight = 900;
public int ScreenHeight
{
    get { return screenHeight; }
    private set { screenHeight = value; }
}
```

`ArcadeFlyerGame` constructor:
```cs
graphics.PreferredBackBufferWidth = screenWidth;
graphics.PreferredBackBufferHeight = screenHeight;
```

## Loading an Enemy Image Asset
To create an enemy, it will be necessary to have an image for the enemy! Use the following evil ball image, or any other image:

![](https://i.imgur.com/Z10wxnw.png)

1. Save a new image named **Enemy.png** in the "Content" folder
2. Open up the **Content.mgcb** file in the MonoGame Pipeline Tool
3. Click the "Add Existing Item" button
4. Select the **Enemy.png** file
5. Click the "Build" button

Now the "Enemy" asset should be loadable in the game!

## Defining the `Enemy` Class
Now it's time to create an `Enemy` class. This will be very similar to creating the `Player` class.

### Setup
Start with the basic setup for the class.

1. Create a new file named **Enemy.cs** in the "src" folder
1. Add `using` statements for `Microsoft.Xna.Framework` and `Microsoft.Xna.Framework.Graphics`
1. Create a `namespace` wrapper for `ArcadeFlyer2D`
1. In the body of the `ArcadeFlyer2D` namespace, define a `class` named `Enemy`

```cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Enemy
    {

    }
}
```

### Basic Fields
Add some basic private fields for the `Enemy` class:

- A reference to the `root` `ArcadeFlyerGame` object
- A `position` stored in a `Vector2` object
- A `spriteImage` stored in a `Texture2D` object
- A `spriteWidth` stored in a `float`
- A two-dimensional `velocity` stored in a `Vector2` object

```cs
private ArcadeFlyerGame root;
private Vector2 position;
private Texture2D spriteImage;
private float spriteWidth;
private Vector2 velocity;
```

### Calculated Properties
Some properties can be calculated based on the private fields:

- A `SpriteHeight` property that calculates the properly scaled height `float` based on the image dimensions and sprite width
- A `PositionRectangle` property that returns a `Rectangle` representing the location of the enemy on the screen

Because they are calculated, these properties should only have `get` accessors, and no `set` accessors.

```cs
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
```

### Construction and Content Loading
Next, create the constructor for the `Enemy` class, and a `LoadContent` method for loading the "Enemy" image asset.

1. Define a `public` constructor for the `Enemy` class that takes in an `ArcadeFlyerGame root` object and a `Vector2 position` object
1. In the body of the constructor, set the `root` and `position` private fields to the parameter values
1. Under those statements, set the `spriteWidth` field to `128.0f`
1. Set the `velocity` field to a new `Vector2` object with `-1.0f` and `5.0f`
    - This means the enemy will move left and down
1. Define a new method named `LoadContent` with a return type of `void` and no parameters
1. In the body of the `LoadContent` method, use `root.Content.Load` to load the "Enemy" asset
1. Under that, set the `spriteImage` field to the loaded "Enemy" asset
1. Back in the constructor, at the bottom of the body, call the `LoadContent` method

```cs
public Enemy(ArcadeFlyerGame root, Vector2 position)
{
    this.root = root;
    this.position = position;
    this.spriteWidth = 128.0f;
    this.velocity = new Vector2(-1.0f, 5.0f);

    LoadContent();
}

public void LoadContent()
{
    this.spriteImage = root.Content.Load<Texture2D>("Enemy");
}
```

### The `Update` Method
To make an enemy move from frame to frame, it will be necessary to update the `position` field. This should happen in a method named `Update`. The enemy should move according to the `velocity` field, and the `velocity` should update so the enemy bounces off the top and bottom of the screen.

1. In the body of the `Enemy` class, define a new method named `Update`
    - The method should have a `void` return type, and it should take in a `GameTime` parameter
1. In the body of the `Update` method, update the `position` field by adding the `velocity` to it: `position += velocity;`
1. Under the `position` update, create an `if` statement
1. In the condition for the `if` statement, check if the enemy is hitting the top or the bottom of the screen. Either of the following:
    - Its Y position is less than `0`
    - Its Y position is greater than the height of the screen minus its own height
1. In the body of the `if` statement, flip the Y velocity by multiplying it by `-1`

Now, the sprite should be able to move and bounce properly!

```cs
public void Update(GameTime gameTime)
{
    position += velocity;

    if (position.Y < 0 || position.Y > (root.ScreenHeight - SpriteHeight))
    {
        velocity.Y *= -1;
    }
}
```

### The `Draw` Method
The last thing the `Enemy` class needs to do is actually draw the enemy on the screen!

1. In the body of the `Enemy` class, define a new method named `Draw`
    - The method should have a `void` return type, and it should take in a `GameTime` parameter and a `SpriteBatch` parameter
1. In the body of the `Draw` method, call the `spriteBatch.Draw` method
    - Pass in the `spriteImage` field, the `PositionRectangle` property, and `Color.White`

That's all that's needed to draw the sprite!

```cs
public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
{
    spriteBatch.Draw(spriteImage, PositionRectangle, Color.White);
}
```

### The `Enemy` Class
The code in the **Enemy.cs** file should look something like this:

```cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Enemy
    {
        private ArcadeFlyerGame root;
        private Vector2 position;
        private Texture2D spriteImage;
        private float spriteWidth;
        private Vector2 velocity;

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

        public Enemy(ArcadeFlyerGame root, Vector2 position)
        {
            this.root = root;
            this.position = position;
            this.spriteWidth = 128.0f;
            this.velocity = new Vector2(-1.0f, 5.0f);

            LoadContent();
        }

        public void LoadContent()
        {
            this.spriteImage = root.Content.Load<Texture2D>("Enemy");
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;

            if (position.Y < 0 || position.Y > (root.ScreenHeight - SpriteHeight))
            {
                velocity.Y *= -1;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteImage, PositionRectangle, Color.White);
        }
    }
}
```

## Using the `Enemy` Class to Create an Enemy
Now that the `Enemy` class has been defined, all that's left is to use it in the `ArcadeFlyerGame` class to make an enemy in the game!

1. Open the **ArcadeFlyerGame.cs** file
1. In the body of the `ArcadeFlyerGame` class, add a private field named `enemy` of type `Enemy`
1. In the body of the `ArcadeFlyerGame` constructor, initialize a new `Enemy` object
1. Pass in `this`, and a new `Vector2` object for the position to make it appear on the right side of the screen:  
    ```cs
    enemy = new Enemy(this, new Vector2(screenWidth, 0));
    ```
1. In the `Update` method, call the `Enemy`'s `Update` method on the `enemy` field
1. In the `Draw` method, call the `Enemy`'s `Draw` method on the `enemy` field

Run the game, and the new enemy should appear! It should move to the left, and bounce off the bottom and top of the screen.

## Final Code
The final code for this walkthrough is available on [GitHub](https://github.com/hylandtechoutreach/ArcadeFlyer/tree/Part3Start).