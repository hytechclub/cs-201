using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingWizard2D
{    class Sprite
    {
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

        private CoolDown animationCoolDown;
        private int currentFrame;

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
            this.animationCoolDown = new CoolDown(timeBetweenFrames);
            this.SourceRectangle = null;
        }

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

        public virtual void Update(GameTime gameTime)
        {
            if (!animationCoolDown.CoolingDown)
            {
                currentFrame = (currentFrame + 1) % NumberOfFrames; 
                int frameHeight = SpriteImage.Height / NumberOfFrames;
                this.SourceRectangle = new Rectangle(0, currentFrame*frameHeight, SpriteImage.Width, frameHeight);

                animationCoolDown.StartCoolDown();
            }

            animationCoolDown.Update(gameTime);
        }
    }
}
