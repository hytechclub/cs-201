using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlyingWizard2D
{
    // The player, controlled by the keyboard
    class Player : Sprite
    {
        // The current velocity of the sprite
        private Vector2 velocity = Vector2.Zero;

        // The current acceleration for the sprite
        private Vector2 acceleration = Vector2.Zero;

        // A reference to the game that will contain the player
        private FlyingWizardGame root;

        // The movement acceleration for the player
        private float movementAcceleration = 0.45f;
        public float VerticalAcceleration
        {
            get { return movementAcceleration; }
            set { movementAcceleration = value; }
        }

        // The rate at which the player decelerates
        private float movementDeceleration = 0.09f;

        // The maximum speed the player can move
        private float maxMovementSpeed = 5.0f;
        public float MaxVerticalSpeed
        {
            get { return maxMovementSpeed; }
            set { value = maxMovementSpeed; }
        }

        // The speed with which the player bounces back
        private float bounceSpeed = 2.0f;
        public float BounceSpeed
        {
            get { return bounceSpeed; }
            set { bounceSpeed = value; }
        }
        
        // Cool down timer for projectile firing
        private Timer projectileCoolDown;
        public Timer ProjectileCoolDown
        {
            get { return projectileCoolDown; }
            set { projectileCoolDown = value;}
        }

        // Initialize a player
        public Player(FlyingWizardGame root, Vector2 position) : base(position)
        {
            this.root = root;
            this.SpriteWidth = 64.0f;
            this.NumberOfFrames = 2;
            this.projectileCoolDown = new Timer();

            LoadContent();
        }

        // Loads all the assets for the player
        public void LoadContent()
        {
            this.SpriteImage = root.Content.Load<Texture2D>("WizardGuy");
        }

        // Update acceleration based on input
        private void HandleInput(KeyboardState currentKeyboardState)
        {
            bool upKeyPressed = currentKeyboardState.IsKeyDown(Keys.Up);
            bool downKeyPressed = currentKeyboardState.IsKeyDown(Keys.Down);
            bool leftKeyPressed = currentKeyboardState.IsKeyDown(Keys.Left);
            bool rightKeyPressed = currentKeyboardState.IsKeyDown(Keys.Right);

            // Get current movement
            float yVelocity = velocity.Y;
            float xVelocity = velocity.X;
            bool movingUp = yVelocity < 0;
            bool movingDown = yVelocity > 0;
            bool movingLeft = xVelocity < 0;
            bool movingRight = xVelocity > 0;

            // If the player is pressing the Down key...
            if (downKeyPressed)
            {
                // Accelerate down
                acceleration.Y = movementAcceleration;
            }
            else if (!upKeyPressed && movingDown)
            {
                // The player is moving down but not pressing Down, slow them down
                acceleration.Y = -movementDeceleration;
            }
            
            // If the player is pressing the Up key...
            if (upKeyPressed)
            {
                // Accelerate up
                acceleration.Y = -movementAcceleration;
            }
            else if (!downKeyPressed && movingUp)
            {
                // The player is moving up but not pressing Up, slow them down
                acceleration.Y = movementDeceleration;
            }

            // If the player is pressing the Right key...
            if (rightKeyPressed)
            {
                // Accelerate right
                acceleration.X = movementAcceleration;
            }
            else if (!leftKeyPressed && movingRight)
            {
                // The player is moving right but not pressing Right, slow them down
                acceleration.X = -movementDeceleration;
            }
            
            // If the player is pressing the Left key...
            if (leftKeyPressed)
            {
                // Accelerate left
                acceleration.X = -movementAcceleration;
            }
            else if (!rightKeyPressed && movingLeft)
            {
                // The player is moving left but not pressing Left, slow them down
                acceleration.X = movementDeceleration;
            }
        }

        // Update movement based on edge collisions
        private void handleBounce()
        {
            // Set up edge bounds
            float rightBound = root.DividerX - SpriteWidth;
            float bottomBound = root.ScreenHeight - SpriteHeight;


            if (position.X < 0)
            {
                // Colliding to the left
                position.X = 0;
                velocity.X = bounceSpeed;
                acceleration.X = -movementAcceleration;
            }
            else if (position.X > rightBound)
            {
                // Colliding to the right
                position.X = rightBound;
                velocity.X = -bounceSpeed;
                acceleration.X = movementAcceleration;
            }

            if (position.Y < 0)
            {
                // Colliding to the top
                position.Y = 0;
                velocity.Y = bounceSpeed;
                acceleration.Y = -movementAcceleration;
            }
            else if (position.Y > bottomBound)
            {
                // Colliding to the bottom
                position.Y = bottomBound;
                velocity.Y = -bounceSpeed;
                acceleration.Y = movementAcceleration;
            }
        }

        // Called each frame
        public override void Update(GameTime gameTime)
        {
            // Update sprite
            base.Update(gameTime);

            // Default up/down acceleration to zero
            acceleration.Y = 0;
            
            // Get current keyboard state
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Handle any movement input
            HandleInput(currentKeyboardState);

            // Fire a projectile if the cool down is not active and Space is pressed
            if (!projectileCoolDown.Active && currentKeyboardState.IsKeyDown(Keys.Space))
            {
                // Generate the projectile information
                Vector2 projectilePosition = new Vector2(position.X + SpriteWidth, position.Y + SpriteHeight / 2);
                Vector2 projectileVelocity = new Vector2(10.0f, 0.0f);

                // Fire the projectile
                root.FireProjectile(projectilePosition, projectileVelocity, ProjectileType.Player);
                
                // Kick off the cool down timer process
                projectileCoolDown.StartTimer();
            }
            
            // Update the cool down
            projectileCoolDown.Update(gameTime);

            // Update position based on velocity
            position += velocity;
            
            // Update velocity based on acceleration
            velocity += acceleration;

            // Update if colliding
            handleBounce();
            
            // Make sure the player does not move faster than the maximum speed
            velocity.Y = MathHelper.Clamp(velocity.Y, -maxMovementSpeed, maxMovementSpeed);
            velocity.X = MathHelper.Clamp(velocity.X, -maxMovementSpeed, maxMovementSpeed);

            // Control for extra tiny movements
            if (velocity.Y >= -0.1f && velocity.Y <= 0.1f)
            {
                velocity.Y = 0;
            }

            // Control for extra tiny movements
            if (velocity.X >= -0.1f && velocity.X <= 0.1f)
            {
                velocity.X = 0;
            }
        }
    }
}
