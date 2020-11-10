using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FlyingWizard2D
{
    // The Game itself
    class FlyingWizardGame : Game
    {
        // Graphics Manager
        private GraphicsDeviceManager graphics;

        // Sprite Drawer
        private SpriteBatch spriteBatch;

        // Font for any text on the screen
        private SpriteFont textFont;

        // Keep track of the player's life count
        private int life = 3;

        // Keep track of the player's score
        private int score = 0;

        // The one player
        private Player player;

        // A list of all Enemy Columns in the game
        private List<EnemyColumn> enemyColumns;

        // List of all Projectile objects currently on the screen
        private List<Projectile> projectiles;

        // Sprite image for what the player fires
        private Texture2D playerProjectileSprite;

        // Sprite image for what the enemies fire
        private Texture2D enemyProjectileSprite;

        // Sprite image for the enemy
        private Texture2D enemySprite;

        // Sprite image for the divider line
        private Texture2D dividerLine;

        // Sprite image that covers the entire screen
        private Texture2D screenCover;

        // Text to appear when the screen is covered
        private string screenCoverText;

        // Is the screen currently covered?
        private bool coverScreen;

        // Timer that controls how often Enemy Columns are formed
        private Timer enemyColumnTimer;

        // Preferred width of the screen in pixels
        private int screenWidth = 1600;
        public int ScreenWidth
        {
            get { return screenWidth; }
            private set { screenWidth = value; }
        }

        // Preferred height of the screen in pixels
        private int screenHeight = 900;
        public int ScreenHeight
        {
            get { return screenHeight; }
            private set { screenHeight = value; }
        }

        // 2-coordinate position of the divider between the player and the space
        private Vector2 dividerPosition;

        // X-position for the divider (privately settable, publically gettable)
        public float DividerX
        {
            get
            {
                return dividerPosition.X;
            }

            private set
            {
                dividerPosition.X = value;
            }
        }
        
        // Initalized the game
        public FlyingWizardGame()
        {
            // Get the graphics
            graphics = new GraphicsDeviceManager(this);

            // Set the height and width
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();

            // Set up the directory containing the assets
            Content.RootDirectory = "Content";

            // Make mouse visible
            IsMouseVisible = true;

            // Create the player on the left side of the screen
            player = new Player(this, new Vector2(0, ScreenHeight / 2));

            // Initialize empty projectile list
            projectiles = new List<Projectile>();

            // Set the position of the divider (Player cannot cross)
            dividerPosition = new Vector2(300.0f, 0);

            // Initialize empty list of enemy columns
            enemyColumns = new List<EnemyColumn>();

            // Set the column creation timer (20 seconds)
            enemyColumnTimer = new Timer(20.0f);
        }

        // Create a texture of a rectangle of the given width, height, and color
        private Texture2D createRectangleTexture(int width, int height, Color color)
        {
            // Create the empty rectangle texture
            Texture2D rectangle = new Texture2D(GraphicsDevice, width, height);

            // Fill an array of the proper size with the proper color
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }

            // Set the rectangle data to be the colors
            rectangle.SetData(data);
            return rectangle;
        }

        // Initialize
        protected override void Initialize()
        {
            base.Initialize();
        }

        // Load the content for the game, called automatically on start
        protected override void LoadContent()
        {
            // Create the sprite batch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load textures from Content
            playerProjectileSprite = Content.Load<Texture2D>("fireball");
            enemyProjectileSprite = Content.Load<Texture2D>("bluefireball");
            enemySprite = Content.Load<Texture2D>("MoonBounce");
            textFont = Content.Load<SpriteFont>("Text");

            // Set additional texture as needed
            dividerLine = createRectangleTexture(1, screenHeight, Color.LightGray);
            screenCover = createRectangleTexture(ScreenWidth, ScreenHeight, Color.White);

            // Once content is loaded - cover the screen, and set the screen cover text
            coverScreen = true;
            screenCoverText = "Press Enter to Play";
        }

        // Decrement life count
        private void decrementLife()
        {
            if(--life == 0)
            {
                // If the player has no lives now, end the game
                gameOver();
            }
        }

        // End the game
        private void gameOver()
        {
            // Cover the screen and display the final message
            coverScreen = true;
            screenCoverText = $"Final Score: {score}\nPress Esc to Exit";
        }

        // Called every frame
        protected override void Update(GameTime gameTime)
        {
            // Get the current keyboard state
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // If Esc is pressed, quit the game
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // If the screen is covered...
            if (coverScreen)
            {
                // Uncover the screen if Enter is pressed
                if (currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    coverScreen = false;
                }
                else
                {
                    // Otherwise exit
                    return;
                }
            }

            // If a new enemy column can be created...
            if (!enemyColumnTimer.Active)
            {
                // Create a new enemy column
                EnemyColumn nextColumn = new EnemyColumn(this, 5, 20.0f, 50, 0.5f, 1.0f, 64.0f, enemySprite);

                // Add the column to the list of columns
                enemyColumns.Add(nextColumn);

                // Restart the timer
                enemyColumnTimer.StartTimer();
            }

            // Update the enemy column cool down timer
            enemyColumnTimer.Update(gameTime);

            // Update the player
            player.Update(gameTime);

            // Update each enemy column
            foreach (EnemyColumn ec in enemyColumns)
            {
                ec.Update(gameTime);
            }

            // Loop through projectiles backwards (in order to remove off-screen projectiles)
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                // Update each projectile
                Projectile p = projectiles[i];
                p.Update();

                // Is this a player projectile?
                bool playerProjectile = p.ProjectileType == ProjectileType.Player;

                // Check if the player collides with this non-player projectile
                if (!playerProjectile && player.Overlaps(p))
                {
                    // There is a collision, decrement life and remove the projectile
                    decrementLife();
                    projectiles.Remove(p);

                    // skip to the next projectile
                    continue;
                }
                else if (playerProjectile)
                {
                    // If this projectile is from a player, loop through all the enemies (backwards to remove if necessary)
                    for (int j = enemyColumns.Count - 1; j >= 0; --j)
                    {
                        // Get the current enemy column
                        EnemyColumn enemyColumn = enemyColumns[j];
                        if (enemyColumn.Count == 0)
                        {
                            // If there are no enemies left in this column, remove it and move to the next one
                            enemyColumns.Remove(enemyColumn);
                            continue;
                        }

                        // Loop through all the enemies in the enemy column (backwards to remove if necessary)
                        for (int k = enemyColumn.Count - 1; k >= 0; --k)
                        {
                            // Check if the enemy is colliding with the projectile
                            Enemy currentEnemy = enemyColumn.GetEnemy(k);
                            if (currentEnemy.Overlaps(p))
                            {
                                // Increment the score
                                ++score;

                                // Remove the projectile and the enemy
                                projectiles.Remove(p);
                                enemyColumn.Remove(currentEnemy);
                            }
                        }
                    }
                }

                // If this projectile is off-screen, remove it
                if (p.Position.X + p.SpriteWidth < 0 || p.Position.X > screenWidth)
                {
                    projectiles.Remove(p);
                }
            }
            
            // Update base game
            base.Update(gameTime);
        }

        // Draws some text in the center of the screen
        private void drawStringInCenter(SpriteBatch spriteBatch, SpriteFont textFont, string text, Color color)
        {
            // Get the size of the text to be drawn in the given font
            Vector2 textMeasurements = textFont.MeasureString(screenCoverText);

            // Get the coordinates for the text based on its size and the screen dimensions
            float xPos = (ScreenWidth / 2) - (textMeasurements.X / 2);
            float yPos = (ScreenHeight / 2) - (textMeasurements.Y / 2);

            // Draw the text
            spriteBatch.DrawString(textFont, text, new Vector2(xPos, yPos), color);
        }

        // Draw everything in the game
        protected override void Draw(GameTime gameTime)
        {
            // First clear the screen
            GraphicsDevice.Clear(Color.Black);

            // Kick off the sprite batch process
            spriteBatch.Begin();

            // Draw the player
            player.Draw(gameTime, spriteBatch);

            // Draw each enemy column
            foreach(EnemyColumn ec in enemyColumns)
            {
                ec.Draw(gameTime, spriteBatch);
            }

            // Draw the divider line
            spriteBatch.Draw(dividerLine, dividerPosition, Color.White);

            // Draw each projectile
            foreach (Projectile p in projectiles)
            {
                p.Draw(gameTime, spriteBatch);
            }

            // Draw Life count and Score count
            spriteBatch.DrawString(textFont, $"Life: {life}\nScore: {score}", Vector2.Zero, Color.White);

            // If screen should be covered...
            if (coverScreen)
            {
                // Draw the screen cover on top of everything else
                spriteBatch.Draw(screenCover, Vector2.Zero, Color.White);

                // Draw any text that should go on top of the screen cover
                drawStringInCenter(spriteBatch, textFont, screenCoverText, Color.Black);
            }

            // Close out the sprite batch
            spriteBatch.End();

            // Call the base draw
            base.Draw(gameTime);
        }

        // Fires a projectile with the given position, velocity, and type
        public void FireProjectile(Vector2 position, Vector2 velocity, ProjectileType projectileType)
        {
            // Create the image for the projectile
            Texture2D projectileImage;
            
            if (projectileType == ProjectileType.Player)
            {
                // This is a projectile sent from the player, set it to the proper sprite
                projectileImage = playerProjectileSprite;
            }
            else
            {
                // This is a projectile sent from the enemy, set it to the proper sprite
                projectileImage = enemyProjectileSprite;
            }

            // Create the new projectile
            Projectile firedProjectile = new Projectile(position, velocity, projectileImage, projectileType);

            // Add the projectile to the list
            projectiles.Add(firedProjectile);
        }
    }
}
