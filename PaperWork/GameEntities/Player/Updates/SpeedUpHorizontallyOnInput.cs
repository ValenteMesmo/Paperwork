using GameCore;
using System;

namespace PaperWork
{
    public class SpeedUpHorizontallyOnInput : IHandleUpdates
    {
        private readonly Action<float> SetHorizontalSpeed;
        private readonly Func<bool> LeftButtonPressed;
        private readonly Func<bool> RightButtonPressed;
        private float speedx = 0;

        public SpeedUpHorizontallyOnInput(
            Action<float> SetHorizontalSpeed,
            Func<bool> LeftButtonPressed,
            Func<bool> RightButtonPressed)
        {
            this.SetHorizontalSpeed = SetHorizontalSpeed;
            this.LeftButtonPressed = LeftButtonPressed;
            this.RightButtonPressed = RightButtonPressed;
        }

        public void Update(Entity ParentEntity)
        {
            if (RightButtonPressed())
            {
                speedx += 0.5f;
            }
            else if (LeftButtonPressed())
            {
                speedx -= 0.5f;
            }
            else
            {
                if (speedx > 0)
                    speedx -= 0.5f;
                else if (speedx < 0)
                    speedx += 0.5f;
            }

            if (speedx < -2)
                speedx = -2;
            if (speedx > 2)
                speedx = 2;

            SetHorizontalSpeed(speedx);
        }
    }
}
