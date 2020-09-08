# Part 2 - The `Sprite` Class
In this walkthrough, refactor the code a bit to make it more maintainable and extendable.

## Motivation
Take a look at the code in the `Player` class and the `Enemy` class. A lot of it looks pretty similar, right? Several parts of the `Player` class (fields, properties, and methods) are identical to the `Enemy` class. Wouldn't it be nice if there were a way to store that repeated code somewhere else, and have both the `Player` and the `Enemy` class access it? Well there is!

### Inheritance
In C#, [inheritance](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/inheritance) enables developers to create new classes that reuse, extend, and modify the behavior defined in other classes. The **base class** is a class that contains some code for reuse, and the **derived class** takes that code for itself. These classes follow an "is-a" relationship pattern: an instance of the derived class _is an_ instance of the base class.

For this game, the `Player` class and the `Enemy` class share a lot of code. This code all fits with the idea of a sprite. So, `Player` objects and `Enemy` objects are both examples of _sprites_. It would make sense to define a new class named `Sprite`, and have the `Player` and `Enemy` classes inherit from `Sprite`!

Note that this will not change the actually functionality of the game; it will simply make it easier to extend and maintain the game in the future.

## Defining the `Sprite` Class
The first step to introduce some inheritance will be to define a base class: `Sprite`.

### Setup
Fill out the basic code needed to define the `Sprite` class.

1. Create a new file named **Enemy.cs** in the "src" folder
2. Add `using` statements for `Microsoft.Xna.Framework` and `Microsoft.Xna.Framework.Graphics`
3. Create a `namespace` wrapper for `ArcadeFlyer2D`
4. In the body of the `ArcadeFlyer2D` namespace, define a `class` named `Sprite`

```cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Sprite
    {

    }
}
```

### Fields
There are three important fields that are shared by the `Player` class and the `Enemy` class. The `Sprite` class should have those fields too! Copy over the `position`, `spriteImage`, and `spriteWidth` fields into the body of the `Sprite` class:

```cs
private Vector2 position;
private Texture2D spriteImage;
private float spriteWidth;
```

### Calculated Properties
In addition to the fields, there are two calculated properties that are identical on the `Player` and the `Enemy` class. Copy over the `SpriteHeight` and `PositionRectangle` properties into the body of the `Sprite` class:

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

### Constructor
Although the `Player` and `Enemy` constructors are not identical, it will be necessary to construct `Sprite` objects and initialize some values. Define a new constructor in the body of the `Sprite` class that should take in a `Vector2` value, and set the `position` field based on the value:

```cs
public Sprite(Vector2 position)
{
    this.position = position;
}
```

### Drawing
One other thing that is identical in the `Player` and `Enemy` classes is the `Draw` method. Copy that over into the body of the `Sprite` class:

```cs
public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
{
    spriteBatch.Draw(spriteImage, PositionRectangle, Color.White);
}
```

### `Enemy` Class Code
At this point, the code in the **Enemy.cs** file should look something like this:

```cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Sprite
    {
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
        
        public Sprite(Vector2 position)
        {
            this.position = position;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteImage, PositionRectangle, Color.White);
        }
    }
}
```

## Updating the `Player` Class
Now that the `Sprite` class has been created, it's time to use it! Make the `Player` class inherit from the `Sprite` class by adding `: Sprite` at the end of the class declaration:

```cs
class Player : Sprite
{
    // Player code
}
```

Next, it is necessary to call the `base` constructor from the `Player` constructor. Add `: base()` after the method signature for the constructor, and pass in the `position` value:

```cs
public Player(ArcadeFlyerGame root, Vector2 position) : base(position)
{
    // Constructor code
}
```

After that comes the fun part: remove all of the repeated code from the `Player` class! This includes:

- `position` field
- `spriteImage` field
- `spriteWidth` field
- `SpriteHeight` property
- `PositionRectangle` property
- `Draw` method

Note that there will be some errors in the `Player` class at this point; those will be handled later.

## Updating the `Enemy` Class
The `Enemy` class should be updated in the same way as the `Player` class! Make `Enemy` inherit from `Sprite`, call the `base` constructor from the `Enemy` constructor, and remove all the repeated code!

## Fixing the Access Issues
Now everything is almost ready, but there is one small issue. When the `Player` class and the `Enemy` class attempt to access the fields from the `Sprite` class, there is an inaccessibility issue. Hovering over the error should display more information. In C#, [access modifiers](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers) define which parts of the code are accessible from which other parts. In general, it is helpful to hide things that should not be accessible, and show things that should be.

Rather than simply making the fields on the `Sprite` class `public`, a common way to fix this issue is to add properties for the fields. This way, if anything changes with the implementation of the `Sprite` class, the code that calls the property will not have to change; it will be easier to update the `Sprite` class to make it work.

Add `public` properties for each of the following fields:

- `position`
- `spriteImage`
- `spriteWidth`

Additionally, update the access level for the `position` field so that it is `protected` instead. That means that the derived classes (`Player` and `Enemy`) can actually update the properties of the `position` object directly.

It should look something like this in the body of the  `Sprite` class:

```cs
protected Vector2 position;
public Vector2 Position
{
    get { return position; }
    set { position = value; }
}

private Texture2D spriteImage;
public Texture2D SpriteImage
{
    get { return spriteImage; }
    set { spriteImage = value; }
}

private float spriteWidth;
public float SpriteWidth
{
    get { return spriteWidth; }
    set { spriteWidth = value; }
}
```

The last thing to do is update the erroring code in the `Player` and `Enemy` classes so that it uses the new properties instead of directly accessing the fields. The constructors and `LoadContent` methods should both be updated to use `SpriteWidth` and `SpriteImage` instead of `spriteWidth` and `spriteImage`.

## Final Code
The final code for this walkthrough is available on [GitHub](https://github.com/hylandtechoutreach/ArcadeFlyer/tree/Part4Start).