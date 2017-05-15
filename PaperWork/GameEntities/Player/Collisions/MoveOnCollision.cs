using GameCore;
using GameCore.Collision;

namespace PaperWork.GameEntities.Collisions
{
    public class MoveBackWhenHittingWall : CollisionHandler
    {
        public MoveBackWhenHittingWall(GameCollider Box) : base(Box)
        {
        }

        public override void CollisionFromTheLeft(GameCollider other)
        {
            if (other.ParentEntity is PapersEntity || other.ParentEntity is SolidBlock)
            {
                var newPosition = new Coordinate2D();
                newPosition.X = other.Position.X + other.Width + 1;
                newPosition.Y = ParentEntity.Position.Y;

                ParentEntity.Position = newPosition;
            }
        }

        public override void CollisionFromTheRight(GameCollider other)
        {
            if (other.ParentEntity is PapersEntity || other.ParentEntity is SolidBlock)
            {
                var newPosition = new Coordinate2D();
                newPosition.X = other.Position.X - other.Width -1;
                newPosition.Y = ParentEntity.Position.Y;

                ParentEntity.Position = newPosition;
            }
        }
    }
}
