# Part 7 - Multiple Enemies
In this walkthrough, update the game so that there are multiple enemies, and they can all be destroyed. All changes for this part will take place in the **ArcadeFlyerGame.cs** file.

## A `List` of `Enemy` Objects
The first thing to do is make sure the `ArcadeFlyerGame` class is able to handle multiple enemies. Currently, there is one `Enemy` field that creates the single enemy in the game. Update it so it uses a `List<Enemy>` field instead. Note that this is an example of _refactoring;_ the game should function the same, but it will be much easier to maintain and update!

### Creating the List
Make the changes in the **ArcadeFlyerGame.cs** file.

1. Find the `private Enemy enemy` field and remove it
1. Instead, add a `private List<Enemy>` field to the `ArcadeFlyerGame` class named `enemies`  
    ```cs
    private List<Enemy> enemies;
    ```
1. In the `ArcadeFlyerGame()` constructor, find where the `enemy` field was set
1. Instead of doing this, initialize the `enemies` field to a new empty list of type `Enemy`  
    ```cs
    enemies = new List<Enemy>();
    ```
1. On the next line, add the original `new Enemy` object from before to the `enemies` list  
    ```cs
    enemies.Add(new Enemy(this, new Vector2(screenWidth, 0)));
    ```

### Fixing the `Update` and `Draw` Methods
At this point, there should be some errors in the **ArcadeFlyerGame.cs** file. This is because the class still references the `enemy` field. Fix the errors in the `Update` and `Draw` methods so that they properly handle the `enemies` field instead.

1. In the `Update` method, find the `enemy.Update(gameTime)` method call
2. Remove that code, and instead create a `foreach` loop
3. Set the loop to iterate through each `Enemy` object in the `enemies` list
4. In the body of the `foreach` loop, call `Update` on the current enemy object
    ```cs
    foreach (Enemy enemy in enemies)
    {
        enemy.Update(gameTime);
    }
    ```
5. In the `Draw` method, find the `enemy.Draw(gameTime, spriteBatch)` method call
6. Remove that code, and instead create a `foreach` loop
7. Set the loop to iterate through each `Enemy` object in the `enemies` list
8. In the body of the `foreach` loop, call `Draw` on the current enemy object
    ```cs
    foreach (Enemy enemy in enemies)
    {
        enemy.Update(gameTime, spriteBatch);
    }
    ```

### Fixing the Collision Detection
Previously, the game only needed to detect collisions for one enemy. Now, since there are multiple enemies, the code will have to handle collisions for all of them.

1. In the `Update` method, find the collision detection for enemies  
    ```cs
    else if (playerProjectile && getCollision(enemy.PositionRectangle, p.PositionRectangle)) { // ...
    ```
1. Cut the `&&` and everything after it, so that the `else if` only checks for `playerProjectile`  
    ```cs
    else if (playerProjectile) { // ...
    ```
1. In the body of the `else if`, create a `foreach` loop to loop through each enemy  
    ```cs
    foreach (Enemy enemy in enemies) { }
    ```
1. In the body of the `foreach` loop, create an `if` statement checking if there is a collision between the current projectile and the current enemy  
    - Note that this is the same condition that was cut from above
1. In the body of the `if` statement, remove the current projectile `p` from the `projectiles` list  
    ```cs
    projectiles.Remove(p);
    ```

#### Code
```cs
if (!playerProjectile && getCollision(player.PositionRectangle, p.PositionRectangle))
{
    projectiles.Remove(p);
}
else if (playerProjectile))
{
    foreach (Enemy enemy in enemies)
    {
        if (getCollision(enemy.PositionRectangle, p.PositionRectangle))
        {
            projectiles.Remove(p);
        }
    }
}
```

At this point, it should be possible to run the game again! It should function exactly the same as it did before, but now it will be possible to create multiple enemies.

## Enemy Destruction
The next step is to destroy an enemy if a player projectile collides with it. Using the `List<Enemy>`, this is as simple as removing the given enemy from the list.

1. In the `Update` method, find where it loops through the `enemies` list
1. Update the `foreach` loop, and make it a `for` loop instead
1. Make the `for` loop loop through the `enemies` list in _reverse_ order  
    ```cs
    for (int enemyIdx = enemies.Count - 1; enemyIdx >= 0; enemyIdx--) { }
    ```
1. In the body of the `for` loop, create an `Enemy enemy` variable and set it to the enemy at the current index  
    ```cs
    Enemy enemy = enemies[enemyIdx];
    ```
1. Now, within the body of the `if` that checks for collisions, under where it removes the projectile, remove the current enemy from the list  
    ```cs
    enemies.Remove(enemy);
    ```

#### Code
```cs
for (int enemyIdx = enemies.Count - 1; enemyIdx >= 0; enemyIdx--)
{
    Enemy enemy = enemies[enemyIdx];

    if (getCollision(enemy.PositionRectangle, p.PositionRectangle))
    {
        projectiles.Remove(p);
        enemies.Remove(enemy);
    }
}
```

Run the game again. At this point, the enemy should disappear when a projectile collides with it! But having only one enemy is not very fun...

## Enemy Creation
Finally, use a `Timer` to add new `Enemy` objects to the `enemies` list on a regular basis. The goal will be to have a new enemy appear every three seconds.

### Creating and Initializing the `enemyCreationTimer` Field
Create a new `Timer` field, and start the timer.

1. Under the `private List<Enemy> enemies` field, create a `private Timer` field named `enemyCreationTimer`  
    ```cs
    private Timer enemyCreationTimer;
    ```
1. In the `ArcadeFlyerGame()` constructor, set the `enemyCreationTimer` to a new `3.0`-second timer  
    ```cs
    enemyCreationTimer = new Timer(3.0f);
    ```
1. Call the `StartTimer` method on the newly-created `enemyCreationTimer`  
    ```cs
    enemyCreationTimer.StartTimer();
    ```

### Adding New Enemies
Now it's time to add some new enemies! A new enemy should be added every time the `enemyCreationTimer` goes off (i.e. any time the `enemyCreationTimer.Active` value is false).

1. Find the `Update` method, and make some new lines at the bottom
1. Create an `if` statement checking `!enemyCreationTimer.Active`  
    ```cs
    if (!enemyCreationTimer.Active) { }
    ```
1. In the body of the `if` statement, create a new `Enemy` and add it to the `enemies` list  
    - Note that this is just like what happens when the first enemy is created in the constructor!
1. Under that, still within the `if` body, start the timer again  
    ```cs
    enemyCreationTimer.StartTimer();
    ```
1. Under the `if` statement, call the `Update` method on the `enemyCreationTimer`  
    ```cs
    enemyCreationTimer.Update(gameTimer);
    ```

#### Code
```cs
if (!enemyCreationTimer.Active)
{
    enemies.Add(new Enemy(this, new Vector2(screenWidth, 0.0f)));
    enemyCreationTimer.StartTimer();
}

enemyCreationTimer.Update(gameTime);
```

Run the game. At this point, enemies should be destroyed _and_ regenerate every three seconds! Feel free to update the timer values to change the rates of enemy creation and projectile firing.

## Final Code
The final code for this walkthrough is available on [GitHub](https://github.com/hylandtechoutreach/ArcadeFlyer/tree/Part8Start).