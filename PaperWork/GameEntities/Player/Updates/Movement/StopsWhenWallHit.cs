using GameCore;
using System;

namespace PaperWork
{
    public class StopsWhenWallHit<T> : IHandleUpdates where T : Entity
    {
        private readonly Func<float> GetHorizontalSpeed;
        private readonly Func<Entity> GetRightWall;
        private readonly Func<Entity> GetLeftWall;
        private readonly Func<Entity> GetBotRightWall;
        private readonly Func<Entity> GetBotLeftWall;
        private readonly Action<float> SetHorizontalSpeed;

        public StopsWhenWallHit(
            Func<float> GetHorizontalSpeed
            , Func<Entity> GetRightWall
            , Func<Entity> GetLeftWall
            , Func<Entity> GetBotRightWall
            , Func<Entity> GetBotLeftWall
            , Action<float> SetHorizontalSpeed)
        {
            this.GetHorizontalSpeed = GetHorizontalSpeed;
            this.GetRightWall = GetRightWall;
            this.GetLeftWall = GetLeftWall;
            this.GetBotRightWall = GetBotRightWall;
            this.GetBotLeftWall = GetBotLeftWall;
            this.SetHorizontalSpeed = SetHorizontalSpeed;
        }

        public void Update(Entity ParentEntity)
        {
            var speedx = GetHorizontalSpeed();

            if (speedx > 0)
            {
                var rightWall = GetRightWall();
                var rightBotWall = GetBotRightWall();

                if (rightWall != null && rightWall is T)
                {
                    ParentEntity.Position = new Coordinate2D(
                        rightWall.Position.X - ParentEntity.Width
                        , ParentEntity.Position.Y);

                    SetHorizontalSpeed(0);
                }
                else if (rightBotWall != null && rightBotWall is T)
                {
                    ParentEntity.Position = new Coordinate2D(
                        rightBotWall.Position.X - ParentEntity.Width
                        , ParentEntity.Position.Y);

                    SetHorizontalSpeed(0);
                }
            }
            else if (speedx < 0)
            {
                var leftWall = GetLeftWall();
                var leftBotWall = GetBotLeftWall();

                if (leftWall != null && leftWall is T)
                {
                    ParentEntity.Position = new Coordinate2D(
                        leftWall.Position.X + leftWall.Width
                        , ParentEntity.Position.Y);

                    SetHorizontalSpeed(0);
                }
                else if (leftBotWall != null && leftBotWall is T)
                {
                    ParentEntity.Position = new Coordinate2D(
                        leftBotWall.Position.X + leftBotWall.Width
                        , ParentEntity.Position.Y);

                    SetHorizontalSpeed(0);
                }
            }
        }
    }
}
