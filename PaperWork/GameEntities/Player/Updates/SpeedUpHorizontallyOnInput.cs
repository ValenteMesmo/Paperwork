using GameCore;
using System;

namespace PaperWork
{
    public class SpeedUpHorizontallyOnInput : IHandleEntityUpdates
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

            speedx = speedx.LimitToRange(-2, 2);

            SetHorizontalSpeed(speedx);
        }
    }
}
