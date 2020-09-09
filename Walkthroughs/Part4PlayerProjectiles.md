# Part 4 - Player Projectiles
In this walkthrough, add the ability for the player to fire a projectile. From an architectural perspective, there will be a `Projectile` class that inherits from the `Sprite` class. The `ArcadeFlyerGame` class will keep track of a `List` of `Projectile` objects. It will create, update, and draw them as necessary. The `Player` class will be able to fire a projectile by communicating with the `ArcadeFlyerGame` class.

## Loading a Projectile Image Asset
To create projectiles, it will be necessary to have an image for the them! Use the following fireball image, or any other image:

![](https://i.imgur.com/qNP92NN.png)

1. Save a new image named **PlayerFire.png** in the "Content" folder
2. Open up the **Content.mgcb** file in the MonoGame Pipeline Tool
3. Click the "Add Existing Item" button
4. Select the **PlayerFire.png** file
5. Click the "Build" button

Now the "PlayerFire" asset should be loadable in the game!

## Creating the `Projectile` Class
The next thing to do is create a `class` that represents a projectile.

### Setup
Start with the basic setup for the class.

1. Create a new file named **Projectile.cs** in the "src" folder
1. Add `using` statements for `Microsoft.Xna.Framework` and `Microsoft.Xna.Framework.Graphics`
1. Create a `namespace` wrapper for `ArcadeFlyer2D`
1. In the body of the `ArcadeFlyer2D` namespace, define a `class` named `Projectile`
1. Use a colon (`:`) to make the `Projectile` class _inherit from_ the `Sprite` class
    - This will provide a lot of the necessary functionality for projectiles out of the box!

```cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Projectile : Sprite
    {

    }
}
```

### Velocity
The projectile will be moving through space, so it will need a velocity. In the body of the `Projectile` class, create a private `Vector2` field for this purpose:

```cs
private Vector2 velocity;
```

### Constructor
The constructor will be used to create `Projectile` objects.

1. In the body of the `Projectile` class, create a public constructor
1. Add a `Vector2` parameter: `position`
1. Add another `Vector2` parameter: `velocity`
1. Add a `Texture2D` parameter: `spriteImage`
1. Use `: base()` after the method signature to call the base `Sprite` constructor, passing in the `position` parameter value
1. In the body of the constructor, set the `velocity` field to the `velocity` parameter value
1. Under that, set the `SpriteWidth` property from the `Sprite` class to `32.0f`
1. Under that, set the `SpriteImage` property from the `Sprite` class to the `spriteImage` parameter value

```cs
public Projectile(Vector2 position, Vector2 velocity, Texture2D spriteImage) : base(position)
{
    this.velocity = velocity;
    this.SpriteWidth = 32.0f;
    this.SpriteImage = spriteImage;
}
```

### An `Update` method
There should be a way for the `Projectile` objects to update from one frame to the next.

1. In the body of the `Projectile` class, define a new public method named `Update`
    - The method should have no parameters, and a return type of `void`
1. In the body of the `Update` method, increment the `position` value by the `velocity`

```cs
public void Update()
{
    position += velocity;
}
```

### `Projectile` Code
At the end of this section, the code in the **Projectile.cs** file should look something like this:

```cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Projectile : Sprite
    {
        private Vector2 velocity;

        public Projectile(Vector2 position, Vector2 velocity, Texture2D spriteImage) : base(position)
        {
            this.velocity = velocity;
            this.SpriteWidth = 32.0f;
            this.SpriteImage = spriteImage;
        }

        public void Update()
        {
            position += velocity;
        }
    }
}
```

## Tracking Projectiles in the Game
Now that `Projectile` objects can be represented in the game, it's time to use them! Open up the **ArcadeFlyerGame.cs** file, and introduce the idea of projectiles.

### Fields
In order to track projectiles, the `ArcadeFlyerGame` class will need some new fields.

1. At the top of the **ArcadeFlyerGame.cs** file, add a `using` statement for `System.Collections.Generic`
    - This will allow the class to utilize the `List` structure
1. In the body of the `ArcadeFlyerGame` class, add a new private field of type `List<Projectile>` named `projectiles`
1. Under the `projectiles` field, add a new private `Texture2D` field named `playerProjectileSprite`
    - This will store the image for the projectile

### Initialization and Loading
Now that the fields exist, it's time to initialize them.

1. At the bottom of the `ArcadeFlyerGame` constructor, set the `projectiles` field to a new empty list:  
    ```cs
    projectiles = new List<Projectile>();
    ```
1. In the `LoadContent` method, use `Content.Load<Texture2D>` to load the "PlayerFire" image asset
1. Store the "PlayerFire" image in the `playerProjectileSprite` field:  
    ```cs
    playerProjectileSprite = Content.Load<Texture2D>("PlayerFire");
    ```

### Updating and Drawing
Each `Projectile` object in the game will need to be updated and drawn each frame. 

1. In the body of the `Update` method, create a new `foreach` loop
1. In the loop, cycle through each `Projectile` object in the `projectiles` list
1. Call the `Projectile`'s `Update` method on each `Projectile` object:  
    ```cs
    foreach (Projectile p in projectiles)
    {
        p.Update();
    }
    ```
1. In the body of the `Draw` method, between the `spriteBatch.Begin` and `spriteBatch.End`, create a new `foreach` loop
1. In the loop, cycle through each `Projectile` object in the `projectiles` list
1. Call the `Sprite`'s `Draw` method on each `Projectile` object, passing in the `gameTime` and `spriteBatch`:  
    ```cs
    foreach (Projectile p in projectiles)
    {
        p.Draw(gameTime, spriteBatch);
    }
    ```

### Firing
The `ArcadeFlyerGame` class should have the ability to fire a projectile. This will involve creating a new `Projectile` object, and adding it to the `projectiles` list.

1. In the body of the `ArcadeFlyerGame` class, define a new public method named `FireProjectile`
1. Make the return type of the `FireProjectile` method `void`
1. Give the `FireProjectile` two `Vector2` parameters: `position` and `velocity`
1. In the body of the `FireProjectile` method, define a `Projectile` object named `fireProjectile`
1. Set the `fireProjectile` variable to be a new `Projectile` object
    - Pass in the `position`, `velocity`, and `playerProjectileSprite` to the `Projectile` constructor
1. Add the newly created `fireProjectile` to the `projectiles` list

```cs
public void FireProjectile(Vector2 position, Vector2 velocity)
{
    Projectile firedProjectile = new Projectile(position, velocity, playerProjectileSprite);
    projectiles.Add(firedProjectile);
}
```

## Firing Projectiles from the `Player` Class
Now that game can properly handle `Projectile` objects, allow the `Player` to fire them! Open up the **Player.cs** file to get started. The `Player` should fire a projectile when the user presses the Space key.

1. At the bottom of the `Update` method, create an `if` statement
1. For the condition of the `if` statement, check if the Space key is pressed
1. In the body of the `if` statement, create a new `Vector2` variable named `projectilePosition`
    - This will be where the projectile is created on the screen
1. For the `X` value of the new `Vector2`, set it to the current `X` position of the sprite, plus the total width of the sprite
    - This will make the projectile appear on the right side of the player sprite
1. For the `Y` value of the new `Vector2`, set it to the current `Y` position of the sprite, plus the sprite's height divided by `2`
    - This will make the projectile appear at the vertical middle of the player sprite
1. Under that, create another new `Vector2` variable named `projectileVelocity`
1. Set the `X` value for the `projectileVelocity` to `10.0f`
    - This will make the projectile move to the right
1. Set the `Y` value for the `projectileVelocity` to `0.0f`
1. Under that, use `root.FireProjectile` to fire the projectile with `projectilePosition` and `projectileVelocity`

The code should look something like this:
```cs
if (currentKeyboardState.IsKeyDown(Keys.Space))
{
    Vector2 projectilePosition = new Vector2(position.X + SpriteWidth, position.Y + SpriteHeight / 2);
    Vector2 projectileVelocity = new Vector2(10.0f, 0.0f);

    root.FireProjectile(projectilePosition, projectileVelocity);
}
```

Run the program and see how it works. The player can fire projectiles, which is great! The only problem is that there is no limit to the number of projectiles that can fire.

## Timed Firing
To make the game a little more interesting, only allow the player to fire projectiles at a certain rate. This is possible using a timer mechanism.

### Fields
There will be a few necessary fields to keep track of the cool down period for the projectile firing. Add the following private fields to the `Player` class:

- A `float` named `projectileCoolDownTime` set to `0.5f`
    - This is the total amount of time (in seconds) the timer will last
- A `float` named `projectileTimer` set to `0.0f`
    - This is the current time (in seconds) for the timer
- A `bool` named `projectileTimerActive` set to `false`
    - This keeps track of whether or not the player is currently cooling down

```cs
private float projectileCoolDownTime = 0.5f;
private float projectileTimer = 0.0f;
private bool projectileTimerActive = false;
```

### Conditional Firing
Next, update the code so that a projectile can only fire if the player is not cooling down.

1. In the body of the `Update` method, find the `if` statement checking if Space is pressed
1. Update the condition so that it checks for BOTH the key press, AND whether the projectile timer is not currently active:  
    ```cs
    if (!projectileTimerActive && currentKeyboardState.IsKeyDown(Keys.Space))
    ```
1. In the body of the `if` statement, at the bottom, set the `projectileTimerActive` field to `true`
    - This will kick off a new cool down process
1. Under that, set the `projectileTimer` field to `0.0f`
    - This will reset the timer

### Timer Updates
For this to work, the timer will have to be updated so that it properly counts down the time! This will require the use of the MonoGame `GameTime` structure. The `GameTime` structure allows developers to track how much time has passed from frame to frame. This is useful for anything in the game that involves time. Check out [this article](http://rbwhitaker.wikidot.com/gametime) for more information.

1. In the body of the `Update` method, at the bottom, create a new `if` statement
1. For the `if` condition, check if `projectileTimerActive` is true
    - This means the timer must be updated
1. In the body of the `if` statement, increment the value of the `projectileTimer` field based on the total seconds from `gameTime`
1. Under that, create another `if` statement
1. In the new `if` statement, check if the `projectileTimer` has reached the total `projectileCoolDownTime` threshold
    - This means the time is up
2. In the body of the new `if` statement, set `projectileTimerActive` to `false` because the timer has completed

```cs
if (projectileTimerActive)
{
    projectileTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

    if (projectileTimer >= projectileCoolDownTime)
    {
        projectileTimerActive = false;
    }
}
```

Run the code again, and this time the player should only be able to fire a new projectile every half-second!

## Final Code
The final code for this walkthrough is available on [GitHub](https://github.com/hylandtechoutreach/ArcadeFlyer/tree/Part5Start).