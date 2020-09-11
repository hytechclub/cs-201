# Part 6 - Collisions
In this walkthrough, add the ability for the game to detect collisions from projectiles.

## Adding a `ProjectileType` on the `Projectile` Class
In order to properly check for collisions, it will be necessary to know the _type_ of each `Projectile` object. If a projectile is coming from the `Player`, it should check for collisions with `Enemy` objects. If a projectile is coming from an `Enemy`, it should check for collisions with the `Player` object.

### Add the Field and Property
First, update the `Projectile` class with a new field and property. Open up the **Projectile.cs** class to get started.

1. In the body of the `Projectile` class, type in "propfull" and press Enter
    - This [code snippet](https://docs.microsoft.com/en-us/visualstudio/ide/visual-csharp-code-snippets?view=vs-2019) creates a template for a new property
1. For the type of the new field/property, enter `ProjectileType`
1. Press Tab to go to the next part of the template; the field name
1. For the field name, enter `projectileType`
1. Press Tab again to move onto the property name, and enter `ProjectileType`

The code should ultimately look something like this:
```cs
private ProjectileType projectileType;
public ProjectileType ProjectileType
{
    get { return projectileType; }
    set { projectileType = value; }
}
```

### Add the Constructor Parameter
Now that there is a `ProjectileType` on the `Projectile` class, it needs to be populated!

1. Find the `Projectile` constructor
1. In the method signature, add a `ProjectileType` parameter named `projectileType`
1. In the body of the constructor, set the `this.projectileType` field to the value of the `projectileType` parameter
1. Open up the **ArcadeFlyerGame.cs** file, and find the `FireProjectile` method
1. Update the call to the `Projectile` constructor, and pass in the `projectileType` parameter value

At this point, there should be no errors in the code. Nothing will have changed, but the new `ProjectileType` property will make it possible to distinguish between projectiles in the game.

## The `getCollision` Method
Next, it's time to actually detect collisions! To do this, define a method named `getCollision` that can detect if two objects overlap. Open up the **ArcadeFlyerGame.cs** file to get started.

### Setup
Start with some basic setup for the method.

1. At the top of the **ArcadeFlyerGame.cs** file, add a `using` statement for `System`
    - This will be necessary for some `Math` utilities
1. In the body of the `ArcadeFlyerGame` class, define a new method named `getCollision`
1. Make the method `private`
    - This is because it will only be used within the `ArcadeFlyerGame` class
1. Make the return type of the method `bool`
1. Give the method two `Rectangle` parameters named `spriteBounds1` and `spriteBounds2`
    - These will represent the locations of the two objects

```cs
private bool getCollision(Rectangle spriteBounds1, Rectangle spriteBounds2)
{

}
```

### Collision Detection Geometry
For two rectangles to collide, they must _overlap_ across both the X and Y axis:

![](https://i.imgur.com/toTo7Di.png)

There are a few different ways to check for this, but for the purposes of this method, the center points of each rectangle will be used. To detect an overlap across the X-axis, the horizontal distance between the center points must be _less_ than the sum of each rectangle's width divided by two.

Take a look at these two examples:

![](https://i.imgur.com/68RsMxF.png)

![](https://i.imgur.com/9ZfHmVb.png)

The same principle holds true for detecting Y-axis overlap. So to detect collisions, the method should find the centers of the rectangles, find the horizontal and vertical distance between the two centers, and check if those distances both are less than the distance required for overlap (A width / 2 + B width / 2).

### Collision Detection Code
Based on the geometry above, it will be possible to write code to detect a collision between the `spriteBounds1` rectangle, and the `spriteBounds2` rectangle.

To get the center points of the rectangles, use the `Center` property which returns a `Point` object. For more information about `Point` objects, check out <a href="https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb198467(v=xnagamestudio.35)">this article</a>. `Point` objects have an `X` and `Y` property representing a position in 2D space.

1. In the body of the `getCollision` method, create two new `Point` variables named `sprite1Center` and `sprite2Center`
1. Set the two `Point` variables to the center points of the corresponding sprite `Rectangle` objects using the `Center` property
1. Under that, create two new `float` variables named `xDistance` and `yDistance`
1. Set the `float` variables to be the absolute value of the horizontal and vertical difference between the two center points
    - Use the `Math.Abs` method to accomplish this
1. Under that, create two more `float` variables named `collisionDistanceX` and `collisionDistanceY`
    - These will represent the distance needed to detect overlap on each axis
1. Set the `collisionDistanceX` variable to the sum of each rectangle's width divided by two
1. Set the `collisionDistanceY` variable to the sum of each rectangle's height divided by two
1. Return a `bool` value that checks if `xDistance` is less than or equal to `collisionDistanceX` AND `yDistance` is less than `collisionDistanceY`

```cs
Point sprite1Center = spriteBounds1.Center;
Point sprite2Center = spriteBounds2.Center;

float xDistance = Math.Abs(sprite1Center.X - sprite2Center.X);
float yDistance = Math.Abs(sprite1Center.Y - sprite2Center.Y);

float collisionDistanceX = (spriteBounds1.Width / 2) + (spriteBounds2.Width / 2);
float collisionDistanceY = (spriteBounds1.Height / 2) + (spriteBounds2.Height / 2);

return xDistance <= collisionDistanceX && yDistance <= collisionDistanceY;
```

## Removing `Projectile` Objects On Collision
Now that it is possible to detect collisions between sprites, it's time to update the game. If an enemy projectile comes in contact with the player, that projectile should be removed. Similarly, if a player projectile comes in contact with an enemy, that projectile should be removed. For now, the collision will not have any other effect (that will come later).

### `for` Loop Setup
In order to remove `Projectile` objects from the `projectiles` list, it will be necessary to loop through each object. However, using a traditional `for` loop or `foreach` loop will not work; when an object is removed from a `List`, it disrupts the flow of the loop. Instead, loop through the list _backwards_ so that removed objects do not interfere with the current position in the list.

1. Find the `Update` method on the `ArcadeFlyerGame` class
1. In the body of the `Update` method, remove the `foreach` loop that loops through the `projectiles` list
1. Create a new `for` loop structure
1. For the initialization, create a new `int i` variable and set it to the `Count` of the `projectiles` list minus `1`
1. For the condition, check if the `i` variable value is greater than or equal to `0`
1. For the increment, decrease the value of the `i` variable by `1` using `i--`
1. In the body of the `for` loop, define a new `Projectile` variable `p`, and set it to the element in the `projectiles` list at `i`
1. Under that, call the `Projectile`'s `Update` method on the `p` variable

```cs
for (int i = projectiles.Count - 1; i >= 0; i--)
{
    Projectile p = projectiles[i];
    p.Update();
}
```

### `for` Loop Body
Now that the `for` loop will properly loop through each `Projectile` object, it's time to remove the ones that collide!

1. Declare a new `bool` variable named `playerProjectile`
1. Set the value of the `playerProjectile` variable to check if the current `Projectile` object `p` has a `ProjectileType` of `ProjectileType.Player`
1. Under that, create an `if` statement
1. In the condition for the `if` statement, check if `playerProjectile` is false AND there is a collision between the `player` and the current projectile `p`
1. In the body of the `if` statement, remove the current `Projectile` object `p` from the `projectiles` list with `projectiles.Remove(p)`
1. Under the `if`, create an `else if`
1. In the condition for the `else if` statement, check if `playerProjectile` is true AND there is a collision between the `enemy` and the current projectile `p`
1. In the body of the `else if`, remove the current `Projectile` object `p` from the `projectiles` list with `projectiles.Remove(p)`

```cs
bool playerProjectile = p.ProjectileType == ProjectileType.Player;

if (!playerProjectile && getCollision(player.PositionRectangle, p.PositionRectangle))
{
    projectiles.Remove(p);
}
else if (playerProjectile && getCollision(enemy.PositionRectangle, p.PositionRectangle))
{
    projectiles.Remove(p);
}
```

Run the game, and verify that when the proper projectiles collide with the player or the enemy, they disappear! This is a stepping stone toward destroying enemies and tracking the player's life and score.

## Final Code
The final code for this walkthrough is available on [GitHub](https://github.com/hylandtechoutreach/ArcadeFlyer/tree/Part7Start).