using GameCore;
using System;

namespace PaperWork
{
    public class JumpOnInputDecreasesVerticalSpeed : IHandleUpdates
    {
        private readonly Func<bool> Grounded;
        private readonly Action<float> SetVerticalSpeed;
        private readonly Func<bool> JumpButtonPressed;
        private DateTime CooldownEnd;

        public JumpOnInputDecreasesVerticalSpeed(
            Func<bool> Grounded,
            Action<float> SetVerticalSpeed,
            Func<bool> JumpButtonPressed)
        {
            this.Grounded = Grounded;
            this.SetVerticalSpeed = SetVerticalSpeed;
            this.JumpButtonPressed = JumpButtonPressed;
        }
        public void Update(Entity ParentEntity)
        {
            if (JumpButtonPressed()
                && Grounded()
                && CooldownEnd < DateTime.Now
                )
            {
                SetVerticalSpeed(-10);
                CooldownEnd = DateTime.Now.AddMilliseconds(100);
            }
        }
    }
}
