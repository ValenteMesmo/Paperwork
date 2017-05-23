using GameCore;
using System;

namespace PaperWork
{
    public class SpeedUpHorizontallyOnInput : IHandleUpdates
    {
        private readonly float MAX = 4;
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
            var acceleration = 0.1f;
            var friction = acceleration * 0.5f;
            var reactivityPercent = 2.5f;

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

            var rightWall = NearRightWall();
            var rightBotWall = NearBotRightWall();
            var leftWall = NearLeftWall();
            var leftBotWall = NearBotLeftWall();

            if (speedx > 0)
            {
                if (rightWall != null)
                {
                    ParentEntity.Position = new Coordinate2D(
                        rightWall.Position.X - ParentEntity.Width
                        , ParentEntity.Position.Y);
                    speedx = 0;

                }
                else if (rightBotWall != null)
                {
                    ParentEntity.Position = new Coordinate2D(
                        rightBotWall.Position.X - ParentEntity.Width
                        , ParentEntity.Position.Y);
                    speedx = 0;
                }
            }
            else if (speedx < 0)
            {
                if (leftWall != null)
                {
                    ParentEntity.Position = new Coordinate2D(
                        leftWall.Position.X + leftWall.Width
                        , ParentEntity.Position.Y);
                    speedx = 0;

                }
                else if (leftBotWall != null)
                {
                    ParentEntity.Position = new Coordinate2D(
                        leftBotWall.Position.X + leftBotWall.Width
                        , ParentEntity.Position.Y);
                    speedx = 0;
                }
            }

            if (speedx < -MAX)
                speedx = -MAX;
            if (speedx > MAX)
                speedx = MAX;

            SetHorizontalSpeed(speedx);
        }
    }
}
