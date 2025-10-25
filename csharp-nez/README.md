# Halloween Candy Dash - C# MonoGame Implementation

A Halloween-themed arcade game built with **MonoGame** - a free, open-source cross-platform game framework.

## About MonoGame

**MonoGame** is the spiritual successor to Microsoft's XNA Framework. It's a powerful, mature framework for creating cross-platform games with C#.

**Features:**
- Cross-platform (Windows, macOS, Linux, iOS, Android, consoles)
- Hardware-accelerated 2D and 3D graphics
- Professional framework used by many commercial games
- Free and open-source (MIT license)

## Prerequisites

To build and run this game, you'll need:

1. **.NET 9.0 SDK or later**
   - Download from: https://dotnet.microsoft.com/download
   - Verify installation: `dotnet --version`

2. **IDE (Optional but recommended)**
   - **Visual Studio 2022** (Windows/Mac) - Free Community Edition
   - **Visual Studio Code** with C# extension
   - **JetBrains Rider**

## How to Build and Run

### Command Line

```bash
# Navigate to the csharp-nez directory
cd csharp-nez

# Restore NuGet packages
dotnet restore

# Build and run the project
dotnet run
```

That's it! The game will launch in a new window.

### Visual Studio

1. Open `HalloweenGame.csproj` in Visual Studio
2. Press **F5** or click the **Start** button
3. The game will build and launch automatically

### Visual Studio Code

1. Open the `csharp-nez` folder in VS Code
2. Install the C# extension if you haven't already
3. Press **F5** to run

## Game Controls

- **Arrow Keys** or **WASD**: Move the pumpkin
- **SPACE**: Restart game (when game over)
- **ESC**: Quit game

## Gameplay

- Control the pumpkin character
- Collect falling candy for points (+10 per candy)
- Avoid bats and spiders!
- Difficulty increases every 15 seconds
- Try to get the highest score!

## Project Structure

```
csharp-nez/
├── HalloweenGame.csproj      # Project file
├── Program.cs                # Entry point
├── HalloweenGameMain.cs      # Main game class (all game logic)
├── images/                   # Sprite images
├── sounds/                   # Sound effects
└── music/                    # Background music
```

## Technical Details

### Architecture

This is a **simple, single-file MonoGame implementation** - perfect for learning!

All game logic is in `HalloweenGameMain.cs`:
- **LoadContent()**: Loads all sprites from images folder
- **Update()**: Handles player movement, spawning, collisions, difficulty
- **Draw()**: Renders background, sprites, and UI

### Key Features

✅ **Direct texture loading** from PNG/JPG files
✅ **Keyboard input** with arrow keys and WASD
✅ **Sprite rendering** with proper centering
✅ **Rectangle collision detection**
✅ **Dynamic spawning** system
✅ **Difficulty scaling** every 15 seconds
✅ **Simple text rendering** (placeholder system)

### Game Objects

Simple data structure for candies and enemies:
```csharp
public class GameObject
{
    public Vector2 Position;
    public string Sprite;
}
```

## Building for Different Platforms

MonoGame can build for multiple platforms:

### Windows
```bash
dotnet publish -c Release -r win-x64 --self-contained
```

### macOS
```bash
dotnet publish -c Release -r osx-x64 --self-contained
```

### Linux
```bash
dotnet publish -c Release -r linux-x64 --self-contained
```

The built executable will be in `bin/Release/net9.0/{runtime}/publish/`

## Troubleshooting

### "dotnet command not found"
- Make sure .NET 9.0 SDK is installed
- Restart your terminal after installation

### Missing textures or black screen
- Ensure the `images/` folder is in the same directory as the executable
- Check console output for error messages
- Verify image files are PNG format (except background.jpg)

### Game runs but sprites don't appear
- Images are being loaded from the working directory
- Make sure you run from the `csharp-nez` folder
- Assets are automatically copied to output directory via `.csproj` rules

## Enhancing the Game

Want to take it further? Here are some ideas:

1. **Add Sound Effects**
   - Use MonoGame's `SoundEffect` and `Song` classes
   - Load .ogg or .wav files for effects and music

2. **Proper Text Rendering**
   - Use MonoGame Content Pipeline to build `.spritefont` files
   - Replace the placeholder `DrawText()` method

3. **Animations**
   - Create sprite sheets for player/enemy animations
   - Use `Rectangle` source rectangles to show different frames

4. **Particle Effects**
   - Create explosion/collection effects when collecting candy

5. **High Score System**
   - Save high scores to a file using `System.IO`

6. **Main Menu**
   - Add different game states (Menu, Playing, GameOver)

## Resources

- **MonoGame Website**: https://www.monogame.net/
- **MonoGame Documentation**: https://docs.monogame.net/
- **MonoGame Tutorials**: https://docs.monogame.net/articles/getting_started/index.html
- **.NET Download**: https://dotnet.microsoft.com/download
- **MonoGame Community**: https://community.monogame.net/

## Why MonoGame?

1. **Industry Standard**: Used by professional game developers
2. **Cross-Platform**: Build once, deploy everywhere
3. **Performance**: Native C# performance with hardware acceleration
4. **Mature**: 10+ years of development and improvements
5. **Free**: MIT licensed, completely open-source
6. **Great for Learning**: Clean API, excellent documentation

**Notable games made with MonoGame:**
- Celeste
- Stardew Valley
- Bastion
- Fez
- And thousands more!

## License

Game code: MIT License
Game assets: CC0 Public Domain (see parent README for credits)
