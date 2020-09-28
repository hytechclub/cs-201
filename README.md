# <span>C# 201:</span> Game Development
Material for the C# 201 Hy-Tech Club Course. This course uses [MonoGame](https://www.monogame.net/) to introduce game development in C# and introduce some C# programming concepts.

## First Session
The first session will be devoted to environment setup, C# review, a course overview, and a quick code-along activity.

### Environment Setup
Hopefully, students will come prepared with their environments. They should have followed [the guide](EnvironmentSetup.md) to set everything up ahead of time.

### <span>C#</span> Review
Hopefully, the students will be well-prepared for the C# 201 course. To gauge their current level, play a quick review game of some sort. This can also serve as an icebreaker activity.

[Kahoot Quiz](https://create.kahoot.it/v2/share/285fd281-8387-4198-bc25-ac0f6cc6b73e)

### Course Overview
Briefly show the final game in action to show the students what the course is building toward. Additionally, make sure to discuss any housekeeping notes for the course.

### Code-Along
Complete the [Hello World](Walkthroughs/Part0HelloWorld.md) activity with the students. This will serve as an introduction to MonoGame.

## New Concepts
Review the new concepts on the [Reference](Reference.md) page.

- MonoGame stuff (content, basic methods, screen, drawing, time, keyboard input)
- Velocity + Acceleration math
- `Texture2D` objects
- `Vector` objects
- `Rectangle` objects
- Properties
- Inheritance
    - Virtual/Override Methods
- Enumerations

## Two Options
- Self-paced
- Group-paced

## Parts
There will be a series of code-along activities, each building on the last. For the starting point of each activity (other than the "Hello World" activity), there will be a branch in the student-facing GitHub repository.

- [Environment setup](EnvironmentSetup.md)
- ["Hello World"](Walkthroughs/Part0HelloWorld.md) - make the player appear, loading in a `Texture2D` object, representing its position with a `Rectangle`
- [Create the `Player` class and control the player's movement](Walkthroughs/Part1PlayerClass.md)
- [Create the `Enemy` class](Walkthroughs/Part2EnemyClass.md)
- [Create the `Sprite` class and make the `Player` and `Enemy` classes inherit from the `Sprite` class](Walkthroughs/Part3SpriteClass.md)
- [Create the `Projectile` class and let the player fire projectiles](Walkthroughs/Part4PlayerProjectiles.md)
- [Allow `Enemy` objects to fire projectiles](Walkthroughs/Part5EnemyProjectiles.md)
- [Handle collisions between projectiles](Walkthroughs/Part6Collisions.md)
- [Make `Enemy` objects disappear and regenerate](Walkthroughs/Part7EnemyGeneration.md)
- [Create the life count / score](Walkthroughs/Part8LifeScore.md)
- [Game Over](Walkthroughs/Part9GameOver.md)
- Create the screen cover
- Create the EnemyColumn class
- Create the divider line
- Add animations
- Make smoother movement

### Bonus Part: Piskel
At any point during the semester, walk through the [Piskel Lesson](PiskelLesson.md) to show the students how they can create their own sprite images.

### Starting/Ending Points for Walkthroughs
All of the walkthrough code is available in [this GitHub Repository](https://github.com/hylandtechoutreach/ArcadeFlyer/). To see the difference from one part to another, utilize GitHub's [diff tool](https://github.com/hylandtechoutreach/ArcadeFlyer/compare/Part1Start...Part2Start). Select the part to start and the part to end to see the changes.