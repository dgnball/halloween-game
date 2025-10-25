--[[
Halloween Game - LÖVE (Love2D) Implementation
A spooky arcade game where you collect candy while avoiding bats!

How to run:
1. Install LÖVE from https://love2d.org/
2. Run: love lua-love2d
   Or drag the lua-love2d folder onto the LÖVE application
--]]

-- Game constants
local WIDTH = 800
local HEIGHT = 600
local TITLE = "Halloween Candy Dash"

-- Available candy and baddy sprites
local CANDY_SPRITES = {"chock1", "chock2", "green sweet", "jbaby"}
local BADDY_SPRITES = {"bat", "spider"}

-- Game state
local score = 0
local gameOver = false
local difficulty = 1
local playerSpeed = 5

-- Player object
local player = {
    x = WIDTH / 2,
    y = HEIGHT - 100,
    width = 50,
    height = 50,
    image = nil
}

-- Collections
local candies = {}
local baddies = {}

-- Timers
local spawnTimer = 0
local difficultyTimer = 0

-- Resources (images and sounds)
local images = {}
local sounds = {}
local backgroundMusic = nil
local musicStarted = false

-- Background image
local backgroundImage = nil

--[[
    Load all game assets
--]]
function love.load()
    -- Set window properties
    love.window.setTitle(TITLE)
    love.window.setMode(WIDTH, HEIGHT, {
        resizable = false,
        vsync = true
    })

    -- Load images
    images.pumpkin = love.graphics.newImage("assets/images/pumpkin.png")
    images.bat = love.graphics.newImage("assets/images/bat.png")
    images.spider = love.graphics.newImage("assets/images/spider.png")
    images.chock1 = love.graphics.newImage("assets/images/chock1.png")
    images.chock2 = love.graphics.newImage("assets/images/chock2.png")
    images["green sweet"] = love.graphics.newImage("assets/images/green sweet.png")
    images.jbaby = love.graphics.newImage("assets/images/jbaby.png")

    -- Load background image
    backgroundImage = love.graphics.newImage("assets/images/background.jpg")

    -- Set player image
    player.image = images.pumpkin

    -- Load sounds (with error handling)
    local success, err = pcall(function()
        sounds.heal = love.audio.newSource("assets/sounds/heal.wav", "static")
        sounds.scream = love.audio.newSource("assets/sounds/scream.ogg", "static")

        -- Load background music
        backgroundMusic = love.audio.newSource("assets/music/halloween.ogg", "stream")
        backgroundMusic:setLooping(true)
        backgroundMusic:setVolume(0.3)

        -- Start music
        if not musicStarted then
            backgroundMusic:play()
            musicStarted = true
        end
    end)

    if not success then
        print("Warning: Could not load audio files - " .. tostring(err))
        print("Game will run without sound")
    end

    -- Set filter mode for crisp pixel art
    love.graphics.setDefaultFilter("nearest", "nearest")
end

--[[
    Update game state
--]]
function love.update(dt)
    if gameOver then
        return
    end

    -- Player movement
    if love.keyboard.isDown("left") and player.x > 25 then
        player.x = player.x - playerSpeed
    end
    if love.keyboard.isDown("right") and player.x < WIDTH - 25 then
        player.x = player.x + playerSpeed
    end
    if love.keyboard.isDown("up") and player.y > 25 then
        player.y = player.y - playerSpeed
    end
    if love.keyboard.isDown("down") and player.y < HEIGHT - 25 then
        player.y = player.y + playerSpeed
    end

    -- Spawn candies and baddies
    spawnTimer = spawnTimer + dt
    local spawnInterval = math.max(0.5, 2.0 - difficulty * 0.2)
    if spawnTimer > spawnInterval then
        spawnTimer = 0
        spawnCandy()
        if math.random() < 0.3 + (difficulty * 0.05) then
            spawnBaddy()
        end
    end

    -- Update candies
    for i = #candies, 1, -1 do
        local candy = candies[i]
        candy.y = candy.y + (2 + difficulty * 0.5)

        if candy.y > HEIGHT then
            table.remove(candies, i)
        elseif checkCollision(player, candy) then
            table.remove(candies, i)
            score = score + 10
            if sounds.heal then sounds.heal:play() end
        end
    end

    -- Update baddies
    for i = #baddies, 1, -1 do
        local baddy = baddies[i]
        baddy.y = baddy.y + (3 + difficulty * 0.5)

        if baddy.y > HEIGHT then
            table.remove(baddies, i)
        elseif checkCollision(player, baddy) then
            if sounds.scream then sounds.scream:play() end
            gameOver = true
        end
    end

    -- Increase difficulty over time
    difficultyTimer = difficultyTimer + dt
    if difficultyTimer > 15 then
        difficultyTimer = 0
        difficulty = difficulty + 1
    end
