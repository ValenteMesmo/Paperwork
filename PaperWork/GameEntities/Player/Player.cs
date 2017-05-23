﻿using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Player.Collisions;
using PaperWork.GameEntities.Player.Updates;
using PaperWork.PlayerHandlers.Updates;
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

        private readonly Property<float> HorizontalSpeed = new Property<float>();
        private readonly Property<float> VerticalSpeed = new Property<float>();

        private readonly List<EntityTexture> TextureLeft = new List<EntityTexture>();

        private IHandleUpdates CurrentState;

        public PlayerEntity(InputRepository Inputs) : base(20, 100)
        {
            FacingRightDirection.Set(true);

            Textures.Add(new EntityTexture("char", 50, 100)
            {
                Offset = new Coordinate2D(-15, 0)
            });

            TextureLeft.Add(new EntityTexture("char_left", 50, 100)
            {
                Offset = new Coordinate2D(-15, 0)
            });

            var rightTrigger = CreateFeeler(Inputs, Width + 30, 20);
            var botRightTrigger = CreateFeeler(Inputs, Width + 30, 75);
            var botTrigger = CreateFeeler(Inputs, 5, 125);
            var botLeftTrigger = CreateFeeler(Inputs, -Width - 20, 75);
            var leftTrigger = CreateFeeler(Inputs, -Width - 20, 20);

            var mainState = new UpdateHandlerAggregator(
                 new SpeedUpHorizontallyOnInput(
                    HorizontalSpeed.Set,
                    HorizontalSpeed.Get,
                    () => Inputs.Left,
                    () => Inputs.Right,
                    Grounded.HasValue
                    , LeftWall.Get
                    , RightWall.Get
                    , BotLeftWall.Get
                    , BotRightWall.Get
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
                , new DragAndDropHandler(
                    Inputs
                    , FacingRightDirection.Get
                    , Grounded.HasValue
                    , rightTrigger.GetEntities
                    , botRightTrigger.GetEntities
                    , leftTrigger.GetEntities
                    , botLeftTrigger.GetEntities
                    , botTrigger.GetEntities)
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
                , new UsesSpeedToMove(
                    HorizontalSpeed.Get,
                    VerticalSpeed.Get)
            );

            CurrentState = mainState;
        }

        protected override void OnUpdate()
        {
            CurrentState.Update(this);
        }

        private Trigger CreateFeeler(InputRepository Inputs, int x, int y)
        {
            var trigger = new Trigger(this, 10, 10);
            trigger.LocalPosition = new Coordinate2D(x, y);
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