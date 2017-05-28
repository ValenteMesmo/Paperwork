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
        private readonly Func<Entity> NearLeftWall;
        private readonly Func<Entity> NearRightWall;
        private readonly Func<Entity> NearBotRightWall;
        private readonly Func<Entity> NearBotLeftWall;

        public SpeedUpHorizontallyOnInput(
            Action<float> SetHorizontalSpeed
            , Func<float> GetHorizontalSpeed
            , Func<bool> LeftButtonPressed
            , Func<bool> RightButtonPressed
            , Func<bool> Grounded
            , Func<Entity> NearLeftWall
            , Func<Entity> NearRightWall
            , Func<Entity> NearBotLeftWall
            , Func<Entity> NearBotRightWall
            )
        {
            this.SetHorizontalSpeed = SetHorizontalSpeed;
            this.LeftButtonPressed = LeftButtonPressed;
            this.RightButtonPressed = RightButtonPressed;
            this.Grounded = Grounded;
            this.GetHorizontalSpeed = GetHorizontalSpeed;
            this.NearLeftWall = NearLeftWall;
            this.NearRightWall = NearRightWall;
            this.NearBotLeftWall = NearBotLeftWall;
            this.NearBotRightWall = NearBotRightWall;
        }

        public void Update(Entity ParentEntity)
        {
            var speedx = GetHorizontalSpeed();

            var rightWall = NearRightWall();
            var rightBotWall = NearBotRightWall();
            var leftWall = NearLeftWall();
            var leftBotWall = NearBotLeftWall();

            if (RightButtonPressed() && rightWall == null && rightBotWall == null)
            {
                if (speedx < 0)
                    speedx += acceleration * reactivityPercent;
                else
                    speedx += acceleration;
            }
            else if (LeftButtonPressed() && leftWall == null && leftBotWall == null)
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
