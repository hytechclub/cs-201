using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingWizard2D
{
    // Little evil things
    class Enemy : Sprite
    {
        // The current velocity of the Enemy
        private Vector2 velocity = Vector2.Zero;

        // X Velocity for this Enemy
        public float VelocityX
        {
            get
            {
                return velocity.X;
            }

            set
            {
                velocity.X = value;
            }
        }

        // Y Velocity for this Enemy
        public float VelocityY
        {
            get
            {
                return velocity.Y;
            }

            set
            {
                velocity.Y = value;
            }
        }

        // Cool down object for projectile firing
        private CoolDown projectileCoolDown;

        // A reference to the game that will contain the player
        private FlyingWizardGame root;

        // Initialize an Enemy
        public Enemy(FlyingWizardGame root, Vector2 position, float projectileCoolDownTime = 1.0f, float spriteWidth = 64.0f) : base(position, 2, 0.5f)
        {
            // Initialize values
            this.root = root;
            this.SpriteWidth = spriteWidth;
            this.projectileCoolDown = new CoolDown(projectileCoolDownTime);
        }

        // Called each frame
        public override void Update(GameTime gameTime)
        {
            // Update base Sprite
            base.Update(gameTime);

            // Fire a projectile if not cooling down
            if (!projectileCoolDown.CoolingDown)
            {
                // Generate projectile information
                Vector2 projectilePosition = new Vector2(position.X, position.Y + SpriteHeight / 2);
                Vector2 projectileVelocity = new Vector2(-10.0f, 0.0f);

                // Fire the projectile
                root.FireProjectile(projectilePosition, projectileVelocity, ProjectileType.Enemy);

                // Kick off the cool down process
                projectileCoolDown.StartCoolDown();
            }
            
            // Update the cool down timer
            projectileCoolDown.Update(gameTime);

            // Update position based on velocity
            position += velocity;
        }
    }
}
