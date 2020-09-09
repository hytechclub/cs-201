using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingWizard2D
{
    // Sprite base class, contains basic sprite functionality
    class Sprite
    {
        // Timer for animation frame switching
        private Timer nextFrameTimer;

        // Current frame of the animation
        private int currentFrame;
        
        // An image texture for the sprite
        private Texture2D spriteImage;
        public Texture2D SpriteImage
        {
            get { return spriteImage; }
            set { spriteImage = value; }
        }

        // The width of the sprite
        private float spriteWidth;
        public float SpriteWidth
        {
            get { return spriteWidth; }
            set { spriteWidth = value; }
        }

        // The height of the sprite
        public float SpriteHeight
        {
            get
            {
                // Calculated based on the width
                float scale = spriteWidth / spriteImage.Width;
                return (spriteImage.Height * scale) / NumberOfFrames;
            }
        }

        // The properly scaled position rectangle for the sprite
        public Rectangle PositionRectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)spriteWidth, (int)SpriteHeight);
            }
        }

        // Number of frames for an animated sprite
        private int numberOfFrames;
        public int NumberOfFrames
        {
            get
            {
                return numberOfFrames;
            }

            set
            {
                numberOfFrames = value;
            }
        }

        // The part of the sprite image that will be drawn for the sprite
        // (can change based on animation)
        public Rectangle? SourceRectangle { get; set; }

        // The current position of the sprite
        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        
        // Initialize a sprite
        public Sprite(Vector2 position, int numberOfFrames = 1, float timeBetweenFrames = 0.25f)
        {
            // Initialize values
            this.position = position;
            this.NumberOfFrames = numberOfFrames;
            this.nextFrameTimer = new Timer(timeBetweenFrames);
            this.SourceRectangle = null;
        }

        // Set the sprite image, along with the total number of frames the image contains
        public void SetSpriteImage(Texture2D spriteImage, int numberOfFrames)
        {
            this.spriteImage = spriteImage;
            this.numberOfFrames = numberOfFrames;
        }

        // Draw the sprite
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteImage, PositionRectangle, SourceRectangle, Color.White);
        }

        // Called every frame
        public virtual void Update(GameTime gameTime)
        {
            // If the animation timer goes off (is no longer active), time to switch frames
            if (!nextFrameTimer.Active)
            {
                // Get the next frame (cycle back to the beginning if necessary)
                currentFrame = (currentFrame + 1) % NumberOfFrames;

                // Calculate the height of a frame
                int frameHeight = SpriteImage.Height / NumberOfFrames;

                // Create the source rectangle based on the current frame, and the frame height
                this.SourceRectangle = new Rectangle(0, currentFrame*frameHeight, SpriteImage.Width, frameHeight);

                // Kick off the timer for the next frame
                nextFrameTimer.StartTimer();
            }

            // Update the animation timer
            nextFrameTimer.Update(gameTime);
        }
    }
}
