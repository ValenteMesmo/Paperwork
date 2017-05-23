using GameCore;
using GameCore.Collision;
using Microsoft.Xna.Framework;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Papers;
using PaperWork.GameEntities.Papers.Updates;
using PaperWork.GameEntities.Player.Updates;
using PaperWork.PlayerHandlers.Updates;
using System;

namespace PaperWork
{
    public class PapersEntity : Entity
    {
        public readonly Property<Entity> Target = new Property<Entity>();
        public readonly Property<Entity> Grounded = new Property<Entity>();
        public readonly Property<float> VerticalSpeed = new Property<float>();
        public readonly Property<float> HorizontalSpeed = new Property<float>();
        public readonly Property<PapersEntity> RightNeighbor = new Property<PapersEntity>();
        public readonly Property<PapersEntity> LeftNeighbor = new Property<PapersEntity>();
        public readonly Property<PapersEntity> TopNeighbor = new Property<PapersEntity>();
        public readonly Property<PapersEntity> BotNeighbor = new Property<PapersEntity>();
        private Color _color;
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                foreach (var item in GetTextures())
                {
                    item.Color = value;
                }
            }
        }

        private readonly IHandleUpdates PaperUpdate;

        public PapersEntity(int cellSize) : base(cellSize, cellSize)
        {

            Textures.Add(new EntityTexture("papers", cellSize, cellSize * 2)
            {
                Offset = new Coordinate2D(0, -cellSize)
            });

            var mainCollider = new Collider(this, cellSize - 2, cellSize);
            mainCollider.Position = new Coordinate2D(1, 0);
            Colliders.Add(mainCollider);

            var rightTrigger = new Trigger(this, cellSize - 40, cellSize - 40);
            rightTrigger.Position = new Coordinate2D(cellSize + 10, +25);
            rightTrigger.AddHandlers(new SetTriggeredNeighbor(RightNeighbor.Set, RightNeighbor.SetDefaut, RightNeighbor.IsNull));
            Colliders.Add(rightTrigger);

            var leftTrigger = new Trigger(this, cellSize - 40, cellSize - 40);
            leftTrigger.Position = new Coordinate2D(-25, 25);
            leftTrigger.AddHandlers(new SetTriggeredNeighbor(LeftNeighbor.Set, LeftNeighbor.SetDefaut, LeftNeighbor.IsNull));
            Colliders.Add(leftTrigger);

            var topTrigger = new Trigger(this, cellSize - 40, cellSize - 40);
            topTrigger.Position = new Coordinate2D(20, -25);
            topTrigger.AddHandlers(new SetTriggeredNeighbor(TopNeighbor.Set, TopNeighbor.SetDefaut, TopNeighbor.IsNull));
            Colliders.Add(topTrigger);

            var botTrigger = new Trigger(this, cellSize - 40, cellSize - 40);
            botTrigger.Position = new Coordinate2D(20, +75);
            botTrigger.AddHandlers(new SetTriggeredNeighbor(BotNeighbor.Set, BotNeighbor.SetDefaut, BotNeighbor.IsNull));
            Colliders.Add(botTrigger);

            PaperUpdate = new UpdateHandlerAggregator(
              new ComputeCombos(new HorizontalNeighborChecker().GetNeighborsCombo)
             , new ComputeCombos(new VerticalNeighborChecker().GetNeighborsCombo)
             , new GravityIncreasesVerticalSpeed(
                 VerticalSpeed.Get, 
                 VerticalSpeed.Set,
                 Grounded.Get)
             , new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get)
             , new FollowOtherEntity(new Coordinate2D(-20, -Height), Target.Get, VerticalSpeed.Set, HorizontalSpeed.Set)
             , new CheckIfGrounded(botTrigger.GetEntities, Grounded.Set)
            );
        }

        protected override void OnUpdate()
        {
            PaperUpdate.Update(this);
        }
    }
}
