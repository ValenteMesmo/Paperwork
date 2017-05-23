using GameCore;
using System;

namespace PaperWork.PlayerHandlers.Updates
{
    public class GravityIncreasesVerticalSpeed : IHandleUpdates
    {
        public const float VELOCITY = .6f;
        public const float MAX_SPEED = 4f;

        private readonly Func<float> GetVerticalSpeed;
        private readonly Action<float> SetVerticalSpeed;
        private readonly Func<Entity> Grounded;

        public GravityIncreasesVerticalSpeed(
            Func<float> GetVerticalSpeed
            , Action<float> SetVerticalSpeed
            , Func<Entity> Grounded)
        {
            this.GetVerticalSpeed = GetVerticalSpeed;
            this.SetVerticalSpeed = SetVerticalSpeed;
            this.Grounded = Grounded;
        }

        public void Update(Entity entity)
        {
            var ground = Grounded();
            //TODO: create other class for this
            if (ground == null)
            {
                var verticalSpeed = GetVerticalSpeed() + VELOCITY;
                if (verticalSpeed > MAX_SPEED)
                    verticalSpeed = MAX_SPEED;

                SetVerticalSpeed(verticalSpeed);
            }
            else
            {
                if (GetVerticalSpeed() > 0)
                {
                    entity.Position = new Coordinate2D(
                        entity.Position.X
                        , ground.Position.Y - entity.Height
                    );
                    SetVerticalSpeed(0);
                }
            }
        }
    }
}
