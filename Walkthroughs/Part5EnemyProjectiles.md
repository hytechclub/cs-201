# Part 5 - Enemy Projectiles
In this walkthrough, add the ability for an `Enemy` object to fire projectiles. This will be similar to how the `Player` fires projectiles, but there will be some refactoring along the way.

## Basic Projectile Firing
Since the concept of projectiles already exists in the game, it won't be too difficult to allow the `Enemy` class to fire them. It will be very similar to the basic `Player` projectile firing, with some different values. Open up the **Enemy.cs** file to get started.

1. Find the `Update` method on the `Enemy` class
1. At the bottom of the `Update` method, create a new `Vector2` object named `projectilePosition`
1. Set the `X` value of `projectilePosition` to be the current `X` position
    - This will make the projectile appear on the left side of the `Enemy` sprite
1. Set the `Y` value of the `projectilePosition` to be the current `Y` position, plus half of the sprite height
    - This will make the projectile appear near the vertical middle of the `Enemy` sprite
1. Under that, create another `Vector2` object named `projectileVelocity`
1. Set the `X` value of `projectileVelocity` to `-5.0f` to make it move to the left
1. Set the `Y` value of `projectileVelocity` to `0.0f` to keep it vertically constant
1. Call `root.FireProjectile` and pass in the position and velocity to fire the projectile!

The code should look something like this:
```cs
Vector2 projectilePosition = new Vector2(position.X, position.Y + SpriteHeight / 2);
Vector2 projectileVelocity = new Vector2(-5.0f, 0.0f);

root.FireProjectile(projectilePosition, projectileVelocity);
```

Run the program to see what happens. The enemy can fire projectiles, but the problem is that they fire constantly! This is the same problem that occurred with the player's projectiles. It would be possible to simply copy the solution from the `Player` class, but there is actually a better way.

## Defining the `Timer` Class
Since both the `Player` and the `Enemy` need a cool down period, it would make sense to create a separate utility that both classes can use: a `Timer` class. In fact, this type of utility could be used for many things in a game (animations, countdowns, etc)!

### Setup
Start with the basic setup for the class.

1. Create a new file named **Timer.cs** in the "src" folder
2. Add `using` statements for `Microsoft.Xna.Framework`
3. Create a `namespace` wrapper for `ArcadeFlyer2D`
4. In the body of the `ArcadeFlyer2D` namespace, define a `class` named `Timer`

```cs
using Microsoft.Xna.Framework;

namespace ArcadeFlyer2D
{
    class Timer
    {

    }
}
```

### Fields and Properties
The fields and properties will be based on the cool down timer fields from the `Player` class:

- `projectileCoolDownTime`
- `projectileTimer`
- `projectileTimerActive`

Since the `Timer` could be used for more than just projectile cool downs, these can be generalized and adapted. Some of the values will need to be accessible, but some can be hidden.

1. In the body of the `Timer` class, add a private `float` field named `totalTime`
    - This will represent the total duration for the timer
1. Under that, add another private `float` field named `timer`
    - This will track the actual current time for the timer
1. Under that, create a new property named `Active`, with an auto-implemented `get` and `private set`
    - This way, the classes that use the timer can check whether the timer is currently active

```cs
private float totalTime;
private float timer;
public bool Active { get; private set; }
```

### Constructor
Define a constructor to initialize new `Timer` objects.

1. In the body of the `Timer` class, define a public constructor for `Timer` objects
1. Add a `float` parameter named `totalTime` to the constructor method signature
1. In the body of the constructor, set the `totalTime` field to the `totalTime` parameter value
1. Under that, set the `timer` field value to `0.0f`
    - This way the timer will start at zero
1. Under that, set the `Active` property to `false`
    - The `Timer` object will not start until it is activated

```cs
public Timer(float totalTime)
{
    this.totalTime = totalTime;
    this.timer = 0.0f;
    this.Active = false;
}
```

### `StartTimer` Method
The `Timer` class should have a way to kick off a new timer process. This will basically be copied from the `Player` class.

1. In the body of the `Timer` class, define a new method named `StartTimer`
    - It should have a `void` return type and no parameters
1. In the body of the `StartTimer` method, set the `Active` property to `true`
    - This means time will be ticking!
1. Under that, set the `timer` field value to `0.0f`
    - This resets the time on the timer

```cs
public void StartTimer()
{
    Active = true;
    timer = 0.0f;
}
```

### `Update` Method
A `Timer` object should update with each new frame, incrementing the time and resetting as needed. This will basically be copied from the `Player` class.

