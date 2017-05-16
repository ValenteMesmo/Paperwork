using GameCore;
using GameCore.Collision;
using System;

namespace PaperWork.GameEntities.Papers
{
    public class StopsOnFixedPositionWhenColliding : CollisionHandler
    {
        private readonly Action<float> SetVerticalSpeed;

        public StopsOnFixedPositionWhenColliding(
            Action<float> SetVerticalSpeed) 
        {
            this.SetVerticalSpeed = SetVerticalSpeed;
        }

        public override void CollisionFromBelow(GameCollider collider, GameCollider other)
        {
            if (other.ParentEntity is SolidBlock
                || other.ParentEntity is PapersEntity)
            {
                SetVerticalSpeed(0);

                collider.ParentEntity.Position =
                    new Coordinate2D(
                        collider.ParentEntity.Position.X,
                        other.Position.Y - other.Height - 1);
            }
        }
    }
}
