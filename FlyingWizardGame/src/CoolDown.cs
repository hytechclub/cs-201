using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingWizard2D
{    class CoolDown
    {
        // Total amount of time (in seconds) for each cool down
        private float coolDownTime;

        // Current amount of time for the cool down
        private float coolDownTimer;
        
        // Is the cool down currently cooling down?
        public bool CoolingDown { get; private set; }
        
        // Initialize a sprite
        public CoolDown(float coolDownTime = 0.5f)
        {
            // Initialize values
            this.coolDownTime = coolDownTime;
            this.coolDownTimer = 0.0f;
            this.CoolingDown = false;
        }

        // Kick off a cool down process
        public void StartCoolDown()
        {
            CoolingDown = true;
            coolDownTimer = 0.0f;
        }

        // Update the cool down timer
        public void Update(GameTime gameTime)
        {
            // If currently cooling down, increment the timer
            if (CoolingDown)
            {
                coolDownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (coolDownTimer >= coolDownTime)
                {
                    // Done cooling down!
                    CoolingDown = false;
                }
            }
        }
    }
}
