using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities;
using PaperWork.GameEntities.Papers.CollisionHandlers;
using PaperWork.GameEntities.Player.Updates;
using PaperWork.PlayerHandlers.Updates;
using System;
using System.Collections.Generic;

namespace PaperWork
{
    public class ExecuteOnUpdate : IHandleUpdates
    {
        private readonly Action Action;

        public ExecuteOnUpdate(Action Action)
        {
            this.Action = Action;
        }

        public void Update()
        {
            Action();
        }
    }

    public class ExecuteOnBotCollision<T> : IHandleCollision
    {
        private readonly Action Action;

        public ExecuteOnBotCollision(Action Action)
        {
            this.Action = Action;
        }

        public void BotCollision(Collider Self, Collider Other)
        {
            if (Other.ParentEntity is T)
                Action();
        }

        public void LeftCollision(Collider Self, Collider Other) { }
        public void RightCollision(Collider Self, Collider Other) { }
        public void TopCollision(Collider Self, Collider Other) { }
    }

    public class PlayerEntity : Entity
    {
        private readonly Property<bool> FacingRightDirection = new Property<bool>();

        private readonly Property<bool> Grounded = new Property<bool>();
        private readonly Property<Entity> LeftWall = new Property<Entity>();
        private readonly Property<Entity> RightWall = new Property<Entity>();
        private readonly Property<Entity> BotLeftWall = new Property<Entity>();
        private readonly Property<Entity> BotRightWall = new Property<Entity>();
        private readonly Property<Entity> RoofTop = new Property<Entity>();

        private readonly List<EntityTexture> TextureLeft = new List<EntityTexture>();

        private IHandleUpdates CurrentState;
        const int WIDTH = 40;
        const int HEIGHT = 100;

        Action ZeroHorizontalSpeed;
        Action ZeroVerticalSpeed;
        Action<float> SetVerticalSpeed;
        Action<float> SetHorizontalSpeed;
        Func<float> GetVerticalSpeed;
        Func<float> GetHorizontalSpeed;

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

            ZeroHorizontalSpeed = () => Speed = new Coordinate2D(0, Speed.Y);
            ZeroVerticalSpeed = () => Speed = new Coordinate2D(Speed.X, 0);
            SetVerticalSpeed = f => Speed = new Coordinate2D(Speed.X, f);
            SetHorizontalSpeed = f => Speed = new Coordinate2D(f, Speed.Y);
            GetHorizontalSpeed = () => Speed.X;
            GetVerticalSpeed = () => Speed.Y;

            var HorizontalCollider = new BoxCollider(
                    this
                    , WIDTH
                    , HEIGHT
                    , 0
                    , 0
                    , new ExecuteOnBotCollision<SolidBlock>(
                        () => Grounded.Set(true)
                    )
                    , new ZeroHorizontalSpeedWhenHitsLeft<SolidBlock>(
                        ZeroHorizontalSpeed
                    )
                    , new ZeroHorizontalSpeedWhenHitsRight<SolidBlock>(
                        ZeroHorizontalSpeed
                    )
                    , new ZeroVerticalSpeedWhenHitsTop<SolidBlock>(
                        ZeroVerticalSpeed
                    )
                    , new ZeroVerticalSpeedWhenHitsBot<SolidBlock>(
                        ZeroVerticalSpeed
                    )
                );
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
                    SetHorizontalSpeed,
                    GetHorizontalSpeed,
                    () => Inputs.Left,
                    () => Inputs.Right,
                    Grounded.Get
                    )
                , new SetDirectionOnInput(
                    () => Inputs.Right,
                    () => Inputs.Left,
                    FacingRightDirection.Set)
                , new GravityIncreasesVerticalSpeed(
                    GetVerticalSpeed
                    , SetVerticalSpeed)
                , new JumpOnInputDecreasesVerticalSpeed(
                    Grounded.Get
                    , SetVerticalSpeed
                    , () => Inputs.Up)
                , new DragAndDropHandler(
                    this
                    , Inputs
                    , FacingRightDirection.Get
                    , Grounded.Get
                    , rightTrigger.GetEntities
                    , botRightTrigger.GetEntities
                    , leftTrigger.GetEntities
                    , botLeftTrigger.GetEntities
                    , botTrigger.GetEntities
                    , SetHorizontalSpeed
                    , RoofTop.IsNull)
                , new CheckIfNearLeftWall(
                    this
                 , LeftWall.Set
                    , leftTrigger.GetEntities
                )
                , new CheckIfNearRightWall(
                    this
                 , RightWall.Set
                    , rightTrigger.GetEntities
                )
                , new CheckIfNearLeftWall(
                    this
                 , BotLeftWall.Set
                    , botLeftTrigger.GetEntities
                )
                , new CheckIfNearRightWall(
                   this
                 , BotRightWall.Set
                    , botRightTrigger.GetEntities
                )
                , new CheckIfNearRoofTop(
                    this
                 , topTrigger.GetEntities
                    , RoofTop.Set
                )
                , new ExecuteOnUpdate(
                    Grounded.SetDefaut
                )
            );
        }

        protected override void OnUpdate()
        {
            CurrentState.Update();
        }

        private Trigger CreateTrigger(InputRepository Inputs, int x, int y)
        {
            var trigger = new Trigger(this, x, y, 30, 30);
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