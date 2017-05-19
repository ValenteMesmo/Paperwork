using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using System;

namespace PaperWork.GameEntities.Papers
{
    public class PaperZeroVerticalSpeedWhenCollidingVertically : IHandleCollision
    {
        private readonly Action<float> SetVerticalSpeed;
        private readonly Action<float> SetHorizontalSpeed;

        public PaperZeroVerticalSpeedWhenCollidingVertically(
            Action<float> SetVerticalSpeed
            , Action<float> SetHorizontalSpeed)
        {
            this.SetVerticalSpeed = SetVerticalSpeed;
            this.SetHorizontalSpeed = SetHorizontalSpeed;
        }

        public void CollisionFromBelow(BaseCollider collider, BaseCollider other)
        {
            if (other.ParentEntity is SolidBlock
                || other.ParentEntity is PapersEntity)
            {
                SetVerticalSpeed(0);

                var y = MathHelper.RoundUp(other.Position.Y - other.Height - 1, 50);
                if (y < 50)
                    y = 50;

                collider.ParentEntity.Position = new Coordinate2D(
                    collider.ParentEntity.Position.X,
                    y);
            }
        }

        public void CollisionFromAbove(BaseCollider collider, BaseCollider other)
        {
        }

        public void CollisionFromTheLeft(BaseCollider collider, BaseCollider other)
        {
        }

        public void CollisionFromTheRight(BaseCollider collider, BaseCollider other)
        {
        }
    }
}
