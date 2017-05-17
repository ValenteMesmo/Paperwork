using GameCore;
using GameCore.Collision;
using System;

namespace PaperWork.GameEntities.Papers
{
    public class ZeroVerticalSpeedWhenCollidingVertically : IHandleCollision
    {
        private readonly Action<float> SetVerticalSpeed;
        private readonly Action<float> SetHorizontalSpeed;

        public ZeroVerticalSpeedWhenCollidingVertically(
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

                var y = RoundUp(other.Position.Y - other.Height - 1, 50);
                if (y < 50)
                    y = 50;

                collider.ParentEntity.Position = new Coordinate2D(
                    collider.ParentEntity.Position.X,
                    y);
            }
        }

        public void CollisionFromAbove(BaseCollider collider, BaseCollider other)
        {
            if (other.ParentEntity is SolidBlock)
            {
                SetVerticalSpeed(0);

                var y = RoundUp(other.Position.Y + collider.Height + 1, 50);
                if (y < 50)
                    y = 50;

                collider.ParentEntity.Position = new Coordinate2D(
                    collider.ParentEntity.Position.X,
                    y);
            }
        }

        //this should not be here
        private int RoundUp(float numToRound, int multiple)
        {
            return (int)Math.Round(numToRound / multiple) * multiple;
        }

        public void CollisionFromTheLeft(BaseCollider collider, BaseCollider other)
        {
        }

        public void CollisionFromTheRight(BaseCollider collider, BaseCollider other)
        {
        }
    }
}
