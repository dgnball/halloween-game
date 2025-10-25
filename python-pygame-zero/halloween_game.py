"""
Halloween Game - Pygame Zero Implementation
A spooky arcade game where you collect candy while avoiding bats!
"""

import random
from typing import Any

from pgzero.actor import Actor

# Pygame Zero magic globals (injected at runtime by pgzrun)
# Declaring them here prevents linter warnings while not affecting runtime
screen: Any
keyboard: Any
keys: Any
music: Any
sounds: Any

# Game constants
WIDTH = 800
HEIGHT = 600
TITLE = "Halloween Candy Dash"

# Available candy sprites
CANDY_SPRITES = ["chock1", "chock2", "green sweet", "jbaby"]
BADDY_SPRITES = ["bat", "spider"]

# Game state
score = 0
game_over = False
difficulty = 1

# Player - will be initialized on first draw
player = None
player_speed = 5

# Collections
candies = []
bats = []

# Timers
spawn_timer = 0
difficulty_timer = 0

# Track if music has started
music_started = False


def init_player():
    """Initialize player actor (called on first draw)"""
    global player, music_started
    if player is None:
        player = Actor("pumpkin")
        player.pos = (WIDTH // 2, HEIGHT - 100)

    if not music_started:
        music.play("halloween")
        music_started = True


def draw():
    """Draw all game elements"""
    init_player()  # Ensure player is initialized

    screen.blit("background", (0, 0))

    if not game_over:
        # Draw player
        player.draw()

        # Draw candies
        for candy in candies:
            candy.draw()

        # Draw bats
        for bat in bats:
            bat.draw()

        # Draw score
        screen.draw.text(f"Score: {score}", (10, 10), color="orange", fontsize=36)
        screen.draw.text(f"Level: {difficulty}", (10, 50), color="white", fontsize=30)
    else:
        # Game over screen
        screen.draw.text(
            "GAME OVER!",
            center=(WIDTH // 2, HEIGHT // 2 - 50),
            color="red",
            fontsize=72,
        )
        screen.draw.text(
            f"Final Score: {score}",
            center=(WIDTH // 2, HEIGHT // 2 + 30),
            color="orange",
            fontsize=48,
        )
        screen.draw.text(
            "Press SPACE to restart",
            center=(WIDTH // 2, HEIGHT // 2 + 100),
            color="white",
            fontsize=32,
        )


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

    # Spawn candies and bats
    spawn_timer += dt
    if spawn_timer > max(0.5, 2.0 - difficulty * 0.2):
        spawn_timer = 0
        spawn_candy()
        if random.random() < 0.3 + (difficulty * 0.05):  # Increasing bat spawn rate
            spawn_bat()

    # Update candies
    for candy in candies[:]:
        candy.y += 2 + difficulty * 0.5
        if candy.y > HEIGHT:
            candies.remove(candy)
        elif player.colliderect(candy):
            candies.remove(candy)
            score += 10
            sounds.heal.play()

    # Update bats
    for bat in bats[:]:
        bat.y += 3 + difficulty * 0.5
        if bat.y > HEIGHT:
            bats.remove(bat)
        elif player.colliderect(bat):
            sounds.scream.play()
            game_over = True

    # Increase difficulty over time
    difficulty_timer += dt
    if difficulty_timer > 15:  # Every 15 seconds
        difficulty_timer = 0
        difficulty += 1


def spawn_candy():
    """Spawn a candy at random position with random sprite"""
    candy_sprite = random.choice(CANDY_SPRITES)
    candy = Actor(candy_sprite)
    candy.x = random.randint(30, WIDTH - 30)
    candy.y = -20
    candies.append(candy)


def spawn_bat():
    """Spawn a bat at random position"""
    baddy_sprite = random.choice(BADDY_SPRITES)
    baddy = Actor(baddy_sprite)
    baddy.x = random.randint(30, WIDTH - 30)
    baddy.y = -20
    bats.append(baddy)


def on_key_down(key):
    """Handle key presses"""
    global game_over, score, difficulty, candies, bats

    if key == keys.SPACE and game_over:
        # Restart game
        game_over = False
        score = 0
        difficulty = 1
        candies.clear()
        bats.clear()
        player.pos = (WIDTH // 2, HEIGHT - 100)


if __name__ == "__main__":
    import pgzrun

    pgzrun.go()
