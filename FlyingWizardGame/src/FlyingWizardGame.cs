using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace FlyingWizard2D
{
    class FlyingWizardGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont textFont;
        private int life = 3;
        private int score = 0;

        Player player;
        List<EnemyColumn> enemyColumns;

        private int screenWidth = 1600;
        public int ScreenWidth
        {
            get { return screenWidth; }
            set { screenWidth = value; }
        }

        private int screenHeight = 900;
        public int ScreenHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }

        private Vector2 dividerPosition;
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

        private List<Projectile> projectiles;

        private Texture2D playerProjectileSprite;
        private Texture2D enemyProjectileSprite;
        private Texture2D enemySprite;
        private Texture2D dividerLine;
        private Texture2D screenCover;
        private string screenCoverText;
        private bool coverScreen;

        private CoolDown enemyColumnTimer;
        
        public FlyingWizardGame()
        {
            graphics = new GraphicsDeviceManager(this);

            // Window
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;

            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            player = new Player(this, new Vector2(0, ScreenHeight / 2));
            projectiles = new List<Projectile>();
            dividerPosition = new Vector2(300.0f, 0);
            enemyColumns = new List<EnemyColumn>();
            enemyColumnTimer = new CoolDown(20.0f);
        }

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

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load content
            playerProjectileSprite = Content.Load<Texture2D>("fireball");
            enemyProjectileSprite = Content.Load<Texture2D>("bluefireball");
            enemySprite = Content.Load<Texture2D>("MoonBounce");
            textFont = Content.Load<SpriteFont>("Text");

            // Set content as needed
            dividerLine = createRectangleTexture(1, screenHeight, Color.LightGray);
            screenCover = createRectangleTexture(ScreenWidth, ScreenHeight, Color.White);
            coverScreen = true;
            screenCoverText = "Press Enter to Play";
        }

        // Check for collisions between bounding rectangles
        private bool getCollision(Rectangle spriteBounds1, Rectangle spriteBounds2)
        {
            // Get the center points
            Point sprite1Center = spriteBounds1.Center;
            Point sprite2Center = spriteBounds2.Center;

            // Get the distances between the rectangle centers
            float xDistance = Math.Abs(sprite1Center.X - sprite2Center.X);
            float yDistance = Math.Abs(sprite1Center.Y - sprite2Center.Y);

            // Get the distances required for collision across each axis
            float collisionDistanceX = (spriteBounds1.Width / 2) + (spriteBounds2.Width / 2);
            float collisionDistanceY = (spriteBounds1.Height / 2) + (spriteBounds2.Height / 2);

            // Check for overlap on BOTH axes
            return xDistance <= collisionDistanceX && yDistance <= collisionDistanceY;
        }

        // Decrement life count, and end the game if it goes to zero
        private void decrementLife()
        {
            if(--life == 0)
            {
                gameOver();
            }
        }

        // End the game
        private void gameOver()
        {
            coverScreen = true;
            screenCoverText = $"Final Score: {score}\nPress Esc to Exit";
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (coverScreen)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    coverScreen = false;
                }
                else
                {
                    return;
                }
            }

            if (!enemyColumnTimer.CoolingDown)
            {
                EnemyColumn nextColumn = new EnemyColumn(this, 5, 20.0f, 50, 0.5f, 1.0f, 64.0f);
                nextColumn.SpriteImage = enemySprite;
                enemyColumns.Add(nextColumn);

                enemyColumnTimer.StartCoolDown();
            }

            enemyColumnTimer.Update(gameTime);

            player.Update(gameTime);
            enemyColumns.ForEach(e => e.Update(gameTime));

            // Loop through backwards in order to remove off-screen projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                // Update the current projectile
                Projectile p = projectiles[i];
                p.Update();

                bool playerProjectile = p.ProjectileType == ProjectileType.Player;

                // Check if the player collides with this projectile
                if (!playerProjectile && getCollision(player.PositionRectangle, p.PositionRectangle))
                {
                    // There is a collision, decrement life and remove the projectile
                    decrementLife();
                    projectiles.Remove(p);
                    continue;
                }
                else if (playerProjectile)
                {
                    // If this projectile is from a player, loop through all the enemies
                    for (int j = enemyColumns.Count - 1; j >= 0; --j)
                    {
                        EnemyColumn enemyColumn = enemyColumns[j];
                        if (enemyColumn.Count == 0)
                        {
                            enemyColumns.Remove(enemyColumn);
                            continue;
                        }

                        for (int k = enemyColumn.Count - 1; k >= 0; --k)
                        {
                            // Check if the enemy is colliding with the projectile
                            Enemy currentEnemy = enemyColumn.GetEnemy(k);
                            if (getCollision(currentEnemy.PositionRectangle, p.PositionRectangle))
                            {
                                // Increment the score
                                ++score;

                                // Remove the projectile and the enemy
                                projectiles.Remove(p);
                                enemyColumn.Remove(currentEnemy);
                                continue;
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
            
            base.Update(gameTime);
        }

        private void drawStringInCenter(SpriteBatch spriteBatch, SpriteFont textFont, string text, Color color)
        {
            Vector2 textMeasurements = textFont.MeasureString(screenCoverText);
            float xPos = (ScreenWidth / 2) - (textMeasurements.X / 2);
            float yPos = (ScreenHeight / 2) - (textMeasurements.Y / 2);

            spriteBatch.DrawString(textFont, text, new Vector2(xPos, yPos), color);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            player.Draw(gameTime, spriteBatch);
            enemyColumns.ForEach(e => e.Draw(gameTime, spriteBatch));

            spriteBatch.Draw(dividerLine, dividerPosition, Color.White);

            foreach (Projectile p in projectiles)
            {
                p.Draw(gameTime, spriteBatch);
            }

            spriteBatch.DrawString(textFont, $"Life: {life}\nScore: {score}", Vector2.Zero, Color.White);

            if (coverScreen)
            {
                spriteBatch.Draw(screenCover, Vector2.Zero, Color.White);
                drawStringInCenter(spriteBatch, textFont, screenCoverText, Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void FireProjectile(Vector2 position, Vector2 velocity, ProjectileType projectileType)
        {
            Texture2D projectileImage;
            
            if (projectileType == ProjectileType.Player)
            {
                projectileImage = playerProjectileSprite;
            }
            else
            {
                projectileImage = enemyProjectileSprite;
            }

            projectiles.Add(new Projectile(position, velocity, projectileImage, projectileType));
        }
    }
}