1. In the body of the `Timer` class, define a new method named `Update`
    - It should have a `void` return type and take in a `GameTime` parameter named `gameTime`
1. In the body of the `Update` method, create an `if` statement
1. In the condition for the `if` statement, check the `Active` property to see if the timer is currently running
1. In the body of the `if` statement, the timer is active, so increment the `timer` field by the `TotalSeconds`
    - Use `(float)gameTime.ElapsedGameTime.TotalSeconds` to get the total seconds elapsed
1. Under that, still in the body of the `if` statement, create a new `if` statement
1. In the condition for the new `if` statement, check if the `timer` field value has surpassed the `totalTime` field value
1. In the body of the `if` statement, the timer has completed, so set the `Active` property to `true`

```cs
public void Update(GameTime gameTime)
{
    if (Active)
    {
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (timer >= totalTime)
        {
            Active = false;
        }
    }
}
```

### `Timer` Code
At the end of this section, the code in the **Timer.cs** file should look something like this:

```cs
using Microsoft.Xna.Framework;

namespace ArcadeFlyer2D
{
    class Timer
    {
        private float totalTime;
        private float timer;
        public bool Active { get; private set; }
        
        public Timer(float totalTime)
        {
            this.totalTime = totalTime;
            this.timer = 0.0f;
            this.Active = false;
        }

        public void StartTimer()
        {
            Active = true;
            timer = 0.0f;
        }

        public void Update(GameTime gameTime)
        {
            if (Active)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timer >= totalTime)
                {
                    Active = false;
                }
            }
        }
    }
}
```

## Using the `Timer` Class
Now that the `Timer` class has been created, it's time to use it!

### Refactoring the `Player` Class
First, refactor the `Player` class to use a `Timer` object instead of its own fields. Open up the **Player.cs** file to begin.

1. In the body of the `Player` class, remove the `projectileCoolDownTime`, `projectileTimer`, and `projectileTimerActive` fields
1. In place of the removed fields, add a new private `Timer` field named `projectileCoolDown`
1. In the `Player` constructor, set the `projectileCoolDown` field to a new `Timer` object, passing in `0.5f` to the `Timer` constructor
1. In the body of the `HandleInput` method, find the `if` statement that checks if a projectile should be fired
1. Fix the condition of the `if` statement so that it uses `projectileCoolDown.Active` instead of `projectileTimerActive`
1. At the bottom of the body of that `if` statement, remove the `projectileTimerActive` and `projectileTimer` field setting
1. In place of the removed statements, kick off a new timer with `projectileCoolDown.StartTimer()`
1. In the `Update` method, remove the entirety of the `if (projectileTimerActive)` statement
1. Replace that `if` statement with an update to the `Timer` object: `projectileCoolDown.Update(gameTime)`

The `Timer` class allowed quite a bit of code to be removed from the `Player` class, which is great!

### Updating the `Enemy` Class
Next, use another `Timer` object on the `Enemy` class to limit the number of projectiles fired. Open up the **Enemy.cs** file to begin.

1. In the body of the `Enemy` class, add a new private `Timer` field named `projectileCoolDown`
1. In the `Enemy` constructor, set the `projectileCoolDown` field to a new `Timer` object, passing in `2.0f` to the `Timer` constructor
1. In the body of the `Update` method, find the projectile firing code that creates the two `Vector2` variables and calls the `root.FireProjectile` method
1. Wrap that code in an `if` statement
1. Make the condition for the `if` statement check if the cool down timer is NOT currently active: `!projectileCoolDown.Active`
1. In the body of the `if` statement, after a projectile has been fired, kick off a new timer with `projectileCoolDown.StartTimer()`
1. Outside of the `if` statement, update the `Timer` object with `projectileCoolDown.Update(gameTime)`

Using the `Timer` class made it easier to repeat the cool down functionality from the `Player` class! Run the game to see how the enemy fires projectiles at a slower rate.

## Different Projectile Types
Functionally, the game is working as expected. However, it would be nice to have different projectiles for the player and the enemy. Luckily, this is not too difficult!

### Loading an Enemy Projectile Image Asset
To create new enemy projectiles, it will be necessary to have a new image for the them! Use the following fireball image, or any other image:

