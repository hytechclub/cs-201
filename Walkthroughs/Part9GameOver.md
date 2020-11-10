# Part 9 - Game Over
The last thing the game is missing is an ending! Update the code so that the game ends when the player runs out of lives. All changes will take place in the **ArcadeFlyerGame.cs** file.

## Tracking Game Over
Add a way to track whether or not the game has ended. If it has, stop the game from running.

1. In the `ArcadeFlyerGame` class, under the `score` field, add a new `private bool` field named `gameOver`
1. Set the value of `gameOver` to be `false` to start  
    ```cs
    private bool gameOver = false;
    ```
1. In the `Update` method, find where the `life` field is decremented
1. Under that, create an `if` statement to check if the `life` field has gone below 1
1. In the body of the `if` statement, set `gameOver` to `true`  
    ```cs
    if (life < 1)
    {
        gameOver = true;
    }
    ```
1. Next, at the very _top_ of the body of the `Update` method, create another `if` statement
1. Make this `if` statement check the `gameOver` field as its condition
1. In the body of the `if` statement, simply `return` from the method so no updates will continue
    ```cs
    if (gameOver)
    {
        return;
    }
    ```

Run the code and see what happens! The game should freeze when the player has zero lives remaining.

## Displaying a Final Message
The program properly freezes when the game ends, but it's not entirely clear what's happening. When the game is over, everything should disappear except a final message near the center of the screen.

1. In the `ArcadeFlyer` class, find the `Draw` method
1. At the very top of this method, create an `if` statement
1. Make the `if` statement check if the game is over
1. In the body of the `if` statement, call `GraphicsDevice.Clear` and pass in an appropriate color
1. Under that, call `spriteBatch.Begin`
1. Next, create a new `Vector2` variable named `textPosition`
1. Set the `textPosition` variable to a new `Vector2` object, passing in `screenWidth / 2` and `screenHeight / 2`
1. Under that, call `spriteBatch.DrawString`, and pass in the following:
    - `textFont` for the `SpriteFont` object
    - A new string displaying a "Game Over" message, along with the final score
    - `textPosition` for the `Vector2` object
    - An appropriate `Color` value
1. Under that, call the `spriteBatch.End` method
1. Finally, under that, `return` early from the method so nothing else is drawn 

### Code
```cs
if (gameOver)
{
    GraphicsDevice.Clear(Color.Black);
    spriteBatch.Begin();

    Vector2 textPosition = new Vector2(screenWidth / 2, screenHeight / 2);
    spriteBatch.DrawString(textFont, $"Game Over :(\nFinal Score: {score}", textPosition, Color.White);

    spriteBatch.End();
    return;
}
```

Run the game again, and it should display a message when the game is over!

## Final Code
The final code for this walkthrough is available on [GitHub](https://github.com/hylandtechoutreach/ArcadeFlyer/tree/FinalBasicGame).

## Next Steps
Beyond this, take a look at the [Enhancement Ideas](../EnhancementIdeas.md) to try to make the game even better!