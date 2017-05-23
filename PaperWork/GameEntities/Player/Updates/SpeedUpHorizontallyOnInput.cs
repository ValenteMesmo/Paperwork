using GameCore;
using System;

namespace PaperWork
{
    public class SpeedUpHorizontallyOnInput : IHandleUpdates
    {
        private readonly Action<float> SetHorizontalSpeed;
        private readonly Func<bool> LeftButtonPressed;
        private readonly Func<bool> RightButtonPressed;
        private readonly Func<bool> Grounded;
        private readonly Func<float> GetHorizontalSpeed;
        private readonly float MAX = 2;

        public SpeedUpHorizontallyOnInput(
            Action<float> SetHorizontalSpeed
            , Func<float> GetHorizontalSpeed
            , Func<bool> LeftButtonPressed
            , Func<bool> RightButtonPressed
            , Func<bool> Grounded
            )
        {
            this.SetHorizontalSpeed = SetHorizontalSpeed;
            this.LeftButtonPressed = LeftButtonPressed;
            this.RightButtonPressed = RightButtonPressed;
            this.Grounded = Grounded;
            this.GetHorizontalSpeed = GetHorizontalSpeed;
        }

        public void Update(Entity ParentEntity)
        {
            var speedx = GetHorizontalSpeed();
            var bonus = 0.5f;
            if (RightButtonPressed())
            {
                speedx += bonus;
            }
            else if (LeftButtonPressed())
            {
                speedx -= bonus;
            }
            else
            {
                if (speedx > 0)
                {
                    speedx -= bonus * 0.5f;
                }
                else if (speedx < 0)
                {
                    speedx += bonus * 0.5f;
                }

                if (speedx > -0.01f && speedx < 0.01f)
                    speedx = 0;
            }

            if (speedx < -MAX)
                speedx = -MAX;
            if (speedx > MAX)
                speedx = MAX;

            SetHorizontalSpeed(speedx);
        }
    }
}
