"""
Halloween Game - Pygame Zero Implementation
A spooky arcade game where you collect candy while avoiding ghosts!
"""

import random

from pgzero.actor import Actor

# Game constants
WIDTH = 800
HEIGHT = 600
TITLE = "Halloween Candy Dash"

# Game state
score = 0
game_over = False
difficulty = 1

# Player - will be initialized on first draw
player = None
player_speed = 5

# Collections
candies = []
ghosts = []

# Timers
spawn_timer = 0
difficulty_timer = 0


def init_player():
    """Initialize player actor (called on first draw)"""
    global player
    if player is None:
        player = Actor('player')
        player.pos = (WIDTH // 2, HEIGHT - 100)


def draw():
    """Draw all game elements"""
    init_player()  # Ensure player is initialized
    screen.fill((20, 10, 40))  # Dark purple background

    if not game_over:
        # Draw player
        player.draw()

        # Draw candies
        for candy in candies:
            candy.draw()

        # Draw ghosts
        for ghost in ghosts:
            ghost.draw()

        # Draw score
        screen.draw.text(f"Score: {score}", (10, 10), color="orange", fontsize=36)
        screen.draw.text(f"Level: {difficulty}", (10, 50), color="white", fontsize=30)
    else:
        # Game over screen
        screen.draw.text("GAME OVER!", center=(WIDTH//2, HEIGHT//2 - 50),
                        color="red", fontsize=72)
        screen.draw.text(f"Final Score: {score}", center=(WIDTH//2, HEIGHT//2 + 30),
                        color="orange", fontsize=48)
        screen.draw.text("Press SPACE to restart", center=(WIDTH//2, HEIGHT//2 + 100),
                        color="white", fontsize=32)


def update(dt):
    """Update game state"""
    global spawn_timer, difficulty_timer, game_over, score, difficulty

    if game_over:
        return

    # Player movement
    if keyboard.left and player.x > 25:
        player.x -= player_speed
    if keyboard.right and player.x < WIDTH - 25:
        player.x += player_speed
    if keyboard.up and player.y > 25:
        player.y -= player_speed
    if keyboard.down and player.y < HEIGHT - 25:
        player.y += player_speed

    # Spawn candies and ghosts
    spawn_timer += dt
    if spawn_timer > max(0.5, 2.0 - difficulty * 0.2):
        spawn_timer = 0
        spawn_candy()
        if random.random() < 0.3 + (difficulty * 0.05):  # Increasing ghost spawn rate
            spawn_ghost()

    # Update candies
    for candy in candies[:]:
        candy.y += 2 + difficulty * 0.5
        if candy.y > HEIGHT:
            candies.remove(candy)
        elif player.colliderect(candy):
            candies.remove(candy)
            score += 10
            # Play sound effect (placeholder for when we add sounds)

    # Update ghosts
    for ghost in ghosts[:]:
        ghost.y += 3 + difficulty * 0.5
        if ghost.y > HEIGHT:
            ghosts.remove(ghost)
        elif player.colliderect(ghost):
            game_over = True

    # Increase difficulty over time
    difficulty_timer += dt
    if difficulty_timer > 15:  # Every 15 seconds
        difficulty_timer = 0
        difficulty += 1


def spawn_candy():
    """Spawn a candy at random position"""
    candy = Actor('candy')
    candy.x = random.randint(30, WIDTH - 30)
    candy.y = -20
    candies.append(candy)


def spawn_ghost():
    """Spawn a ghost at random position"""
    ghost = Actor('ghost')
    ghost.x = random.randint(30, WIDTH - 30)
    ghost.y = -20
    ghosts.append(ghost)


def on_key_down(key):
    """Handle key presses"""
    global game_over, score, difficulty, candies, ghosts

    if key == keys.SPACE and game_over:
        # Restart game
        game_over = False
        score = 0
        difficulty = 1
        candies.clear()
        ghosts.clear()
        player.pos = (WIDTH // 2, HEIGHT - 100)
