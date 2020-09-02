using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;

namespace FlyingWizard2D
{
    // A group of little evil things
    class EnemyColumn
    {
        // The top Y coordinate of the column (stays the same even when Enemy objects are removed)
        private float columnTop;

        // The bottom Y coordinate of the column (stays the same even when Enemy objects are removed)
        private float columnBottom;

        // A reference to the game that will contain the column
        private FlyingWizardGame root;

        // All the enemies in this column
        private List<Enemy> enemies;
        public List<Enemy> Enemies
        {
            get { return enemies; }
            set { enemies = value; }
        }

        // The sprite image used for each enemy in this column
        private Texture2D spriteImage;
        public Texture2D SpriteImage
        {
            get
            {
                return spriteImage;
            }
            
            set
            {
                spriteImage = value;
                foreach (Enemy e in enemies)
                {
                    // Update the sprite image for each enemy
                    e.SpriteImage = spriteImage;
                }
            }
        }

        // Speed each enemy travels horizontally
        private float xSpeed;
        public float XSpeed
        {
            get { return xSpeed; }
            set { xSpeed = value; }
        }

        // Speed each enemy travels vertically
        private float ySpeed;
        public float YSpeed
        {
            get { return ySpeed; }
            set { ySpeed = value; }
        }
        
        // The number of enemies in the column
        public int Count { get { return enemies.Count; } }

        // Initialize a column
        public EnemyColumn(FlyingWizardGame root, int numberOfEnemies, float spaceBetween, float topBound, float xSpeed, float ySpeed, float enemyWidth, Texture2D spriteImage)
        {
            // Set game root
            this.root = root;

            // Create an example sprite for calculations
            Enemy example = new Enemy(root, new Vector2(0, 0), 3.0f, enemyWidth);
            example.SpriteImage = spriteImage;

            // Calculate initial values
            this.xSpeed = -xSpeed;
            this.ySpeed = ySpeed;
            this.columnTop = topBound;
            this.columnBottom = topBound + numberOfEnemies * (example.SpriteHeight + spaceBetween);

            // Create the list of Enemy objects
            enemies = new List<Enemy>();
            for (int i = 0; i < numberOfEnemies; i++)
            {
                // Calculate Enemy Coordinates
                float xPosition = root.ScreenWidth;
                float yPosition = topBound + i * (example.SpriteHeight + spaceBetween);

                // Create the new Enemy and set some values
                Enemy nextEnemy = new Enemy(root, new Vector2(xPosition, yPosition), 3.0f, enemyWidth);
                nextEnemy.VelocityX = this.xSpeed;
                nextEnemy.VelocityY = this.ySpeed;

                // Add the Enemy to the list
                enemies.Add(nextEnemy);
            }

            // Set the sprite image for all Enemy objects
            this.SpriteImage = spriteImage;
        }

        // Called each frame
        public void Update(GameTime gameTime)
        {
            // Re-calculate the current top and bottom of the column
            columnTop += ySpeed;
            columnBottom += ySpeed;

            // If the column is going above or below the screen, flip its velocity
            if (columnTop < 0 || columnBottom > root.ScreenHeight)
            {
                ySpeed = -ySpeed;
            }

            foreach (Enemy e in enemies)
            {
                // Update each Enemy in the column
                e.VelocityY = ySpeed;
                e.Update(gameTime);
            }
        }

        // Draw each enemy in the column
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Enemy e in enemies)
            {
                e.Draw(gameTime, spriteBatch);
            }
        }

        // Get the enemy at the given index
        public Enemy GetEnemy(int index)
        {
            return enemies[index];
        }

        // Remove the given enemy from the column
        public void Remove(Enemy enemy)
        {
            enemies.Remove(enemy);
        }
    }
}
