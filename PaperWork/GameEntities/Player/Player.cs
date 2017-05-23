using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Player.Collisions;
using PaperWork.GameEntities.Player.Updates;
using PaperWork.PlayerHandlers.Collisions;
using PaperWork.PlayerHandlers.Updates;
using System.Collections.Generic;

namespace PaperWork
{
    public class PlayerEntity : Entity
    {
        private readonly Property<Entity> RightEntity = new Property<Entity>();
        private readonly Property<Entity> BotRightEntity = new Property<Entity>();
        private readonly Property<Entity> BotEnity = new Property<Entity>();
        private readonly Property<Entity> LeftEnity = new Property<Entity>();
        private readonly Property<Entity> BotLeftEnity = new Property<Entity>();

        private readonly Property<bool> FacingRightDirection = new Property<bool>();
        private readonly Property<bool> Grounded = new Property<bool>();

        private readonly Property<float> HorizontalSpeed = new Property<float>();
        private readonly Property<float> VerticalSpeed = new Property<float>();

        private readonly List<EntityTexture> TextureLeft = new List<EntityTexture>();

        private IHandleUpdates CurrentState;

        public PlayerEntity(InputRepository Inputs) : base()
        {
            FacingRightDirection.Set(true);

            var width = 20;
            var height = 100;

            var mainCollider = new Collider(this, width, height);

            Textures.Add(new EntityTexture("char", 50, 100)
            {
                Offset = new Coordinate2D(-15, 0)
            });

            TextureLeft.Add(new EntityTexture("char_left", 50, 100)
            {
                Offset = new Coordinate2D(-15, 0)
            });

            CreateFeeler(Inputs, width + 30, 20, RightEntity);
            CreateFeeler(Inputs, width + 30, 75, BotRightEntity);
            var botTrigger = CreateFeeler(Inputs, 5, 125, BotEnity, false);
            CreateFeeler(Inputs, -width - 20, 75, BotLeftEnity, false);
            CreateFeeler(Inputs, -width - 20, 20, LeftEnity, false);

            var mainState = new UpdateHandlerAggregator(
                new JumpOnInputDecreasesVerticalSpeed(
                    Grounded.Get
                    , VerticalSpeed.Set
                    , () => Inputs.Up)
               , new UsesSpeedToMove(
                    HorizontalSpeed.Get,
                    VerticalSpeed.Get)
                , new SpeedUpHorizontallyOnInput(
                    HorizontalSpeed.Set,
                    HorizontalSpeed.Get,
                    () => Inputs.Left,
                    () => Inputs.Right,
                    Grounded.Get)
                , new SetDirectionOnInput(
                    () => Inputs.Right,
                    () => Inputs.Left,
                    FacingRightDirection.Set)
                , new GravityIncreasesVerticalSpeed(
                    VerticalSpeed.Get
                    , VerticalSpeed.Set
                    , Grounded.Get)
                , new DragAndDropHandler(
                    Inputs
                    , FacingRightDirection.Get
                    , Grounded.Get
                    , RightEntity.Get
                    , BotRightEntity.Get
                    , LeftEnity.Get
                    , BotLeftEnity.Get
                    , BotEnity.Get)
                , new CheckIfGrounded(
                        botTrigger.GetEntities,
                        Grounded.Set,
                        mainCollider.Height)
                , new ZeroVerticalSpeedIfGrounded(
                    Grounded.Get
                    , VerticalSpeed.Set
                )
            );

            CurrentState = mainState;


            mainCollider.AddHandlers(
                new HandleCollisionWithSolidObjects(
                    //VerticalSpeed.Set,
                    HorizontalSpeed.Set)
            );

            Colliders.Add(mainCollider);
        }

        protected override void OnUpdate()
        {
            CurrentState.Update(this);
        }

        private Trigger CreateFeeler(InputRepository Inputs, int x, int y, Property<Entity> entityBeenFelt, bool respectDirection = true)
        {
            var trigger = new Trigger(this, 10, 10);
            trigger.LocalPosition = new Coordinate2D(x, y);
            trigger.AddHandlers(
                new SetNearEntityOnTriggerEnter(
                    entityBeenFelt.Set,
                    entityBeenFelt.Get)
            );
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