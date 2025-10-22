# Halloween Game - Python/Pygame Zero

## About Pygame Zero

Pygame Zero is a beginner-friendly game framework that removes boilerplate code, allowing you to focus on game logic. It's built on top of Pygame and provides automatic game loop management, built-in Actor system, and simple resource loading.

## Installation

```bash
pip install pgzero
```

## Project Structure

```
python-pygame-zero/
├── halloween_game.py       # Main game file
└── README.md              # This file
```

The game will look for assets in the parent `assets/` directory using Pygame Zero's conventions:
- `../assets/sprites/` → Pygame Zero's `images/` directory
- `../assets/sounds/` → Pygame Zero's `sounds/` directory

## Running the Game

```bash
cd python-pygame-zero
pgzrun halloween_game.py
```

## Game Controls

- **Arrow Keys / WASD**: Move player
- **SPACE**: Restart (when game over)
- **ESC**: Quit

## Current Status

### Implemented
- Basic game loop and structure
- Player movement (keyboard controls)
- Candy spawning and collection
- Ghost spawning and collision detection
- Score tracking
- Difficulty escalation over time
- Game over and restart functionality

### TODO
- Create/add placeholder sprite assets
- Add sound effects
- Add particle effects for collections
- Add background graphics
- Polish UI/HUD

## Game Mechanics

- **Objective**: Collect as many candies as possible while avoiding ghosts
- **Scoring**: +10 points per candy collected
- **Difficulty**: Increases every 15 seconds, making items fall faster and ghosts spawn more frequently
- **Game Over**: Collision with any ghost ends the game

## Dependencies

- Python 3.7+
- pgzero (Pygame Zero)

## Notes

This implementation uses minimal code thanks to Pygame Zero's conventions:
- No explicit game loop needed
- `draw()` and `update()` functions are automatically called
- Actor collision detection built-in
- Simple keyboard input handling
