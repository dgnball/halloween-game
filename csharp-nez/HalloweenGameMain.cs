using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace HalloweenGame
{
    /// <summary>
    /// Main game class for Halloween Candy Dash
    /// </summary>
    public class HalloweenGameMain : Game
    {
        public const int ScreenWidth = 800;
        public const int ScreenHeight = 600;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Audio
        private SoundEffect collectSound;
        private SoundEffect gameOverSound;
        private Song backgroundMusic;

        // Game state
        private int score = 0;
        private int difficulty = 1;
        private bool gameOver = false;
        private float spawnTimer = 0f;
        private float difficultyTimer = 0f;

        // Textures
        private Texture2D backgroundTexture;
        private Texture2D playerTexture;
        private Dictionary<string, Texture2D> candyTextures = new Dictionary<string, Texture2D>();
        private Dictionary<string, Texture2D> enemyTextures = new Dictionary<string, Texture2D>();

        // Sprite arrays
        private readonly string[] candySprites = { "chock1", "chock2", "green sweet", "jbaby" };
        private readonly string[] enemySprites = { "bat", "spider" };

        // Game objects
        private Vector2 playerPosition;
        private const float PlayerSpeed = 300f;
        private const int SpriteSize = 50;

        private List<GameObject> candies = new List<GameObject>();
        private List<GameObject> enemies = new List<GameObject>();

        public HalloweenGameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "Halloween Candy Dash";
            playerPosition = new Vector2(ScreenWidth / 2, ScreenHeight - 100);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            try
            {
                // Load background
                backgroundTexture = LoadTextureFromFile("images/background.jpg");

                // Load player
                playerTexture = LoadTextureFromFile("images/pumpkin.png");

                // Load candies
                foreach (var sprite in candySprites)
                {
                    candyTextures[sprite] = LoadTextureFromFile($"images/{sprite}.png");
                }

                // Load enemies
                foreach (var sprite in enemySprites)
                {
                    enemyTextures[sprite] = LoadTextureFromFile($"images/{sprite}.png");
                }

                // Load audio
                LoadAudio();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading content: {ex.Message}");
            }
        }

        private void LoadAudio()
        {
            try
            {
                // Load sound effects
                collectSound = LoadSoundEffect("sounds/heal.wav");
                gameOverSound = LoadSoundEffect("sounds/scream.ogg");

                // Load and play background music
                backgroundMusic = LoadSong("music/halloween.ogg");
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.3f;
                MediaPlayer.Play(backgroundMusic);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Warning: Could not load audio - {ex.Message}");
                System.Diagnostics.Debug.WriteLine("Game will run without sound");
            }
        }

        private SoundEffect LoadSoundEffect(string path)
        {
            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                return SoundEffect.FromStream(stream);
            }
        }

        private Song LoadSong(string path)
        {
            // For Song, we need to use Uri with file path
            return Song.FromUri(path, new Uri(System.IO.Path.GetFullPath(path)));
        }

        private Texture2D LoadTextureFromFile(string path)
        {
            using (var fileStream = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                return Texture2D.FromStream(GraphicsDevice, fileStream);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (gameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    RestartGame();
                }
                base.Update(gameTime);
                return;
            }

            // Player movement
            var keyboardState = Keyboard.GetState();
            Vector2 movement = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                movement.X -= 1;
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                movement.X += 1;
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                movement.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                movement.Y += 1;

            if (movement != Vector2.Zero)
            {
                movement.Normalize();
                playerPosition += movement * PlayerSpeed * deltaTime;

                // Clamp to screen
                playerPosition.X = MathHelper.Clamp(playerPosition.X, 25, ScreenWidth - 25);
                playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 25, ScreenHeight - 25);
            }

            // Spawn objects
            spawnTimer += deltaTime;
            float spawnInterval = MathHelper.Max(0.5f, 2.0f - difficulty * 0.2f);

            if (spawnTimer > spawnInterval)
            {
                spawnTimer = 0;
                SpawnCandy();

                if (new Random().NextDouble() < 0.3 + difficulty * 0.05)
                {
                    SpawnEnemy();
                }
            }

            // Update candies
            for (int i = candies.Count - 1; i >= 0; i--)
            {
                candies[i].Position.Y += (100 + difficulty * 25) * deltaTime;

                if (candies[i].Position.Y > ScreenHeight + 50)
                {
                    candies.RemoveAt(i);
                }
                else if (CheckCollision(playerPosition, candies[i].Position, SpriteSize, SpriteSize))
                {
                    score += 10;
                    candies.RemoveAt(i);
                    collectSound?.Play();
                }
            }

            // Update enemies
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Position.Y += (150 + difficulty * 30) * deltaTime;

                if (enemies[i].Position.Y > ScreenHeight + 50)
                {
                    enemies.RemoveAt(i);
                }
                else if (CheckCollision(playerPosition, enemies[i].Position, SpriteSize, 55))
                {
                    gameOver = true;
                    gameOverSound?.Play();
                }
            }

            // Increase difficulty
            difficultyTimer += deltaTime;
            if (difficultyTimer > 15f)
            {
                difficultyTimer = 0;
                difficulty++;
            }

            base.Update(gameTime);
        }

        private void SpawnCandy()
        {
            var random = new Random();
            var sprite = candySprites[random.Next(candySprites.Length)];
            candies.Add(new GameObject
            {
                Position = new Vector2(random.Next(50, ScreenWidth - 50), -20),
                Sprite = sprite
            });
        }

        private void SpawnEnemy()
        {
            var random = new Random();
            var sprite = enemySprites[random.Next(enemySprites.Length)];
            enemies.Add(new GameObject
            {
                Position = new Vector2(random.Next(50, ScreenWidth - 50), -20),
                Sprite = sprite
            });
        }

        private bool CheckCollision(Vector2 pos1, Vector2 pos2, int size1, int size2)
        {
            Rectangle rect1 = new Rectangle((int)(pos1.X - size1 / 2), (int)(pos1.Y - size1 / 2), size1, size1);
            Rectangle rect2 = new Rectangle((int)(pos2.X - size2 / 2), (int)(pos2.Y - size2 / 2), size2, size2);
            return rect1.Intersects(rect2);
        }

        private void RestartGame()
        {
            score = 0;
            difficulty = 1;
            gameOver = false;
            spawnTimer = 0;
            difficultyTimer = 0;
            candies.Clear();
            enemies.Clear();
            playerPosition = new Vector2(ScreenWidth / 2, ScreenHeight - 100);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(20, 10, 40));

            spriteBatch.Begin();

            // Draw background
            if (backgroundTexture != null)
            {
                spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
            }

            if (!gameOver)
            {
                // Draw player
                if (playerTexture != null)
                {
                    DrawCentered(playerTexture, playerPosition, SpriteSize, SpriteSize);
                }

                // Draw candies
                foreach (var candy in candies)
                {
                    if (candyTextures.ContainsKey(candy.Sprite))
                    {
                        DrawCentered(candyTextures[candy.Sprite], candy.Position, SpriteSize, SpriteSize);
                    }
                }

                // Draw enemies
                foreach (var enemy in enemies)
                {
                    if (enemyTextures.ContainsKey(enemy.Sprite))
                    {
                        DrawCentered(enemyTextures[enemy.Sprite], enemy.Position, 55, 55);
                    }
                }

                // Draw UI text (simple rectangles as placeholders since we don't have a font)
                DrawText($"Score: {score}", new Vector2(10, 10), Color.Orange);
                DrawText($"Level: {difficulty}", new Vector2(10, 50), Color.White);
            }
            else
            {
                // Game Over screen
                DrawText("GAME OVER!", new Vector2(ScreenWidth / 2 - 150, ScreenHeight / 2 - 50), Color.Red, 3f);
                DrawText($"Final Score: {score}", new Vector2(ScreenWidth / 2 - 120, ScreenHeight / 2 + 20), Color.Orange, 2f);
                DrawText("Press SPACE to restart", new Vector2(ScreenWidth / 2 - 120, ScreenHeight / 2 + 80), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawCentered(Texture2D texture, Vector2 position, int width, int height)
        {
            spriteBatch.Draw(texture,
                new Rectangle((int)(position.X - width / 2), (int)(position.Y - height / 2), width, height),
                Color.White);
        }

        private void DrawText(string text, Vector2 position, Color color, float scale = 1f)
        {
            // Simple bitmap-style text rendering
            // Each character is rendered as a small rectangle
            int charWidth = (int)(10 * scale);
            int charHeight = (int)(16 * scale);
            int spacing = (int)(2 * scale);

            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c == ' ') continue; // Skip spaces

                // Draw a simple rectangle for each character
                spriteBatch.Draw(pixel,
                    new Rectangle((int)position.X + i * (charWidth + spacing), (int)position.Y, charWidth, charHeight),
                    color * 0.9f); // Slightly transparent for better look
            }

            pixel.Dispose();
        }
    }

    public class GameObject
    {
        public Vector2 Position;
        public string Sprite;
    }
}
