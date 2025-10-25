# Halloween Candy Dash - JavaScript Version

A spooky arcade game where you collect candy while avoiding bats and spiders!

**Now fully mobile-friendly!** Play on iPhone, Android, or desktop with touch controls and responsive design.

## How to Run

1. Open `index.html` in a web browser
   - **Desktop:** Simply double-click the file or right-click and select "Open with" > your browser
   - **Mobile:** Copy the files to a web server or use a local server (see below)

   **Note:** Due to browser security restrictions with local files, you may need to run a local server:

   ```bash
   # Using Python 3
   python3 -m http.server 8000

   # Then open http://localhost:8000 in your browser
   # On mobile: Open http://YOUR_IP:8000 (e.g., http://192.168.1.100:8000)
   ```

   Or use any other local server solution (Live Server extension in VS Code, etc.)

2. Click "Start Game" button to begin (required for audio on iOS)
3. **Desktop:** Use arrow keys to move
4. **Mobile/Touch:** Touch and drag to move the pumpkin
5. Collect candy to increase your score
6. Avoid bats and spiders
7. The difficulty increases every 15 seconds
8. Tap screen or press SPACE to restart after game over

## Features

- **Cross-platform:** Works on desktop, iPhone, Android, and tablets
- **Responsive design:** Automatically scales to fit any screen size
- **Touch controls:** Smooth touch-to-move gameplay on mobile devices
- **Keyboard controls:** Classic arrow key controls on desktop
- HTML5 Canvas rendering with smooth animations
- Progressive difficulty system
- Score tracking and level display
- Full sound effects and background music
- Sprite-based graphics with preserved aspect ratios

## Mobile Features

### iPhone/iOS Support
- ✅ **Touch controls:** Tap and drag to move
- ✅ **Responsive scaling:** Canvas adapts to screen size (portrait or landscape)
- ✅ **iOS audio workaround:** Start button enables background music
- ✅ **No scrolling:** Touch events prevent page scrolling
- ✅ **Full-screen capable:** Can be added to home screen

### Android Support
- ✅ All mobile features work on Android devices
- ✅ Touch controls with smooth following
- ✅ Responsive design for all screen sizes

## Sounds

The game includes all audio:

- `assets/music/halloween.ogg` - Background music (loops continuously)
- `assets/sounds/heal.ogg` - Candy collection sound
- `assets/sounds/scream.ogg` - Game over sound

**iOS/Mobile Audio:** Tap the "Start Game" button to enable audio. This is required by iOS and most mobile browsers.

## Game Controls

### Desktop
- **Arrow Keys:** Move the pumpkin (up, down, left, right)
- **Space:** Restart game (when game over)

### Mobile/Touch Devices
- **Touch & Drag:** Touch anywhere on the game canvas and the pumpkin will smoothly follow your finger
- **Tap:** Tap the canvas to restart after game over

## Technical Details

- Pure vanilla JavaScript (no frameworks required)
- HTML5 Canvas API for rendering
- Responsive canvas scaling for all device sizes
- 60 FPS game loop using requestAnimationFrame
- Delta time-based movement for smooth gameplay
- Aspect ratio-preserving sprite rendering
- Touch event handling with coordinate transformation
- Collision detection using AABB (Axis-Aligned Bounding Box)
