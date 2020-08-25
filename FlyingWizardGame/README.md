# Flying Wizard Game - MonoGame
This folder contains the the "final" version of the game we plan to build throughout C# 201.

## Setup
The setup is based on [this boilerplate project](https://github.com/andrew-r-king/monogame-vscode-boilerplate). To get the game up and running locally, follow these steps:

1. Download and Install [Visual Studio Code](https://code.visualstudio.com/download)
1. Install the [Official C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) for Visual Studio Code
1. Download and Install [Git](https://git-scm.com/downloads)
1. Clone the [C# 201 GitHub Repository](https://github.com/hytechclub/cs-201.git)
    - In the command line: `git clone https://github.com/hytechclub/cs-201.git`
    - In VS Code: `Ctrl`+`Shift`+`P` to open the Command Palette, then run "Git: Clone" and enter
    - Alternatively, download the code directly without cloning it
1. Download and Install [MonoGame](https://www.monogame.net/downloads/)
1. Download and Install the [.NET Core SDK (3.x)](https://dotnet.microsoft.com/download)
    - Make sure to grab the Core SDK, not the Framework or Runtime 
1. Open VS Code
1. In VS Code, open the [FlyingWizardGame](/) folder
    - It must be this specific folder, not the `cs-201` folder that contains it!
1. Press `F5` to run the game!

If there are any issues with the environment setup, please do not hesitate to reach out to [Joseph Maxwell](joseph.maxwell@hyland.com).

## Project Structure
There are three important folders that make up the **Flying Wizard Game** project:

- The [.vscode](/.vscode) folder contains configuration files that tell VS Code how to build and run the game
- The [Content](/Content) folder contains all the assets for the game, and the [Content.mgcb](Content/Content.mgcb) file that tells MonoGame how to build those assets
- The [src](/src) folder contains all the C# code for the project

## The Source Code
There are several code files that make the game run.

The [FlyingWizardGame.csproj](FlyingWizardGame.csproj) file in the main directory contains everything needed to build the project. It should not be altered unless there is a change in OS, framework, or folder structure.

All of the C# code files are in the [src](/src) folder. These create the actual elements in the game!

### Main Files
- The [Program](src/Program.cs) class is the main entry point for the project. It simply starts the game.
- The [FlyingWizardGame](src/FlyingWizardGame.cs) class contains the entire game. It sets up the screen, and ultimately controls how each element interacts.

### Sprites
- The [Sprite](src/Sprite.cs) class contains all the code shared by any sprite element. This includes drawing, sizing, and animation. The `Player`, `Enemy`, and `Projectile` classes inherit from the `Sprite` class.
- The [Player](src/Player.cs) class contains the code that controls the player. It draws the main character, and handles input.
- The [Enemy](src/Enemy.cs) class contains the code that controls an enemy. It draws the enemy, and handles projectile firing.
- The [Projectile](src/Projectile.cs) class contains the code for a projectile (a fired object). It doesn't do much other than move.

### Other Code
- The [EnemyColumn](src/EnemyColumn.cs) class contains a `List` of `Enemy` objects that move together in a column. It controls each of the enemies it contains.
- The [ProjectileType](src/ProjectileType.cs) enumeration is a type that determines whether a projectile originated from the player, or an enemy.
- The [CoolDown](src/CoolDown.cs) class contains a timer mechanism. This is used for animations, as well as the cool down period for the firing of projectiles.