end

--[[
    Draw all game elements
--]]
function love.draw()
    -- Draw background
    if backgroundImage then
        love.graphics.draw(backgroundImage, 0, 0)
    else
        love.graphics.clear(0.08, 0.04, 0.16)
    end

    if not gameOver then
        -- Draw player
        if player.image then
            local imgWidth = player.image:getWidth()
            local imgHeight = player.image:getHeight()
            love.graphics.draw(player.image, player.x - imgWidth/2, player.y - imgHeight/2)
        end

        -- Draw candies
        for _, candy in ipairs(candies) do
            if candy.image then
                local imgWidth = candy.image:getWidth()
                local imgHeight = candy.image:getHeight()
                love.graphics.draw(candy.image, candy.x - imgWidth/2, candy.y - imgHeight/2)
            end
        end

        -- Draw baddies
        for _, baddy in ipairs(baddies) do
            if baddy.image then
                local imgWidth = baddy.image:getWidth()
                local imgHeight = baddy.image:getHeight()
                love.graphics.draw(baddy.image, baddy.x - imgWidth/2, baddy.y - imgHeight/2)
            end
        end

        -- Draw score
        love.graphics.setColor(1, 0.53, 0) -- Orange
        love.graphics.print("Score: " .. score, 10, 10, 0, 2, 2)

        love.graphics.setColor(1, 1, 1) -- White
        love.graphics.print("Level: " .. difficulty, 10, 60, 0, 1.5, 1.5)
    else
        -- Game over screen
        love.graphics.setColor(1, 0, 0) -- Red
        local gameOverText = "GAME OVER!"
        local font = love.graphics.newFont(72)
        love.graphics.setFont(font)
        local textWidth = font:getWidth(gameOverText)
        love.graphics.print(gameOverText, WIDTH/2 - textWidth/2, HEIGHT/2 - 50)

        love.graphics.setColor(1, 0.53, 0) -- Orange
        font = love.graphics.newFont(48)
        love.graphics.setFont(font)
        local scoreText = "Final Score: " .. score
        textWidth = font:getWidth(scoreText)
        love.graphics.print(scoreText, WIDTH/2 - textWidth/2, HEIGHT/2 + 30)

        love.graphics.setColor(1, 1, 1) -- White
        font = love.graphics.newFont(32)
        love.graphics.setFont(font)
        local restartText = "Press SPACE to restart"
        textWidth = font:getWidth(restartText)
        love.graphics.print(restartText, WIDTH/2 - textWidth/2, HEIGHT/2 + 100)
    end

    -- Reset color
    love.graphics.setColor(1, 1, 1)
end

--[[
    Handle key presses
--]]
function love.keypressed(key)
    if key == "space" and gameOver then
        -- Restart game
        gameOver = false
        score = 0
        difficulty = 1
        candies = {}
        baddies = {}
        player.x = WIDTH / 2
        player.y = HEIGHT - 100
        spawnTimer = 0
        difficultyTimer = 0
    end

    if key == "escape" then
        love.event.quit()
    end
end

--[[
    Spawn a candy at random position with random sprite
--]]
function spawnCandy()
    local candySpriteName = CANDY_SPRITES[math.random(#CANDY_SPRITES)]
    local candy = {
        x = math.random(30, WIDTH - 30),
        y = -20,
        width = 50,
        height = 50,
        image = images[candySpriteName]
    }
    table.insert(candies, candy)
end

--[[
    Spawn a baddy at random position
--]]
function spawnBaddy()
    local baddySpriteName = BADDY_SPRITES[math.random(#BADDY_SPRITES)]
    local baddy = {
        x = math.random(30, WIDTH - 30),
        y = -20,
        width = 55,
        height = 55,
        image = images[baddySpriteName]
    }
    table.insert(baddies, baddy)
end

--[[
    Check collision between two rectangles
--]]
function checkCollision(a, b)
    -- Get image dimensions if available
    local aWidth = a.width or 50
    local aHeight = a.height or 50
    local bWidth = b.width or 50
    local bHeight = b.height or 50

    if a.image then
        aWidth = a.image:getWidth()
        aHeight = a.image:getHeight()
    end
    if b.image then
        bWidth = b.image:getWidth()
        bHeight = b.image:getHeight()
    end

    return a.x - aWidth/2 < b.x + bWidth/2 and
           a.x + aWidth/2 > b.x - bWidth/2 and
           a.y - aHeight/2 < b.y + bHeight/2 and
           a.y + aHeight/2 > b.y - bHeight/2
end
