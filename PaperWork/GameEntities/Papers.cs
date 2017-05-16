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

        public PapersEntity(int cellSize, Action<Entity> SelfDestruct) : base(SelfDestruct)
        {
            Textures.Add(new EntityTexture("papers", cellSize, cellSize * 2)
            {
                Offset = new Coordinate2D(0, -cellSize)
            });

            mainCollider = new Collider(this, cellSize-2, cellSize);
            mainCollider.Position = new Coordinate2D(1,0);
            mainCollider.AddHandlers(
                new StopsOnFixedPositionWhenColliding(VerticalSpeed.Set, HorizontalSpeed.Set));
            Colliders.Add(mainCollider);

            AddUpdateHandlers(                
                 new ComputeHorizontalCombosCombo(new HorizontalNeighborChecker().GetNeighborsCombo)
                , new GravityIncreasesVerticalSpeed(VerticalSpeed.Get, VerticalSpeed.Set)
                , new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get)
                , new FollowOtherEntity(new Coordinate2D(-20, -mainCollider.Height), Target.Get)
            );

            var rightTrigger = new Trigger(this, cellSize - 20, cellSize - 40);
            rightTrigger.Position = new Coordinate2D(cellSize - 15, +25);
            rightTrigger.AddHandlers(new SetTriggeredNeighbor(RightNeighbor.Set, RightNeighbor.SetDefaut));
            Colliders.Add(rightTrigger);

            var leftTrigger = new Trigger(this, cellSize -20 , cellSize - 40);
            leftTrigger.Position = new Coordinate2D(- 15, 25);
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
        public void Drop()
        {
            mainCollider.Disabled = false;
            Target.Set(null);
            VerticalSpeed.Set(0);
            HorizontalSpeed.Set(0);
            var x = RoundUp(Position.X + 50,50);
            if (x > 50 * 12)
                x = 50 * 12;
            var y = Position.Y + 25;
            if (y < 50)
                y = 50;

            Position = new Coordinate2D(x, y);
        }


        //this should not be here
        private int RoundUp(float numToRound, int multiple)
        {
            return (int)Math.Round(numToRound / multiple) * multiple;
        }
    }
}
