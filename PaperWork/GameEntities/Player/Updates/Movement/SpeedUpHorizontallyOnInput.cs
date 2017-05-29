using GameCore;
using System;

namespace PaperWork
{
    public class SpeedUpHorizontallyOnInput : IHandleUpdates
    {
        private readonly float MAX = 4;

        readonly float acceleration = 0.2f;
        readonly float friction = 0.2f;
        readonly float reactivityPercent = 3f;

        private readonly Action<float> SetHorizontalSpeed;
        private readonly Func<bool> LeftButtonPressed;
        private readonly Func<bool> RightButtonPressed;
        private readonly Func<bool> Grounded;
        private readonly Func<float> GetHorizontalSpeed;
        
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

            if (RightButtonPressed())
            {
                if (speedx < 0)
                    speedx += acceleration * reactivityPercent;
                else
                    speedx += acceleration;
            }
            else if (LeftButtonPressed())
            {
                if (speedx > 0)
                    speedx -= acceleration * reactivityPercent;
                else
                    speedx -= acceleration;
            }
            else
            {
                if (speedx > 0)
                {
                    speedx -= friction;
                }
                else if (speedx < 0)
                {
                    speedx += friction;
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
