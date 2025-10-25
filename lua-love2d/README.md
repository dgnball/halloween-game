# Halloween Candy Dash - Lua/LÖVE Version

A spooky arcade game where you collect candy while avoiding bats and spiders!

This version is built with **LÖVE (Love2D)** - a popular Lua game framework.

## Prerequisites

You need to install LÖVE (Love2D) to run this game:

- **macOS:** Download from [love2d.org](https://love2d.org/) or install via Homebrew:
  ```bash
  brew install --cask love
  ```

- **Windows:** Download the installer from [love2d.org](https://love2d.org/)

- **Linux (Ubuntu/Debian):**
  ```bash
  sudo add-apt-repository ppa:bartbes/love-stable
  sudo apt-get update
  sudo apt-get install love
  ```

## How to Run

### Method 1: Command Line
```bash
# From the project root directory
love lua-love2d

# Or from inside the lua-love2d directory
cd lua-love2d
love .
```

### Method 2: Drag and Drop (macOS/Windows)
1. Locate the `lua-love2d` folder
2. Drag the folder onto the LÖVE application icon

### Method 3: Create Executable (Optional)
You can package the game as a standalone executable. See the [LÖVE documentation](https://love2d.org/wiki/Game_Distribution) for details.

## Game Controls

- **Arrow Keys:** Move the pumpkin (up, down, left, right)
- **Space:** Restart game (when game over)
- **Escape:** Quit game

## Gameplay

1. Control your pumpkin character using the arrow keys
2. Collect falling candy (chocolate, sweets, jelly babies) to increase your score
3. Avoid the bats and spiders falling from the top
4. The game gets progressively harder - enemies fall faster and spawn more frequently
5. Every 15 seconds, the difficulty level increases
6. Try to get the highest score before game over!

## Features

- **Smooth gameplay** with 60 FPS
- **Progressive difficulty** system
- **Multiple candy sprites** (4 different types)
- **Multiple enemy sprites** (bats and spiders)
- **Score tracking** and level display
- **Background music** (looping Halloween theme)
- **Sound effects** for collecting candy and game over
- **Sprite-based graphics** with proper collision detection

## Project Structure

```
lua-love2d/
├── main.lua              # Main game code
├── conf.lua              # LÖVE configuration
├── README.md             # This file
└── assets/
    ├── images/           # Game sprites
    │   ├── pumpkin.png
    │   ├── bat.png
    │   ├── spider.png
    │   ├── chock1.png
    │   ├── chock2.png
    │   ├── green sweet.png
    │   ├── jbaby.png
    │   └── background.jpg
    ├── sounds/           # Sound effects
    │   ├── heal.ogg
    │   └── scream.ogg
    └── music/            # Background music
        └── halloween.ogg
```

## Technical Details

- **Language:** Lua 5.1+
- **Framework:** LÖVE 11.4 (Love2D)
- **Resolution:** 800x600 (fixed)
- **Frame Rate:** 60 FPS (with VSync)
- **Audio Format:** OGG Vorbis
- **Image Format:** PNG, JPG

## Code Overview

The game uses LÖVE's standard callback functions:

- `love.load()` - Initialize game, load assets
- `love.update(dt)` - Game logic (movement, collisions, spawning)
- `love.draw()` - Render all game elements
- `love.keypressed(key)` - Handle keyboard input

## Learning Resources

If you want to learn more about LÖVE or modify this game:

- [LÖVE Official Documentation](https://love2d.org/wiki/Main_Page)
- [LÖVE Tutorial](https://love2d.org/wiki/Tutorial:Baseline_2D_Platformer)
- [Programming in Lua](https://www.lua.org/pil/)

## Troubleshooting

### "Cannot load game at path" error
Make sure you're running LÖVE from the correct directory or providing the full path to the lua-love2d folder.

### No sound/music playing
- Check that the audio files are in the correct location (`assets/sounds/` and `assets/music/`)
- Ensure your system audio is not muted
- OGG format should be supported on all platforms

### Images not loading
- Verify that all image files are present in `assets/images/`
- Check that filenames match exactly (including spaces and case sensitivity)

## Performance

This game should run smoothly on any system that can run LÖVE, including:
- Older computers (5+ years old)
- Raspberry Pi 3 or newer
- Most laptops and desktops

Expected performance: 60 FPS constant on modern hardware.

## License

Educational project - free to use and modify.

## Credits

- Built with LÖVE (Love2D) game framework
- Ported from the Python/Pygame Zero version
