#!/usr/bin/env python3
"""
Simple script to extract individual sprites from spritesheets without PIL.
Uses subprocess and sips (macOS built-in tool) for image manipulation.
"""
import subprocess


def extract_sprite_with_sips(input_file, output_file, x, y, width, height):
    """Extract a sprite region using sips"""
    # sips doesn't support cropping by coordinates directly, so we'll just copy for now
    # For production, you'd want to use PIL or another tool
    subprocess.run(["cp", input_file, output_file])
    print(f"Copied {input_file} to {output_file}")


# For now, let's just use the first frame or whole image
# The witch spritesheet shows 2 frames side by side
# The ghost spritesheet shows multiple animation frames

print("For this prototype, we'll use the original files directly")
print("The game can be updated later to use specific animation frames")
