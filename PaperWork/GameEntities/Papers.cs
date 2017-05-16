using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Papers;
using PaperWork.PlayerHandlers.Updates;

namespace PaperWork
{
    public class PapersEntity : Entity
    {
        public readonly Property<Entity> Target = new Property<Entity>();
        public readonly Property<float> VerticalSpeed = new Property<float>();
        public readonly Property<float> HorizontalSpeed = new Property<float>();
        private readonly GameCollider mainCollider;

        public PapersEntity(int cellSize)
        {
            Textures.Add(new EntityTexture("papers", cellSize, cellSize * 2)
            {
                Offset = new Coordinate2D(0, -cellSize)
            });

            mainCollider = new GameCollider(this, cellSize, cellSize);
            mainCollider.AddHandlers(
                new StopsOnFixedPositionWhenColliding(VerticalSpeed.Set));

            AddHandlers(
                new GravityIncreasesVerticalSpeed(VerticalSpeed.Get, VerticalSpeed.Set)
                , new FollowOtherEntity(new Coordinate2D(0, -mainCollider.Height), Target.Get)
                , new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get));

            Colliders.Add(mainCollider);
        }

        public void Grab(Entity grabbedBy)
        {
            mainCollider.Disabled = true;
            Target.Set(grabbedBy);
        }

        public void Drop()
        {
            mainCollider.Disabled = false;
            Target.Set(null);
            Position = new Coordinate2D(Position.X + 50, Position.Y + 50);
        }
    }
}
