using GameCore;
using GameCore.Collision;

namespace PaperWork.GameEntities.Collisions
{
    public class MoveBackWhenHittingWall : CollisionHandler
    {
        public override void CollisionFromTheLeft(GameCollider collider, GameCollider other)
        {
            if (other.ParentEntity is PapersEntity || other.ParentEntity is SolidBlock)
            {
                var newPosition = new Coordinate2D();
                newPosition.X = other.Position.X + other.Width + 1;
                newPosition.Y = collider.ParentEntity.Position.Y;

                collider.ParentEntity.Position = newPosition;
            }
        }

        public override void CollisionFromTheRight(GameCollider collider, GameCollider other)
        {
            if (other.ParentEntity is PapersEntity || other.ParentEntity is SolidBlock)
            {
                var newPosition = new Coordinate2D();
                newPosition.X = other.Position.X - collider.ParentEntity.Colliders[0].Width -1;
                newPosition.Y = collider.ParentEntity.Position.Y;

                collider.ParentEntity.Position = newPosition;
            }
        }
    }
}
