# <span>C# 201:</span> Game Development
Material for the C# 201 Hy-Tech Club Course. This course uses [MonoGame](https://www.monogame.net/) to introduce game development in C# and introduce some C# programming concepts.

## First Session
The first session will be devoted to environment setup, C# review, a course overview, and a quick code-along activity.

### Environment Setup
Hopefully, students will come prepared with their environments. They should have followed [the guide](EnvironmentSetup.md) to set everything up ahead of time.

### <span>C#</span> Review
Hopefully, the students will be well-prepared for the C# 201 course. To gauge their current level, play a quick review game of some sort. This can also serve as an icebreaker activity.

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
There will be a series of code-along activities, each building on the last. There will be written guides as well as videos. For the starting point of each activity (other than the "Hello World" activity), there will be a branch in the student-facing GitHub repository.

- Environment setup
- "Hello World" - make the player appear, loading in a Texture2D object, representing its position with a Vector and Rectangle
- Create the Player class
- Control the Player's movement
- Create the Enemy class
- Create the Sprite class
- Make the Player and Enemy classes inherit from the Sprite class
- Create the Projectile class
- Create the CoolDown class
- Allow the Player and Enemy to fire Projectiles
- Update the Enemy sprite image with Piskel
- Handle collisions between projectiles
    - Create the ProjectileType enum
- Create the EnemyColumn class
- Create the life count / score
- Create the screen cover
- Create the divider line
- Add animations
- Make smoother movement