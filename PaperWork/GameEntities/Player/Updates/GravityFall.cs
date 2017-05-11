using GameCore;
using System;

namespace PaperWork.PlayerHandlers.Updates
{
    public class GravityIncreasesVerticalSpeed : IHandleEntityUpdates
    {
        public const float VELOCITY = .05f;
        public const float MAX_SPEED = 5.0f;
        Func<float> GetVerticalSpeed;
        Action<float> SetVerticalSpeed;

        public GravityIncreasesVerticalSpeed(
            Func<float> GetVerticalSpeed
            , Action<float> SetVerticalSpeed) 
        {
            this.GetVerticalSpeed = GetVerticalSpeed;
            this.SetVerticalSpeed = SetVerticalSpeed;
        }

        public void Update(Entity entity)
        {
            var verticalSpeed = GetVerticalSpeed() + VELOCITY;
            if (verticalSpeed > MAX_SPEED)
                verticalSpeed = MAX_SPEED;

            SetVerticalSpeed(verticalSpeed);

            //ParentEntity.Position = new Coordinate2D(
            //    ParentEntity.Position.X,
            //    ParentEntity.Position.Y + ParentEntity.Speed.Y);
        }
    }
}
