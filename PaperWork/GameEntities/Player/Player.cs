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
        private readonly Property<PapersEntity> DraggedEntity = new Property<PapersEntity>();

        private readonly Property<Entity> RightEntity = new Property<Entity>();
        private readonly Property<Entity> BotRightEntity = new Property<Entity>();
        private readonly Property<Entity> BotEnity = new Property<Entity>();
        private readonly Property<Entity> LeftEnity = new Property<Entity>();
        private readonly Property<Entity> BotLeftEnity = new Property<Entity>();

        private readonly Property<bool> SteppingOnTheFloor = new Property<bool>();
        private readonly Property<bool> FacingRightDirection = new Property<bool>();

        private readonly Property<float> HorizontalSpeed = new Property<float>();
        private readonly Property<float> VerticalSpeed = new Property<float>();

        private readonly Cooldown DragAndDropCooldown = new Cooldown(200);
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

            var grounded = new UpdateHandlerAggragator(
                new UsesSpeedToMove(
                    HorizontalSpeed.Get, 
                    VerticalSpeed.Get)
                , new SpeedUpHorizontallyOnInput(
                    HorizontalSpeed.Set, 
                    Inputs.Left_Pressed, 
                    Inputs.Right_Pressed)
                , new SetDirectionOnInput(
                    Inputs.Right_Pressed, 
                    Inputs.Left_Pressed, 
                    FacingRightDirection.Set)
                , new JumpOnInputDecreasesVerticalSpeed(
                    SteppingOnTheFloor.Get, 
                    VerticalSpeed.Set, 
                    Inputs.Up_Pressed)
                , new DragNearPaperOnInput(
                    GrabButtonPressed: Inputs.Action1_JustPressed
                    , PlayerHandsAreFree: DraggedEntity.IsNull
                    , GrabCooldownEnded: DragAndDropCooldown.Ended
                    , DownButtonPressed: Inputs.Down_Pressed
                    , PlayerFacingRight: FacingRightDirection.Get
                    , GetRightEntity: RightEntity.Get
                    , GetVerticalSpeed: VerticalSpeed.Get
                    , GetBotEntity: BotEnity.Get
                    , GetLeftEntity: LeftEnity.Get
                    , GetBotLeftEntity: BotLeftEnity.Get
                    , GivePaperToPlayer: DraggedEntity.Set
                    , SetGrabOnCooldown: DragAndDropCooldown.TriggerCooldown
                    , GetBotRightEntity: BotRightEntity.Get
                    , PlayerFalling: BotEnity.IsNull)
                , new GravityIncreasesVerticalSpeed(
                    VerticalSpeed.Get, 
                    VerticalSpeed.Set)
                , new ForbidJumpIfVerticalSpeedNotZero(
                    SteppingOnTheFloor.Set, 
                    BotEnity.False)
                , new DropThePapers(
                    DraggedEntity.Get,
                    Inputs.Action1_JustPressed,
                    DragAndDropCooldown.Ended,
                    DragAndDropCooldown.TriggerCooldown,
                    DraggedEntity.SetDefaut,
                    RightEntity.IsNull,
                    Inputs.Down_Pressed,
                    FacingRightDirection.False,
                    50)
                , new DropThePapers(
                    DraggedEntity.Get,
                    Inputs.Action1_JustPressed,
                    DragAndDropCooldown.Ended,
                    DragAndDropCooldown.TriggerCooldown,
                    DraggedEntity.SetDefaut,
                    LeftEnity.IsNull,
                    Inputs.Down_Pressed,
                    FacingRightDirection.True,
                    -40)
            );

            CurrentState = grounded;

            CreateFeeler(Inputs, width + 30, 20, RightEntity);
            CreateFeeler(Inputs, width + 30, 75, BotRightEntity);
            CreateFeeler(Inputs, 5, 125, BotEnity, false);
            CreateFeeler(Inputs, -width - 20, 75, BotLeftEnity, false);
            CreateFeeler(Inputs, -width - 20, 20, LeftEnity, false);

            mainCollider.AddHandlers(
                new HandleCollisionWithSolidObjects(
                    SteppingOnTheFloor.Set, 
                    VerticalSpeed.Set, 
                    HorizontalSpeed.Set)
            );

            Colliders.Add(mainCollider);
        }

        protected override void OnUpdate()
        {
            CurrentState.Update(this);
        }

        private void CreateFeeler(InputRepository Inputs, int x, int y, Property<Entity> entityBeenFelt, bool respectDirection = true)
        {
            var trigger = new Trigger(this, 10, 10);
            trigger.LocalPosition = new Coordinate2D(x, y);
            trigger.AddHandlers(
                new SetNearEntityOnTriggerEnter(
                    entityBeenFelt.Set, 
                    entityBeenFelt.Get)
            );
            Colliders.Add(trigger);
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