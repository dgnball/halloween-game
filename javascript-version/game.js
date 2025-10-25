/**
 * Halloween Game - JavaScript Implementation
 * A spooky arcade game where you collect candy while avoiding bats!
 */

// Game constants
const WIDTH = 800;
const HEIGHT = 600;

// Available candy sprites
const CANDY_SPRITES = ["chock1", "chock2", "green sweet", "jbaby"];
const BADDY_SPRITES = ["bat", "spider"];

// Game state
let score = 0;
let gameOver = false;
let difficulty = 1;
let playerSpeed = 5;

// Collections
let candies = [];
let baddies = [];

// Timers
let spawnTimer = 0;
let difficultyTimer = 0;

// Track if music has started
let musicStarted = false;

// Player object
let player = {
    x: WIDTH / 2,
    y: HEIGHT - 100,
    size: 60, // Display size (will preserve aspect ratio)
    image: null
};

// Keyboard state
const keys = {
    ArrowLeft: false,
    ArrowRight: false,
    ArrowUp: false,
    ArrowDown: false,
    Space: false
};

// Canvas and context
const canvas = document.getElementById('gameCanvas');
const ctx = canvas.getContext('2d');

// Image loading
const images = {};
const imagesToLoad = [
    'pumpkin',
    'bat',
    'spider',
    'chock1',
    'chock2',
    'green sweet',
    'jbaby',
    'background'
];

let imagesLoaded = 0;
let totalImages = imagesToLoad.length;

// Audio placeholders
const audio = {
    backgroundMusic: null,
    candyCollect: null,
    baddyHit: null
};

/**
 * Load all images
 */
function loadImages() {
    imagesToLoad.forEach(name => {
        const img = new Image();
        const fileName = name === 'background' ? 'background.jpg' : `${name}.png`;
        img.src = `assets/images/${fileName}`;

        img.onload = () => {
            imagesLoaded++;
            if (imagesLoaded === totalImages) {
                initGame();
            }
        };

        img.onerror = () => {
            console.error(`Failed to load image: ${fileName}`);
            imagesLoaded++;
            if (imagesLoaded === totalImages) {
                initGame();
            }
        };

        images[name] = img;
    });
}

/**
 * Initialize game
 */
function initGame() {
    player.image = images['pumpkin'];

    // Preload background music (but don't play yet due to browser restrictions)
    if (!audio.backgroundMusic) {
        audio.backgroundMusic = new Audio('assets/music/halloween.ogg');
        audio.backgroundMusic.loop = true;
        audio.backgroundMusic.volume = 0.3;
    }

    // Start game loop
    requestAnimationFrame(gameLoop);
}

/**
 * Start background music (called on first user interaction)
 */
function startMusic() {
    if (!musicStarted && audio.backgroundMusic) {
        audio.backgroundMusic.play().catch(e => {
            console.log('Could not play background music:', e);
        });
        musicStarted = true;
    }
}

/**
 * Spawn a candy at random position
 */
function spawnCandy() {
    const candySprite = CANDY_SPRITES[Math.floor(Math.random() * CANDY_SPRITES.length)];
    const candy = {
        x: Math.random() * (WIDTH - 60) + 30,
        y: -20,
        size: 50,
        image: images[candySprite]
    };
    candies.push(candy);
}

/**
 * Spawn a baddy at random position
 */
function spawnBaddy() {
    const baddySprite = BADDY_SPRITES[Math.floor(Math.random() * BADDY_SPRITES.length)];
    const baddy = {
        x: Math.random() * (WIDTH - 60) + 30,
        y: -20,
        size: 55,
        image: images[baddySprite]
    };
    baddies.push(baddy);
}

/**
 * Get sprite dimensions preserving aspect ratio
 */
function getSpriteDimensions(sprite) {
    if (!sprite.image || !sprite.image.complete) {
        return { width: sprite.size, height: sprite.size };
    }

    const aspectRatio = sprite.image.width / sprite.image.height;
    let width, height;

    if (aspectRatio > 1) {
        // Wider than tall
        width = sprite.size;
        height = sprite.size / aspectRatio;
    } else {
        // Taller than wide or square
        height = sprite.size;
        width = sprite.size * aspectRatio;
    }

    return { width, height };
}

/**
 * Check collision between two sprites
 */
function checkCollision(sprite1, sprite2) {
    const dims1 = getSpriteDimensions(sprite1);
    const dims2 = getSpriteDimensions(sprite2);

    const halfWidth1 = dims1.width / 2;
    const halfHeight1 = dims1.height / 2;
    const halfWidth2 = dims2.width / 2;
    const halfHeight2 = dims2.height / 2;

    return sprite1.x - halfWidth1 < sprite2.x + halfWidth2 &&
           sprite1.x + halfWidth1 > sprite2.x - halfWidth2 &&
           sprite1.y - halfHeight1 < sprite2.y + halfHeight2 &&
           sprite1.y + halfHeight1 > sprite2.y - halfHeight2;
}

/**
 * Update game state
 */
