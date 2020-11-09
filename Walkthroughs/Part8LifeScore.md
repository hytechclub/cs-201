# Part 8 - Life & Score
At this point, the game should feel almost like a real game. The biggest thing missing is a way to track the player's life and score. Do this by creating `life` and `score` fields on the `ArcadeFlyerGame` class, and displaying the information in the game using a `SpriteFont`. All changes will take place in the **ArcadeFlyerGame.cs** class.

## Adding Life
In almost every game, there is some concept of health. In this game, the player should lose a life whenever a projectiles with the sprite.

1. Add a `private int` field named `life` to the `ArcadeFlyerGame` class
1. After the declaration, set the `life` field to `3`  
    ```cs
    private int life = 3;
    ```
1. Find the `Update` method in the `ArcadeFlyerGame` class
1. Find the `if` statement that checks whether the player has collided with an enemy projectile  
    ```cs
    if (!playerProjectile && getCollision(player.PositionRectangle, p.PositionRectangle))
    ```
1. In the body of that `if` statement, decrement the value of `life` by 1  
    ```cs
    life--;
    ```

## Adding Score
Another thing that exists in almost every game is a score. In this game, the player should gain a point whenever an enemy is destroyed.

1. Under the `life` field, add a `private int` field named `score` to the `ArcadeFlyerGame` class
1. After the declaration, set the `score` field to `0`  
    ```cs
    private int score = 0;
    ```
1. Find the `Update` method in the `ArcadeFlyerGame` class
1. Find the `if` statement that checks whether an enemy has collided with a player projectile  
    ```cs
    if (getCollision(enemy.PositionRectangle, p.PositionRectangle))
    ```
1. In the body of that `if` statement, increment the value of `score` by 1  
    ```cs
    score++;
    ```

## Showing the Life & Score
Now the life and score are tracked, but they don't actually show up anywhere yet! To do that, it will be necessary to create a `SpriteFont` [like this](http://rbwhitaker.wikidot.com/monogame-drawing-text-with-spritefonts).

### Creating a `SpriteFont` Asset
Just like all the image assets in the game, fonts are created with the Content Pipeline tool.

1. Open up the **Content.mgcb** file in the MonoGame Pipeline Tool
1. Click the "New Item" button  
    ![](https://i.imgur.com/o9adUKk.png)
1. In the popup, select the "SpriteFont Description (.spritefont)" option
1. Change the name to "Text"
1. Click the "Create" button at the bottom  
    ![](https://i.imgur.com/BC3uJNn.png)
1. When the popup closes, click the "Build" button  
    ![](https://i.imgur.com/lWCNlQm.png)
1. Go back into VS Code, and make sure the **Text.spritefont** file appears in the **Content** directory  
    ![](https://i.imgur.com/me0Hueh.png)

At this point, the **Content.mgcb** file should be updated, and there should be a **Text.xnb** file in the **Content/bin/DesktopGL/** directory.

### Using the `SpriteFont` to Write in the Game
Now that the asset has been created, it will be possible to use it in the game's code.

1. Under the `spriteBatch` field in the `ArcadeFlyerGame` class, add a new `private SpriteFont` field named `textFont`  
    ```cs
    private SpriteFont textFont;
    ```
1. Find the `ArcadeFlyerGame()` constructor
1. Somewhere in the method, initialize the `textFont` field using `Content.Load`  
    ```cs
    textFont = Content.Load<SpriteFont>("Text");
    ```
1. Find the `Draw` method
1. Toward the end of the method, call the `spriteBatch.DrawString` method  
   - The first parameter should be a `SpriteFont` object - use `textFont`
   - The second parameter should be a `string` value - create a string that has both the `life` and `score` values
   - The third parameter should be a `Vector2` object for position - use the `Vector2.Zero` for the upper left
   - The fourth parameter should be a `Color` object - use `Color.Black`
1. Add a semi-colon at the end, and it should be good to go  
    ```cs
    spriteBatch.DrawString(textFont, $"Life: {life}\nScore: {score}", Vector2.Zero, Color.Black);
    ```

At this point, try running the game. The "Life" and "Score" values should update based on what happens in the game!

## Final Code
The final code for this walkthrough is available on [GitHub](https://github.com/hylandtechoutreach/ArcadeFlyer/tree/Part9Start).