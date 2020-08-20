using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingWizard2D
{    class Projectile : Sprite
    {
        // The current velocity of the sprite
        private Vector2 velocity = Vector2.Zero;

        // The current acceleration for the sprite
        private Vector2 acceleration = Vector2.Zero;

        // The type of this projectile
        private ProjectileType projectileType;
        public ProjectileType ProjectileType
        {
            get { return projectileType; }
            set { projectileType = value; }
        }
        

        // Initialize a projectile
        public Projectile(Vector2 position, Vector2 velocity, Texture2D spriteImage, ProjectileType projectileType) : base(position)
        {
            // Initialize values
            this.velocity = velocity;
            this.SpriteWidth = 16.0f;
            this.SpriteImage = spriteImage;
            this.projectileType = projectileType;
        }

        // Called each frame
        public void Update()
        {
            // Update velocity based on acceleration
            velocity += acceleration;

            // Update position based on velocity
            position += velocity;
        }
    }
}
