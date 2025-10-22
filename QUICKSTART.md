# Quick Start Guide

## What's Ready

The **Python/Pygame Zero** version is fully playable right now!

## Play the Game

```bash
# 1. Install dependencies (one-time setup)
pip install -r python-pygame-zero/requirements.txt

# 2. Run the game
cd python-pygame-zero
pgzrun halloween_game.py
```

## Game Controls

| Key | Action |
|-----|--------|
| Arrow Keys | Move the witch character |
| SPACE | Restart game (when game over) |
| ESC | Quit game |

## Gameplay

- **Goal**: Collect as many pumpkins as possible
- **Scoring**: +10 points per pumpkin
- **Challenge**: Avoid the ghosts or it's game over!
- **Difficulty**: Speeds up every 15 seconds

## What's Next

This is a multi-language showcase project. The Python version is complete, and the following versions are planned:

1. ✅ **Python/Pygame Zero** - PLAYABLE NOW (180 lines)
2. ⏳ **Lua/LÖVE 2D** - Coming soon
3. ⏳ **JavaScript/Phaser 3** - Coming soon (will run in browser)
4. ⏳ **C#/MonoGame+Nez** - Coming soon

## Project Features

- Shared assets across all implementations
- CC0 licensed sprites (no attribution required)
- Minimal code thanks to framework choices
- Perfect for LinkedIn/GitHub portfolio showcase

## Troubleshooting

**Q: Game won't start?**
A: Make sure you're in the `python-pygame-zero` directory when running `pgzrun halloween_game.py`

**Q: Missing pygame?**
A: Run `pip install pgzero pygame` to install dependencies

**Q: Sprites not showing?**
A: The game looks for sprites in the `images/` subdirectory. Make sure the symlinks are intact.

## File Structure

```
halloween-game/
├── README.md              # Main project documentation
├── QUICKSTART.md          # This file
├── assets/
│   ├── sprites/           # Witch, ghost, pumpkin sprites
│   └── CREDITS.md         # Asset attribution
└── python-pygame-zero/
    ├── halloween_game.py  # Main game code
    ├── images/            # Symlinks to sprites
    └── requirements.txt   # Python dependencies
```

## Asset Credits

All sprites are CC0 (Public Domain):
- Witch: TagGames via OpenGameArt.org
- Ghost: LetargicDev via OpenGameArt.org
- Pumpkin: Code Inferno Games via OpenGameArt.org

See `assets/CREDITS.md` for full details.

## Have Fun!

Enjoy the game and feel free to modify the code to learn how it works!
