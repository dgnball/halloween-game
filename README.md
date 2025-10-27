# Halloween Game - Multi-Language Implementation

A 2D Halloween-themed arcade game implemented in **Python**, **JavaScript**, **Lua**, and **C#** to showcase cross-platform game development.

## Project Overview

**Game Concept**: Halloween arcade-style game where players control a character, collect items, and avoid obstacles with escalating difficulty.

## Technology Stack

| Language   | Framework/Engine |
|------------|------------------|
| Python     | Pygame Zero      |
| JavaScript | HTML5 Canvas     |
| Lua        | LÖVE 2D          |
| C#         | MonoGame         |



## Core Game Mechanics

- Player-controlled character movement
- Item collection
- Obstacle avoidance
- Score tracking
- Escalating difficulty
- Halloween theme with spooky effects

## Running the Games

### Python Version
```bash
# Install dependencies
pip install -r python-pygame-zero/requirements.txt

# Run the game
cd python-pygame-zero
python halloween_game.py
```

### Javascript Version
```bash
cd javascript-version
python3 -m http.server 8000
```
Then open http://localhost:8000 in your browser or go to https://halloween-candy-dash.netlify.app


### Lua Version
```bash
brew install --cask love
love lua-love2d
```

### C# Version
```bash
cd csharp-monogame
dotnet restore
dotnet run
```


**Controls:**
- Arrow keys or WASD to move the witch
- Collect pumpkins for points (+10 each)
- Avoid the ghosts!
- Press SPACE to restart after game over
- Difficulty increases every 15 seconds
