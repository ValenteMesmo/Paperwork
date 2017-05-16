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
        private readonly Collider mainCollider;

        public PapersEntity(int cellSize)
        {
            Textures.Add(new EntityTexture("papers", cellSize, cellSize * 2)
            {
                Offset = new Coordinate2D(0, -cellSize)
            });

            mainCollider = new Collider(this, cellSize, cellSize);
            mainCollider.AddHandlers(
                new StopsOnFixedPositionWhenColliding(VerticalSpeed.Set, HorizontalSpeed.Set));

            AddUpdateHandlers(
                new GravityIncreasesVerticalSpeed(VerticalSpeed.Get, VerticalSpeed.Set)
                , new FrictionSpeedLoss(HorizontalSpeed.Set,HorizontalSpeed.Get)
                , new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get)
                , new FollowOtherEntity(new Coordinate2D(-20, -mainCollider.Height), Target.Get)
            );

            Colliders.Add(mainCollider);
        }

        //this should not be here
        public void Grab(Entity grabbedBy)
        {
            mainCollider.Disabled = true;
            Target.Set(grabbedBy);
        }

        //this should not be here
        public void Drop(float speedX, float speedY)
        {
            mainCollider.Disabled = false;
            Target.Set(null);
            VerticalSpeed.Set(speedY);
            HorizontalSpeed.Set(speedX);
            var x = Position.X + 50;
            if (x > 50 * 12)
                x = 50 * 12;
            var y = Position.Y + 25;
            if (y < 50)
                y = 50;
            
            Position = new Coordinate2D(x, y);
        }
    }
}
