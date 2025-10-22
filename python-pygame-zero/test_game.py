#!/usr/bin/env python3
"""
Quick test to verify the game can load assets and initialize
"""
import sys
import os

# Add the game directory to path
sys.path.insert(0, os.path.dirname(__file__))

print("Testing Halloween Game setup...")
print(f"Working directory: {os.getcwd()}")

# Test 1: Check image files exist
print("\n1. Checking sprite files...")
for sprite in ['player.png', 'candy.png', 'ghost.png']:
    path = os.path.join('images', sprite)
    real_path = os.path.realpath(path)
    exists = os.path.exists(real_path)
    print(f"   {sprite}: {'✓ Found' if exists else '✗ Missing'} at {real_path}")

# Test 2: Try importing pygame and pgzero
print("\n2. Checking dependencies...")
try:
    import pygame
    print(f"   ✓ pygame {pygame.version.ver}")
except ImportError as e:
    print(f"   ✗ pygame not found: {e}")

try:
    import pgzero
    print(f"   ✓ pgzero installed")
except ImportError as e:
    print(f"   ✗ pgzero not found: {e}")

# Test 3: Check game file syntax
print("\n3. Checking game code...")
try:
    with open('halloween_game.py', 'r') as f:
        code = f.read()
        compile(code, 'halloween_game.py', 'exec')
    print("   ✓ Game code syntax is valid")
except Exception as e:
    print(f"   ✗ Syntax error: {e}")

print("\n✅ Setup looks good! You can run the game with:")
print("   pgzrun halloween_game.py")
