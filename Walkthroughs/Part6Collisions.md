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

## Sprite Collisions
At long last, it's time to check whether any of the objects in our game collide with each other. One structure for doing this is by modifying our `Sprite` class, to add a method to see if any other sprites collide with it.

Every `Sprite` has a member `Rectangle` named `PositionRectangle`. The `Rectangle` class has a method named `Intersects()`, which takes another `Rectangle` as a parameter, and returns a `bool` indicating if the two rectangles overlap. We can write a "wrapper" for this in our `Sprite` class, to tell us if a `Sprite` intersects with another `Sprite`.

1. In the **Sprite.cs** file, inside the `Sprite` class, add a new method named `Overlaps`
1. Make the `Overlaps` method take a `Sprite` object named `otherSprite` as the parameter
1. Make the `Overlaps` method return a `bool` indicating if the two sprites overlap
1. The `Overlaps` method should call `Intersects` on the `PositionRectangle` of this object, and pass in the `PositionRectangle` of the `otherSprite` as the parameter to `Intersects`

```cs
public bool Overlaps(Sprite otherSprite)
{
    bool doesOverlap = this.PositionRectangle.Intersects(otherSprite.PositionRectangle);
    return doesOverlap;
}
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

if (!playerProjectile && player.Overlaps(p))
{
    projectiles.Remove(p);
}
else if (playerProjectile && enemy.Overlaps(p))
{
    projectiles.Remove(p);
}
```

Run the game, and verify that when the proper projectiles collide with the player or the enemy, they disappear! This is a stepping stone toward destroying enemies and tracking the player's life and score.

## Final Code
The final code for this walkthrough is available on [GitHub](https://github.com/hylandtechoutreach/ArcadeFlyer/tree/Part7Start).