using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities;
using System;

namespace PaperWork.PlayerHandlers.Collisions
{
    public class HandleCollisionWithSolidObjects : IHandleCollision
    {
        private readonly Action<float> SetHorizontalSpeed;

        public HandleCollisionWithSolidObjects(
            Action<float> SetHorizontalSpeed)
        {
            this.SetHorizontalSpeed = SetHorizontalSpeed;
        }

        public void CollisionFromBelow(BaseCollider collider, BaseCollider other)
        {
            if (other.ParentEntity is PapersEntity
                || other.ParentEntity is SolidBlock)
            {
                collider.ParentEntity.Position =
                    new Coordinate2D(
                        collider.ParentEntity.Position.X,
                        other.Position.Y - collider.Height - 1);
            }
        }

        public void CollisionFromAbove(BaseCollider collider, BaseCollider other)
        {
            if (other.ParentEntity is SolidBlock)
            {
                collider.ParentEntity.Position =
                    new Coordinate2D(
                        collider.ParentEntity.Position.X,
                        other.Position.Y + other.Height + 1);
            }
            else if (other.ParentEntity is PapersEntity
                && other.ParentEntity.As<PapersEntity>().Grounded.Get() == false)
            {
                SetHorizontalSpeed(-10);
            }
        }
        
        public void CollisionFromTheLeft(BaseCollider collider, BaseCollider other)
        {
            if (other.ParentEntity is PapersEntity || other.ParentEntity is SolidBlock)
            {
                if (other.ParentEntity is PapersEntity
                    && other.ParentEntity.As<PapersEntity>().Grounded.Get() == false)
                    return;

                var newPosition = new Coordinate2D();
                newPosition.X = other.Position.X + other.Width + 1;
                newPosition.Y = collider.ParentEntity.Position.Y;

                collider.ParentEntity.Position = newPosition;
            }
        }

        public void CollisionFromTheRight(BaseCollider collider, BaseCollider other)
        {
            if (other.ParentEntity is PapersEntity || other.ParentEntity is SolidBlock)
            {
                if (other.ParentEntity is PapersEntity
                    && other.ParentEntity.As<PapersEntity>().Grounded.Get() == false)
                    return;

                var newPosition = new Coordinate2D();
                newPosition.X = other.Position.X - collider.Width - 1;
                newPosition.Y = collider.ParentEntity.Position.Y;

                collider.ParentEntity.Position = newPosition;
            }
        }
    }
}
