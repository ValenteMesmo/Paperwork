using GameCore;
using GameCore.Collision;
using Microsoft.Xna.Framework;
using PaperWork.GameEntities;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Papers.CollisionHandlers;
using PaperWork.PlayerHandlers.Updates;

namespace PaperWork
{
    static class EntityExtensions
    {
        public static void ZeroHorizontalSpeed(this Entity entity)
        {
             entity.Speed = new Coordinate2D(0, entity.Speed.Y);
        }

        public static void ZeroVerticalSpeed(this Entity entity)
        {
             entity.Speed = new Coordinate2D(entity.Speed.X, 0);
        }

        public static void SetVerticalSpeed(this Entity entity, float f)
        {
             entity.Speed = new Coordinate2D(entity.Speed.X, f);
        }

        public static void SetHorizontalSpeed(this Entity entity, float f)
        {
             entity.Speed = new Coordinate2D(f, entity.Speed.Y);
        }

        public static float GetHorizontalSpeed(this Entity entity)
        {
            return  entity.Speed.X;
        }

        public static float GetVerticalSpeed(this Entity entity)
        {
            return  entity.Speed.Y;
        }
    }

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
                 new Coordinate2D(-20, -Height)
                 , Target.Get
                 , this.SetVerticalSpeed
                 , this.SetHorizontalSpeed)
            );
        }

        protected override void OnUpdate()
        {
            PaperUpdate.Update(this);
        }
    }
}
