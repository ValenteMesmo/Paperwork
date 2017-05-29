using GameCore;
using GameCore.Collision;
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
                    , WIDTH - 20
                    , Height + 30
                    , 10
                    , -10
                );
            HorizontalCollider = new Collider(
                    this
                    , WIDTH +20
                    , Height - 20
                    , -10
                    , 10
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
                centerTrigger.GetEtities
            );
        }

        private UpdateHandlerAggregator CreateMainState(
            InputRepository Inputs
            , Trigger rightTrigger
            , Trigger botRightTrigger
            , Trigger botTrigger
            , Trigger topTrigger
            , Trigger botLeftTrigger
            , Trigger leftTrigger
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
               
                , new DragAndDropHandler(
                    Inputs
                    , FacingRightDirection.Get
                    , Grounded.HasValue
                    , rightTrigger.GetEtities
                    , botRightTrigger.GetEtities
                    , leftTrigger.GetEtities
                    , botLeftTrigger.GetEtities
                    , botTrigger.GetEtities
                    , HorizontalSpeed.Set
                    , RoofTop.IsNull)
                , new CheckIfGrounded(
                    botTrigger.GetEtities
                    , Grounded.Set)
                , new CheckIfNearLeftWall(
                    LeftWall.Set
                    , leftTrigger.GetEtities
                )
                , new CheckIfNearRightWall(
                    RightWall.Set
                    , rightTrigger.GetEtities
                )
                , new CheckIfNearLeftWall(
                    BotLeftWall.Set
                    , botLeftTrigger.GetEtities
                )
                , new CheckIfNearRightWall(
                    BotRightWall.Set
                    , botRightTrigger.GetEtities
                )
                , new CheckIfNearRoofTop(
                    topTrigger.GetEtities
                    , RoofTop.Set
                )              
                , new UsesSpeedToMove(
                    HorizontalSpeed.Get,
                    VerticalSpeed.Get)                
                //, new ZeroSpeedWhenHittingTop<SolidBlock>(VerticalCollider, VerticalSpeed.SetDefaut)
                //, new ZeroSpeedWhenHittingLeft<PapersEntity>(HorizontalCollider, HorizontalSpeed.SetDefaut)
                //, new ZeroSpeedWhenHittingLeft<SolidBlock>(HorizontalCollider, HorizontalSpeed.SetDefaut)
                //, new ZeroSpeedWhenHittingRight<PapersEntity>(HorizontalCollider, HorizontalSpeed.SetDefaut)
                //, new ZeroSpeedWhenHittingRight<SolidBlock>(HorizontalCollider, HorizontalSpeed.SetDefaut)
            );
        }

        protected override void OnUpdate()
        {
            CurrentState.Update(this);
        }

        private Trigger CreateTrigger(InputRepository Inputs, int x, int y)
        {
            var trigger = new Trigger(this, 30, 30, x, y);
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