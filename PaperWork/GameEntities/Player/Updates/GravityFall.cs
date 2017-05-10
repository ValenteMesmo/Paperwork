using GameCore;
using System;

namespace PaperWork.PlayerHandlers.Updates
{
    public class GravityIncreasesVerticalSpeed : UpdateHandler
    {
        public const float VELOCITY = .05f;
        public const float MAX_SPEED = 5.0f;
        Func<float> GetVerticalSpeed;
        Action<float> SetVerticalSpeed;

        public GravityIncreasesVerticalSpeed(
            Entity player
            , Func<float> GetVerticalSpeed
            , Action<float> SetVerticalSpeed) : base(player)
        {
            this.GetVerticalSpeed = GetVerticalSpeed;
            this.SetVerticalSpeed = SetVerticalSpeed;
        }

        public override void Update()
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