![](https://i.imgur.com/13tDtFL.png)

1. Save a new image named **EnemyFire.png** in the "Content" folder
1. Open up the **Content.mgcb** file in the MonoGame Pipeline Tool
1. Click the "Add Existing Item" button
1. Select the **EnemyFire.png** file
1. Click the "Build" button

Now the "EnemyFire" asset should be loadable in the game!

### Storing the Image in the `ArcadeFlyerGame` Class
To make two different types of projectiles, it will be necessary to load in the new image. Open up the **ArcadeFlyerGame.cs** file to get started.

1. In the body of the `ArcadeFlyerGame` class, add a new private `Texture2D` field named `enemyProjectileSprite` to hold the enemy projectile image:  
    ```cs
    private Texture2D enemyProjectileSprite;
    ```
1. In the body of the `LoadContent` method, use `Content.Load<Texture2D>` to load the "EnemyFire" image asset
1. Store the "EnemyFire" image in the `enemyProjectileSprite` field:  
    ```cs
    enemyProjectileSprite = Content.Load<Texture2D>("EnemyFire");
    ```

### Updating the `FireProjectile` Method Definition
Next, make the `FireProjectile` method use a different image depending on the source of the projectile. 

1. Find the `FireProjectile` method in the `ArcadeFlyerGame` class
1. Add a `string` parameter named `projectileType` to the method signature
1. In the body of the `FireProjectile` method, declare (but do not set) a new `Texture2D` variable named `projectileImage`
    - This will store the proper image for the projectile based on the type
1. Create an `if`/`else` statement under that
1. Set the condition of the `if` statement to check if the `projectileType` parameter is equal to `"Player"`
1. In the body of the `if` statement, set the `projectileImage` variable to `playerProjectileSprite`
1. In the body of the `else`, set the `projectileImage` variable to `enemyProjectileSprite`
1. Update the call to the `Projectile` constructor so that it passes in `projectileImage` for the image

```cs
public void FireProjectile(Vector2 position, Vector2 velocity, string projectileType)
{
    Texture2D projectileImage;
    
    if (projectileType == "Player")
    {
        projectileImage = playerProjectileSprite;
    }
    else
    {
        projectileImage = enemyProjectileSprite;
    }

    Projectile firedProjectile = new Projectile(position, velocity, projectileImage);
    projectiles.Add(firedProjectile);
}
```

### Updating the `FireProjectile` Method Calls
At this point, the calls to `FireProjectile` are broken in both the `Player` class and the `Enemy` class. They need to pass in an extra parameter!

1. In the **Player.cs** file, find the call to the `FireProjectile` method
1. Add a third argument to the method call: `"Player"`
1. In the **Enemy.cs** file, find the call to the `FireProjectile` method
1. Add a third argument to the method call: `"Enemy"`

Run the program, and the different types of projectiles should appear!

### The `ProjectileType` Enumeration
The current code works, but there is one slight improvement to be made. Currently, since `FireProjectile` takes in a `string` for the projectile type, any text value could be passed. However, not every text value would make sense as a projectile type; there are only "Player" projectiles and "Enemy" projectiles. One way to constrain the possible values passed is to use an [enumeration](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum).

In C#, **enumerations** are value types that are defined by a set of named constants with symbolic meaning. They are quite useful because they can ensure that variables respect any constraints that are necessary. Create a `ProjectileType` enumeration to use in the `FireProjectile` method.

1. Make a new file named **ProjectileType.cs** in the "src" folder
1. In the new file, add a `namespace` wrapper for `ArcadeFlyer2D`
1. Within the body of the `namespace`, define a new `enum` named `ProjectileType`
1. In the body of the `ProjectileType` enumeration, add `Player` and `Enemy`, separated by a comma (`,`)

```cs
namespace ArcadeFlyer2D
{   
    enum ProjectileType
    {
        Player,
        Enemy
    }
}
```

### Updating the `FireProjectile` Code
Now that the `ProjectileType` enumeration exists, there is no need to use `string` values to represent a type of projectile! Update the `FireProjectile` code to use `ProjectileType` instead of `string`.

1. In the **ArcadeFlyerGame.cs** file, find the `FireProjectile` method
1. In the method signature, change the type of the `projectileType` parameter from `string` to `ProjectileType`
1. In the body of the `FireProjectile` method, find the `if` statement checking if it is a player type
1. Update the `if` condition so that it checks against `ProjectileType.Player` instead of `"Player"`
1. In the **Player.cs**, find the `Update` method
1. In the body of the `Update` method, update the `root.FireProjectile` call to pass in `ProjectileType.Player` instead of `"Player"`
1. In the **Enemy.cs**, find the `Update` method
1. In the body of the `Update` method, update the `root.FireProjectile` call to pass in `ProjectileType.Enemy` instead of `"Enemy"`

Run the code again, and make sure everything works the same as it did before!

## Final Code
The final code for this walkthrough is available on [GitHub](https://github.com/hylandtechoutreach/ArcadeFlyer/tree/Part6Start).