function update(deltaTime) {
    if (gameOver) {
        return;
    }

    // Player movement
    if (keys.ArrowLeft && player.x > 25) {
        player.x -= playerSpeed;
    }
    if (keys.ArrowRight && player.x < WIDTH - 25) {
        player.x += playerSpeed;
    }
    if (keys.ArrowUp && player.y > 25) {
        player.y -= playerSpeed;
    }
    if (keys.ArrowDown && player.y < HEIGHT - 25) {
        player.y += playerSpeed;
    }

    // Spawn candies and baddies
    spawnTimer += deltaTime;
    const spawnInterval = Math.max(0.5, 2.0 - difficulty * 0.2);
    if (spawnTimer > spawnInterval) {
        spawnTimer = 0;
        spawnCandy();
        if (Math.random() < 0.3 + (difficulty * 0.05)) {
            spawnBaddy();
        }
    }

    // Update candies
    for (let i = candies.length - 1; i >= 0; i--) {
        const candy = candies[i];
        candy.y += 2 + difficulty * 0.5;

        if (candy.y > HEIGHT) {
            candies.splice(i, 1);
        } else if (checkCollision(player, candy)) {
            candies.splice(i, 1);
            score += 10;

            // Play candy collection sound
            const collectSound = new Audio('assets/sounds/heal.ogg');
            collectSound.volume = 0.5;
            collectSound.play();
        }
    }

    // Update baddies
    for (let i = baddies.length - 1; i >= 0; i--) {
        const baddy = baddies[i];
        baddy.y += 3 + difficulty * 0.5;

        if (baddy.y > HEIGHT) {
            baddies.splice(i, 1);
        } else if (checkCollision(player, baddy)) {
            // Play baddy hit sound
            const hitSound = new Audio('assets/sounds/scream.ogg');
            hitSound.volume = 0.6;
            hitSound.play();
            gameOver = true;
        }
    }

    // Increase difficulty over time
    difficultyTimer += deltaTime;
    if (difficultyTimer > 15) {
        difficultyTimer = 0;
        difficulty += 1;
    }
}

/**
 * Draw all game elements
 */
function draw() {
    // Draw background
    if (images['background']) {
        ctx.drawImage(images['background'], 0, 0, WIDTH, HEIGHT);
    } else {
        ctx.fillStyle = '#140a28';
        ctx.fillRect(0, 0, WIDTH, HEIGHT);
    }

    if (!gameOver) {
        // Draw player
        if (player.image) {
            const dims = getSpriteDimensions(player);
            ctx.drawImage(player.image, player.x - dims.width / 2, player.y - dims.height / 2, dims.width, dims.height);
        }

        // Draw candies
        candies.forEach(candy => {
            if (candy.image) {
                const dims = getSpriteDimensions(candy);
                ctx.drawImage(candy.image, candy.x - dims.width / 2, candy.y - dims.height / 2, dims.width, dims.height);
            }
        });

        // Draw baddies
        baddies.forEach(baddy => {
            if (baddy.image) {
                const dims = getSpriteDimensions(baddy);
                ctx.drawImage(baddy.image, baddy.x - dims.width / 2, baddy.y - dims.height / 2, dims.width, dims.height);
            }
        });

        // Draw score
        ctx.fillStyle = 'orange';
        ctx.font = '36px Arial';
        ctx.fillText(`Score: ${score}`, 10, 40);

        ctx.fillStyle = 'white';
        ctx.font = '30px Arial';
        ctx.fillText(`Level: ${difficulty}`, 10, 80);
    } else {
        // Game over screen
        ctx.fillStyle = 'red';
        ctx.font = 'bold 72px Arial';
        ctx.textAlign = 'center';
        ctx.fillText('GAME OVER!', WIDTH / 2, HEIGHT / 2 - 50);

        ctx.fillStyle = 'orange';
        ctx.font = '48px Arial';
        ctx.fillText(`Final Score: ${score}`, WIDTH / 2, HEIGHT / 2 + 30);

        ctx.fillStyle = 'white';
        ctx.font = '32px Arial';
        ctx.fillText('Press SPACE to restart', WIDTH / 2, HEIGHT / 2 + 100);

        ctx.textAlign = 'left';
    }
}

/**
 * Game loop
 */
let lastTime = 0;
function gameLoop(currentTime) {
    const deltaTime = (currentTime - lastTime) / 1000; // Convert to seconds
    lastTime = currentTime;

    if (deltaTime < 0.1) { // Cap delta time to prevent huge jumps
        update(deltaTime);
    }
    draw();

    requestAnimationFrame(gameLoop);
}

/**
 * Reset game
 */
function resetGame() {
    gameOver = false;
    score = 0;
    difficulty = 1;
    candies = [];
    baddies = [];
    player.x = WIDTH / 2;
    player.y = HEIGHT - 100;
    spawnTimer = 0;
    difficultyTimer = 0;
}

/**
 * Keyboard event handlers
 */
document.addEventListener('keydown', (e) => {
    // Start music on first keypress (due to browser autoplay restrictions)
    startMusic();

    if (e.code in keys) {
        keys[e.code] = true;
    }

    if (e.code === 'Space' && gameOver) {
        resetGame();
        e.preventDefault();
    }
});

document.addEventListener('keyup', (e) => {
    if (e.code in keys) {
        keys[e.code] = false;
    }
});

// Start loading images
loadImages();
