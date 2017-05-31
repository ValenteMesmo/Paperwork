using GameCore;
using GameCore.Collision;
using Microsoft.Xna.Framework;
using PaperWork.GameEntities;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Papers.CollisionHandlers;
using PaperWork.PlayerHandlers.Updates;

namespace PaperWork
{
    public class PapersEntity : Entity
    {
        public readonly Property<Entity> Target = new Property<Entity>();
        public readonly Property<bool> Grounded = new Property<bool>();
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
        private readonly Collider mainCollider;

        public PapersEntity(
            int cellSize) : base(cellSize, cellSize)
        {

            Textures.Add(new EntityTexture("papers", cellSize, cellSize * 2)
            {
                Offset = new Coordinate2D(0, -cellSize)
            });

            mainCollider = new BoxCollider(this, cellSize, cellSize, 0, 0
                , new ZeroVerticalSpeedWhenHitsBot<SolidBlock>(
                    this.ZeroVerticalSpeed)
                , new ZeroVerticalSpeedWhenHitsBot<PapersEntity>(
                    this.ZeroVerticalSpeed)
            );
            Colliders.Add(mainCollider);

            PaperUpdate = new UpdateHandlerAggregator(
              new GravityIncreasesVerticalSpeed(
                 this.GetVerticalSpeed,
                 this.SetVerticalSpeed)
             , new FollowOtherEntity(
                 this
                 ,new Coordinate2D(-20, -Height)
                 , Target.Get
                 , this.SetVerticalSpeed
                 , this.SetHorizontalSpeed)
            );
        }

        protected override void OnUpdate()
        {
            PaperUpdate.Update();
        }
    }
}
