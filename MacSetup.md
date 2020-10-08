# Mac Setup
To set up the C# 201 environment on a Mac, it may look a little different than on a Windows computer. Follow the steps in the [Environment Setup](EnvironmentSetup.md) guide, but refer to _this_ guide for some specific steps.

## Installing Git
There are a few different ways to do it; one option is to use [Homebrew](https://brew.sh/). A user must have Administrator privileges to install Git on a Mac; make sure to log in as an admin user, then complete the steps below.

### Installing Homebrew
First, install Homebrew.

1. Open the [Terminal](https://www.howtogeek.com/682770/how-to-open-the-terminal-on-a-mac/) app
1. Copy and paste the command below into the terminal window, and press Enter/Return:  
    ```sh
    /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install.sh)"
    ```
1. Enter your password, and press the Return or Enter key  
    ![](https://i.imgur.com/NnDDnQ2.png)
1. Press the Return or Enter key to continue
1. Wait a few minutes for the process to complete  
    ![](https://i.imgur.com/4mb0CJQ.png)
    ![](https://i.imgur.com/Oe6MQIz.png)

### Installing Git from Homebrew
Once Homebrew has been installed, use it to install Git.

1. In the terminal, enter the command below and press Enter/Return:  
    ```sh
    brew install git
    ```
1. Wait for the process to complete  
    ![](https://i.imgur.com/QZ3WgnU.png)
1. When the process is done, enter the command below and press Enter/Return:  
    ```sh
    git --version
    ```
1. Verify that a version appears  
    ![](https://i.imgur.com/r93jXpd.png)

If the `git --version` command returns a version, that means Git has been installed!

## Installing .NET Core SDK
Next, install the **.NET Core SDK**. This will be necessary to run C# programs.

1. Go to the [.NET Core download page](https://dotnet.microsoft.com/download), and make sure macOS is selected
1. Next to "Build Apps", click on the **Download .NET Core SDK** button  
    ![](https://i.imgur.com/vsfzfvr.png)
1. When the file has downloaded, open it to start the installer  
    ![](https://i.imgur.com/UygpwlI.png)
1. Continue through the installer, keeping all of the default settings
1. Verify that the installation was completed successfully  
    ![](https://i.imgur.com/xCgyLMP.png)
1. Open the Terminal application again
1. Type in `dotnet`, press Enter/Return, and verify that the proper results appear  
    ![](https://i.imgur.com/4RvXPaq.png)

If the `dotnet` command returns the message in the image above, that means the .NET Core SDK has been installed!

## Installing Mono and MonoGame
Next, install Mono and MonoGame. These will be necessary to run the game built throughout the course.

### Installing Mono
First, install Mono.

1. Go to the [Mono download page](https://www.mono-project.com/download/stable/#download-mac), and make sure macOS is selected
1. Click the **Download Mono (Stable channel)** button 
    ![](https://i.imgur.com/a5YnPWf.png)
1. When the file is downloaded, open it to start the installer  
    ![](https://i.imgur.com/IcX2rzt.png)
1. Continue through the installer, keeping all of the default settings
1. Verify that the installation was completed successfully  
    ![](https://i.imgur.com/H5YOHJD.png)

After these steps, Mono should be installed.

### Installing MonoGame
MonoGame is a framework built on top of Mono. Follow the steps below to install it.

1. Open a terminal
1. Copy and run the following commands, one after the other:  
    ```sh
    dotnet new --install MonoGame.Templates.CSharp
    ```
    ```sh
    dotnet tool install --global dotnet-mgcb-editor
    ```
    ```sh
    mgcb-editor --register
    ```

Once the commands complete, MonoGame should be installed.

## Beyond
The rest of the environment setup should be platform independent. Refer back to the main [Environment Setup](EnvironmentSetup.md) guide to continue. By the end, it should be possible to:

- Open Visual Studio Code
- Open the **ArcadeFlyer** folder
- Open the "Run" pane from the left menu
- Click the "Play" button
- See an empty blue screen appear

If all of that works, that means the environment has been setup properly!