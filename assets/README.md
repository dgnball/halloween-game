# Game Assets

This directory contains all shared assets used across the different language implementations.

## Directory Structure

```
assets/
├── sprites/          # Character sprites, items, obstacles
├── sounds/          # Sound effects and music
└── fonts/           # Custom fonts for UI
```

## Asset Requirements

### Sprites Needed

#### Player Character
- `player.png` - Player character sprite (suggest: witch, ghost, or pumpkin character)
- Recommended size: 64x64 pixels

#### Collectibles
- `candy.png` - Candy/pumpkin collectible
- Recommended size: 32x32 pixels

#### Obstacles
- `ghost.png` - Ghost enemy sprite
- Recommended size: 48x48 pixels

### Sounds Needed

- `collect.wav` - Sound when collecting candy
- `gameover.wav` - Sound when hitting a ghost
- `background_music.ogg` - Optional background music

### Creating Placeholder Assets

For quick prototyping, we can create simple colored rectangles or use free assets from:
- [OpenGameArt.org](https://opengameart.org/)
- [Kenney.nl](https://kenney.nl/) - Excellent free game assets
- [itch.io](https://itch.io/game-assets/free) - Free game assets

## Current Status

⏳ Assets needed - we can either:
1. Create simple placeholder shapes programmatically
2. Download free assets from the sources above
3. Create simple pixel art sprites

## License Notes

When adding assets, ensure they are:
- Created by you, or
- Free to use with appropriate attribution, or
- Public domain

Document the source and license for each asset.
