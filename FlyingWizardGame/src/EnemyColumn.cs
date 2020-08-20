using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;

namespace FlyingWizard2D
{    class EnemyColumn
    {
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

        private float columnTop;
        private float columnBottom;
        
        // The number of enemies in the column
        public int Count { get { return enemies.Count; } }

        // A reference to the game that will contain the player
        private FlyingWizardGame root;

        // Initialize a player
        public EnemyColumn(FlyingWizardGame root, int numberOfEnemies, float spaceBetween, float topBound, float xSpeed, float ySpeed, float enemyWidth)
        {
            this.xSpeed = -xSpeed;
            this.ySpeed = ySpeed;
            this.columnTop = topBound;
            this.columnBottom = topBound + numberOfEnemies * (enemyWidth + spaceBetween);

            enemies = new List<Enemy>();
            for (int i = 0; i < numberOfEnemies; i++)
            {
                float xPosition = root.ScreenWidth;
                float yPosition = topBound + i * (enemyWidth + spaceBetween);

                Enemy nextEnemy = new Enemy(root, new Vector2(xPosition, yPosition), 3.0f, enemyWidth);
                nextEnemy.VelocityX = this.xSpeed;
                nextEnemy.VelocityY = this.ySpeed;
                enemies.Add(nextEnemy);
            }

            // Initialize values
            this.root = root;
        }

        // Called each frame
        public void Update(GameTime gameTime)
        {
            columnTop += ySpeed;
            columnBottom += ySpeed;

            if (columnTop < 0 || columnBottom > root.ScreenHeight)
            {
                ySpeed = -ySpeed;
            }

            foreach (Enemy e in enemies)
            {
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
