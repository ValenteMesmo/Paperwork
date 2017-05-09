using GameCore;
using GameCore.Collision;

namespace PaperWork.GameEntities.Collisions
{
    public class MoveOnCollision : CollisionHandler
    {
        public MoveOnCollision(GameCollider Box) : base(Box)
        {
        }

        public override void CollisionFromTheLeft(GameCollider other)
        {
            var newPosition = new Coordinate2D();
            newPosition.X = other.Position.X + other.Width;
            newPosition.Y = ParentEntity.Position.Y;

            ParentEntity.Position = newPosition;
        }

        public override void CollisionFromTheRight(GameCollider other)
        {
            var newPosition = new Coordinate2D();
            newPosition.X = other.Position.X - other.Width;
            newPosition.Y = ParentEntity.Position.Y;

            ParentEntity.Position = newPosition;
        }
    }
}
