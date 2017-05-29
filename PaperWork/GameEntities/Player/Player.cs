using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities;
using PaperWork.GameEntities.Player.Updates;
using PaperWork.PlayerHandlers.Updates;
using System;
using System.Collections.Generic;

namespace PaperWork
{
    public class PlayerEntity : Entity
    {
        private readonly Property<bool> FacingRightDirection = new Property<bool>();

        private readonly Property<Entity> Grounded = new Property<Entity>();
        private readonly Property<Entity> LeftWall = new Property<Entity>();
        private readonly Property<Entity> RightWall = new Property<Entity>();
        private readonly Property<Entity> BotLeftWall = new Property<Entity>();
        private readonly Property<Entity> BotRightWall = new Property<Entity>();
        private readonly Property<Entity> RoofTop = new Property<Entity>();

        private readonly Property<float> HorizontalSpeed = new Property<float>();
        private readonly Property<float> VerticalSpeed = new Property<float>();

        private readonly List<EntityTexture> TextureLeft = new List<EntityTexture>();

        private IHandleUpdates CurrentState;
        const int WIDTH = 40;
        const int HEIGHT = 100;
        private readonly Collider VerticalCollider;
        private readonly Collider HorizontalCollider;

        public PlayerEntity(InputRepository Inputs) : base(WIDTH, HEIGHT)
        {
            FacingRightDirection.Set(true);

            Textures.Add(new EntityTexture("char", 50, 100)
            {
                Offset = new Coordinate2D(-5, 0)
            });

            TextureLeft.Add(new EntityTexture("char_left", 50, 100)
            {
                Offset = new Coordinate2D(-5, 0)
            });

            VerticalCollider = new Collider(
                    this
                    , WIDTH - 2
                    , Height + 30
                    , 1
                    , -10
                );
            HorizontalCollider = new Collider(
                    this
                    , WIDTH +20
                    , Height - 2
                    , -10
                    , 1
                );

            Colliders.Add(VerticalCollider);
            Colliders.Add(HorizontalCollider);

            var rightTrigger = CreateTrigger(Inputs, Width, 10);
            var botRightTrigger = CreateTrigger(Inputs, Width, 65);
            var botTrigger = CreateTrigger(Inputs, 5, 100);
            var topTrigger = CreateTrigger(Inputs, 5, -30);
            var botLeftTrigger = CreateTrigger(Inputs, -Width + 10, 65);
            var leftTrigger = CreateTrigger(Inputs, -Width + 10, 10);
            var centerTrigger = CreateTrigger(Inputs, 5, 38);

            CurrentState = CreateMainState(
                Inputs,
                rightTrigger,
                botRightTrigger,
                botTrigger,
                topTrigger,
                botLeftTrigger,
                leftTrigger,
                centerTrigger.GetEntities
            );
        }

        private UpdateHandlerAggregator CreateMainState(
            InputRepository Inputs
            , Collider rightTrigger
            , Collider botRightTrigger
            , Collider botTrigger
            , Collider topTrigger
            , Collider botLeftTrigger
            , Collider leftTrigger
            , Func<IEnumerable<Entity>> objectsInsideTHePlayer)
        {
            return new UpdateHandlerAggregator(
                 new SpeedUpHorizontallyOnInput(
                    HorizontalSpeed.Set,
                    HorizontalSpeed.Get,
                    () => Inputs.Left,
                    () => Inputs.Right,
                    Grounded.HasValue
                    )
                , new SetDirectionOnInput(
                    () => Inputs.Right,
                    () => Inputs.Left,
                    FacingRightDirection.Set)
                , new GravityIncreasesVerticalSpeed(
                    VerticalSpeed.Get
                    , VerticalSpeed.Set
                    , Grounded.Get)
                , new JumpOnInputDecreasesVerticalSpeed(
                    Grounded.HasValue
                    , VerticalSpeed.Set
                    , () => Inputs.Up)
                //, new StopsWhenHitsTheRoof<SolidBlock>(
                //    RoofTop.Get
                //    , VerticalSpeed.Set
                //)
                , new ZeroSpeedWhenHittingBot<SolidBlock>(VerticalCollider, VerticalSpeed.SetDefaut)
                , new ZeroSpeedWhenHittingBot<PapersEntity>(VerticalCollider, VerticalSpeed.SetDefaut)
                , new ZeroSpeedWhenHittingTop<SolidBlock>(VerticalCollider, VerticalSpeed.SetDefaut)
                , new ZeroSpeedWhenHittingLeft<PapersEntity>(HorizontalCollider, HorizontalSpeed.SetDefaut)
                , new ZeroSpeedWhenHittingLeft<SolidBlock>(HorizontalCollider, HorizontalSpeed.SetDefaut)
                , new ZeroSpeedWhenHittingRight<PapersEntity>(HorizontalCollider, HorizontalSpeed.SetDefaut)
                , new ZeroSpeedWhenHittingRight<SolidBlock>(HorizontalCollider, HorizontalSpeed.SetDefaut)
                , new DragAndDropHandler(
                    Inputs
                    , FacingRightDirection.Get
                    , Grounded.HasValue
                    , rightTrigger.GetEntities
                    , botRightTrigger.GetEntities
                    , leftTrigger.GetEntities
                    , botLeftTrigger.GetEntities
                    , botTrigger.GetEntities
                    , HorizontalSpeed.Set
                    , RoofTop.IsNull)
                , new CheckIfGrounded(
                    botTrigger.GetEntities
                    , Grounded.Set)
                , new CheckIfNearLeftWall(
                    LeftWall.Set
                    , leftTrigger.GetEntities
                )
                , new CheckIfNearRightWall(
                    RightWall.Set
                    , rightTrigger.GetEntities
                )
                , new CheckIfNearLeftWall(
                    BotLeftWall.Set
                    , botLeftTrigger.GetEntities
                )
                , new CheckIfNearRightWall(
                    BotRightWall.Set
                    , botRightTrigger.GetEntities
                )
                , new CheckIfNearRoofTop(
                    topTrigger.GetEntities
                    , RoofTop.Set
                )
                , new UsesSpeedToMove(
                    HorizontalSpeed.Get,
                    VerticalSpeed.Get)
            );
        }

        protected override void OnUpdate()
        {
            CurrentState.Update(this);
        }

        private Collider CreateTrigger(InputRepository Inputs, int x, int y)
        {
            var trigger = new Collider(this, 30, 30, x, y, true);
            Colliders.Add(trigger);
            return trigger;
        }

        public override IEnumerable<EntityTexture> GetTextures()
        {
            if (FacingRightDirection.Get())
                return Textures;
            else
                return TextureLeft;
        }
    }
}