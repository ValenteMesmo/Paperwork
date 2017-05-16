using GameCore;
using GameCore.Collision;

namespace PaperWork.GameEntities.Collisions
{
    public class MoveBackWhenHittingWall : IHandleCollision
    {
        public void CollisionFromAbove(BaseCollider collider, BaseCollider other)
        {
        }

        public void CollisionFromBelow(BaseCollider collider, BaseCollider other)
        {
        }

        public void CollisionFromTheLeft(BaseCollider collider, BaseCollider other)
        {
            if (other.ParentEntity is PapersEntity || other.ParentEntity is SolidBlock)
            {
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
                var newPosition = new Coordinate2D();
                newPosition.X = other.Position.X - collider.ParentEntity.Colliders[0].Width - 1;
                newPosition.Y = collider.ParentEntity.Position.Y;

                collider.ParentEntity.Position = newPosition;
            }
        }
    }
}
