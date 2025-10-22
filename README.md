# Halloween Game - Multi-Language Implementation

A 2D Halloween-themed arcade game implemented in **Python**, **JavaScript**, **Lua**, and **C#** to showcase cross-platform game development.

## Project Overview

**Game Concept**: Halloween arcade-style game where players control a character, collect items, and avoid obstacles with escalating difficulty.

**Target Use**: GitHub showcase project and LinkedIn talking point demonstrating proficiency across multiple programming languages and game frameworks.

## Technology Stack

| Language | Framework/Engine | Status |
|----------|-----------------|--------|
| Python | Pygame Zero | ✅ Complete & Playable |
| JavaScript | Phaser 3 | ⏳ Planned |
| Lua | LÖVE 2D | ⏳ Planned |
| C# | MonoGame + Nez | ⏳ Planned |

## Project Structure

```
halloween-game/
├── assets/                    # Shared assets across all implementations
│   ├── sprites/              # Character sprites, obstacles, collectibles
│   ├── sounds/               # Sound effects and music
│   └── fonts/                # Game fonts
├── python-pygame-zero/       # Python implementation
├── javascript-phaser/        # JavaScript browser-based version
├── lua-love2d/              # Lua implementation
└── csharp-nez/              # C# implementation with Nez framework
```

## Implementation Strategy

1. ✅ Set up project structure with shared assets
2. ✅ Download and prepare CC0 Halloween sprites (witch, ghost, pumpkin)
3. ✅ Implement Python/Pygame Zero version - **PLAYABLE NOW!**
4. ⏳ Port to Lua/LÖVE 2D
5. ⏳ Create JavaScript/Phaser browser version (for GitHub Pages)
6. ⏳ Implement C#/MonoGame+Nez version
7. ⏳ Create comparison documentation for LinkedIn content

## Core Game Mechanics

- Player-controlled character movement
- Item collection (candy/pumpkins)
- Obstacle avoidance
- Score tracking
- Escalating difficulty
- Halloween theme with spooky effects

## Development Progress

### Completed
- ✅ Created multi-language project structure
- ✅ Set up shared assets directory with sprite management
- ✅ Downloaded CC0 licensed Halloween sprites from OpenGameArt
  - Witch character by TagGames
  - Ice ghost by LetargicDev
  - Pumpkin by Code Inferno Games
- ✅ Completed Python/Pygame Zero implementation
- ✅ Game is playable with full mechanics

### Next Steps
- Add sound effects to Python version
- Port game to Lua/LÖVE 2D
- Create JavaScript/Phaser browser version
- Implement C#/MonoGame+Nez version

## Running the Games

### Python Version (READY TO PLAY!)
```bash
# Install dependencies
pip install -r python-pygame-zero/requirements.txt

# Run the game
cd python-pygame-zero
pgzrun halloween_game.py
```

**Controls:**
- Arrow keys or WASD to move the witch
- Collect pumpkins for points (+10 each)
- Avoid the ghosts!
- Press SPACE to restart after game over
- Difficulty increases every 15 seconds

### JavaScript Version
```bash
cd javascript-phaser
# Instructions TBD
```

### Lua Version
```bash
cd lua-love2d
# Instructions TBD
```

### C# Version
```bash
cd csharp-nez
# Instructions TBD
```

## Assets & Credits

All game sprites are licensed under **CC0 (Creative Commons Zero)** - Public Domain.

See [assets/CREDITS.md](assets/CREDITS.md) for full attribution.

**Sprite Artists:**
- Witch character: TagGames
- Ice Ghost: LetargicDev (Viktor Gorbulin)
- Pumpkin: Code Inferno Games

All assets sourced from [OpenGameArt.org](https://opengameart.org/)

## License

Game code: MIT License (see individual implementation directories)
Game assets: CC0 Public Domain (see assets/CREDITS.md)
