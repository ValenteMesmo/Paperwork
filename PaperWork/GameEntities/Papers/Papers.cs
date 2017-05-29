using GameCore;
using GameCore.Collision;
using Microsoft.Xna.Framework;
using PaperWork.GameEntities;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Player.Updates;
using PaperWork.PlayerHandlers.Updates;
using System;

namespace PaperWork
{
    public class ZeroSpeedWhenHittingBot<T> : IHandleUpdates
    {
        private readonly Collider Collider;
        private readonly Action ZeroVerticalSpeed;

        public ZeroSpeedWhenHittingBot(
            Collider Collider
            , Action ZeroVerticalSpeed)
        {
            this.Collider = Collider;
            this.ZeroVerticalSpeed = ZeroVerticalSpeed;
        }

        public void Update(Entity entity)
        {
            foreach (var collision in Collider.GetBotCollisions())
            {
                if (collision.Source.ParentEntity is T
                    && collision.Difference.Between(-1, 6))
                {
                    Collider.ParentEntity.Position =
                        new Coordinate2D(
                            Collider.ParentEntity.Position.X
                            , collision.Source.ParentEntity.Position.Y - Collider.ParentEntity.Height);
                    ZeroVerticalSpeed();
                }
            }
        }
    }

    public class ZeroSpeedWhenHittingTop<T> : IHandleUpdates
    {
        private readonly Collider Collider;
        private readonly Action ZeroVerticalSpeed;

        public ZeroSpeedWhenHittingTop(
            Collider Collider
            , Action ZeroVerticalSpeed)
        {
            this.Collider = Collider;
            this.ZeroVerticalSpeed = ZeroVerticalSpeed;
        }

        public void Update(Entity entity)
        {
            foreach (var collision in Collider.GetTopCollisions())
            {
                //if (collision.Source. ParentEntity is T
                //    && collision.Difference.Between(-2, 2))
                //{
                //    Collider.ParentEntity.Position =
                //        new Coordinate2D(
                //            Collider.ParentEntity.Position.X
                //            , collision.Source.ParentEntity.Position.Y + collision.Source.ParentEntity.Height);
                //    ZeroVerticalSpeed();
                //}
            }
        }
    }

    public class ZeroSpeedWhenHittingLeft<T> : IHandleUpdates
    {
        private readonly Collider Collider;
        private readonly Action ZeroHorizontalSpeed;

        public ZeroSpeedWhenHittingLeft(
            Collider Collider
            , Action ZeroHorizontalSpeed)
        {
            this.Collider = Collider;
            this.ZeroHorizontalSpeed = ZeroHorizontalSpeed;
        }

        public void Update(Entity entity)
        {
            foreach (var collision in Collider.GetLeftCollisions())
            {
                if (collision.Source.ParentEntity is T
                    && collision.Difference < 2)
                {
                    Collider.ParentEntity.Position =
                        new Coordinate2D(
                            collision.Source.ParentEntity.Position.X + collision.Source.ParentEntity.Width
                            , Collider.ParentEntity.Position.Y);

                    ZeroHorizontalSpeed();
                }
            }
        }
    }

    public class ZeroSpeedWhenHittingRight<T> : IHandleUpdates
    {
        private readonly Collider Collider;
        private readonly Action ZeroHorizontalSpeed;

        public ZeroSpeedWhenHittingRight(
            Collider Collider
            , Action ZeroHorizontalSpeed)
        {
            this.Collider = Collider;
            this.ZeroHorizontalSpeed = ZeroHorizontalSpeed;
        }

        public void Update(Entity entity)
        {
            foreach (var collision in Collider.GetRightCollisions())
            {
                if (collision.Source.ParentEntity is T
                    && collision.Difference < 1)
                {
                    Collider.ParentEntity.Position =
                        new Coordinate2D(
                        collision.Source.ParentEntity.Position.X - Collider.ParentEntity.Width
                        , Collider.ParentEntity.Position.Y);

                    ZeroHorizontalSpeed();
                }
            }
        }
    }

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
        private readonly Collider mainCollider;

        public PapersEntity(int cellSize) : base(cellSize, cellSize)
        {

            Textures.Add(new EntityTexture("papers", cellSize, cellSize * 2)
            {
                Offset = new Coordinate2D(0, -cellSize)
            });

            mainCollider = new Collider(this, cellSize - 2, cellSize - 2, 1, 1);
            Colliders.Add(mainCollider);

            var botTrigger = new Collider(this, cellSize - 40, cellSize - 40, 20, +75, true);
            Colliders.Add(botTrigger);

            PaperUpdate = new UpdateHandlerAggregator(
              new GravityIncreasesVerticalSpeed(
                 VerticalSpeed.Get,
                 VerticalSpeed.Set,
                 Grounded.Get)
             , new ZeroSpeedWhenHittingBot<SolidBlock>(mainCollider, VerticalSpeed.SetDefaut)
             , new ZeroSpeedWhenHittingBot<PapersEntity>(mainCollider, VerticalSpeed.SetDefaut)
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
