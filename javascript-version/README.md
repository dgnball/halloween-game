# Halloween Candy Dash - JavaScript Version

A spooky arcade game where you collect candy while avoiding bats and spiders!

## How to Run

1. Open `index.html` in a web browser
   - Simply double-click the file, or
   - Right-click and select "Open with" > your preferred browser

   **Note:** Due to browser security restrictions with local files, you may need to run a local server:

   ```bash
   # Using Python 3
   python3 -m http.server 8000

   # Then open http://localhost:8000 in your browser
   ```

   Or use any other local server solution (Live Server extension in VS Code, etc.)

2. Use arrow keys to move your pumpkin
3. Collect candy to increase your score
4. Avoid bats and spiders
5. The difficulty increases every 15 seconds
6. Press SPACE to restart after game over

## Features

- HTML5 Canvas rendering
- Smooth sprite-based graphics
- Progressive difficulty
- Score tracking
- Background image support
- Full sound effects and background music

## Sounds

The game includes all audio:

- `assets/music/halloween.ogg` - Background music (starts when you press your first key)
- `assets/sounds/heal.ogg` - Candy collection sound
- `assets/sounds/scream.ogg` - Game over sound

**Note:** Due to browser autoplay policies, background music will start playing when you press any arrow key to begin playing.

## Game Controls

- **Arrow Keys**: Move the pumpkin
- **Space**: Restart game (when game over)

## Technical Details

- Pure vanilla JavaScript (no frameworks required)
- HTML5 Canvas API for rendering
- 60 FPS game loop using requestAnimationFrame
- Delta time-based movement for smooth gameplay
- Collision detection using AABB (Axis-Aligned Bounding Box)
