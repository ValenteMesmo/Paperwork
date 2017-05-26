using GameCore;
using GameCore.Collision;
using Microsoft.Xna.Framework;
using PaperWork.GameEntities.Collisions;
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

            var botTrigger = new Trigger(this, cellSize - 40, cellSize - 40);
            botTrigger.Position = new Coordinate2D(20, +75);
            Colliders.Add(botTrigger);

            PaperUpdate = new UpdateHandlerAggregator(
              new GravityIncreasesVerticalSpeed(
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
