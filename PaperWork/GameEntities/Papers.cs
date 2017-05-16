using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Papers;
using PaperWork.GameEntities.Papers.Updates;
using PaperWork.PlayerHandlers.Updates;
using System;

namespace PaperWork
{
    public class PapersEntity : Entity
    {
        public readonly Property<Entity> Target = new Property<Entity>();
        public readonly Property<float> VerticalSpeed = new Property<float>();
        public readonly Property<float> HorizontalSpeed = new Property<float>();
        public readonly Property<PapersEntity> RightNeighbor = new Property<PapersEntity>();
        public readonly Property<PapersEntity> LeftNeighbor = new Property<PapersEntity>();
        private readonly Collider mainCollider;

        public PapersEntity(int cellSize, Action<Entity> DestroyEntity) : base(DestroyEntity)
        {
            Textures.Add(new EntityTexture("papers", cellSize, cellSize * 2)
            {
                Offset = new Coordinate2D(0, -cellSize)
            });

            mainCollider = new Collider(this, cellSize, cellSize);
            mainCollider.AddHandlers(
                new StopsOnFixedPositionWhenColliding(VerticalSpeed.Set, HorizontalSpeed.Set));
            Colliders.Add(mainCollider);

            AddUpdateHandlers(
                new GravityIncreasesVerticalSpeed(VerticalSpeed.Get, VerticalSpeed.Set)
                , new FrictionSpeedLoss(HorizontalSpeed.Set, HorizontalSpeed.Get)
                , new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get)
                , new FollowOtherEntity(new Coordinate2D(-20, -mainCollider.Height), Target.Get)
                , new ComputeHorizontalCombosCombo(new HorizontalNeighborChecker().GetNeighborsCombo)
            );

            var rightTrigger = new Trigger(this, cellSize - 10, cellSize - 10);
            rightTrigger.Position = new Coordinate2D(cellSize + 5, +5);
            rightTrigger.AddHandlers(new SetTriggeredNeighbor(RightNeighbor.Set, RightNeighbor.SetDefaut));
            Colliders.Add(rightTrigger);

            var leftTrigger = new Trigger(this, cellSize - 10, cellSize - 10);
            leftTrigger.Position = new Coordinate2D(-cellSize + 5, 5);
            leftTrigger.AddHandlers(new SetTriggeredNeighbor(LeftNeighbor.Set, LeftNeighbor.SetDefaut));
            Colliders.Add(leftTrigger);
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
