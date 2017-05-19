using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Player.Collisions;
using PaperWork.GameEntities.Player.Updates;
using PaperWork.PlayerHandlers.Collisions;
using PaperWork.PlayerHandlers.Updates;
using System;
using System.Collections.Generic;

namespace PaperWork
{
    public class PlayerEntity : Entity
    {
        //bug: sometimes dragged entity is not null.... even when no paper above
        private readonly Property<Entity> DraggedEntity = new Property<Entity>();
        private readonly Property<Entity> EntityNearFace = new Property<Entity>();
        private readonly Property<Entity> EntityNearBelly = new Property<Entity>();
        private readonly Property<Entity> EntityBelowFeet = new Property<Entity>();
        private readonly Property<bool> SteppingOnTheFloor = new Property<bool>();
        private readonly Property<bool> FacingRightDirection = new Property<bool>();
        private readonly Property<float> HorizontalSpeed = new Property<float>();
        private readonly Property<float> VerticalSpeed = new Property<float>();
        private readonly Cooldown DragAndDropCooldown = new Cooldown(200);
        private readonly List<EntityTexture> TextureLeft = new List<EntityTexture>();

        public PlayerEntity(InputRepository Inputs, Action<Entity> DestroyEntity) : base(DestroyEntity)
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

            AddUpdateHandlers(
                new SpeedUpHorizontallyOnInput(HorizontalSpeed.Set, Inputs.Left_Pressed, Inputs.Right_Pressed)
                , new SetDirectionOnInput(Inputs.Right_Pressed, Inputs.Left_Pressed, FacingRightDirection.Set)
                , new JumpOnInputDecreasesVerticalSpeed(SteppingOnTheFloor.Get, VerticalSpeed.Set, Inputs.Up_Pressed)
                , new DragNearPaperOnInput(
                    GetNearPaper: EntityNearFace.Get
                    , GeEntityBelowFeet: EntityBelowFeet.Get
                    , GrabButtonPressed: Inputs.Action1_JustPressed
                    , GrabCooldownEnded: DragAndDropCooldown.CooldownEnded
                    , SetGrabOnCooldown: DragAndDropCooldown.TriggerCooldown
                    , PlayerHandsAreFree: DraggedEntity.IsNull
                    , GivePaperToPlayer: DraggedEntity.Set
                    , GetAlternativeNearPaper: EntityNearBelly.Get
                    , GetVerticalSpeed: VerticalSpeed.Get
                    , DownButtonPressed: Inputs.Down_Pressed)
                , new GravityIncreasesVerticalSpeed(VerticalSpeed.Get, VerticalSpeed.Set)
                , new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get)
                , new ForbidJumpIfVerticalSpeedNotZero(SteppingOnTheFloor.Set, VerticalSpeed.Get)
                , new DropThePapers(DraggedEntity.Get, Inputs.Action1_JustPressed, DragAndDropCooldown.CooldownEnded, DragAndDropCooldown.TriggerCooldown, DraggedEntity.SetDefaut, FacingRightDirection.Get, EntityNearFace.IsNull, EntityNearBelly.IsNull, () => VerticalSpeed.Get() != 0, Inputs.Down_Pressed)
            );

            CreateFeeler(Inputs, 30, 20, EntityNearFace);
            CreateFeeler(Inputs, 30, 75, EntityNearBelly);
            CreateFeeler(Inputs, 5, 125, EntityBelowFeet, false);

            mainCollider.AddHandlers(
                new StopsWhenHitsPapers(SteppingOnTheFloor.Set, VerticalSpeed.Set)
                , new MoveBackWhenHittingWall()
            );

            Colliders.Add(mainCollider);
        }

        private void CreateFeeler(InputRepository Inputs, int x, int y, Property<Entity> entityBeenFelt, bool respectDirection = true)
        {
            var trigger = new Trigger(this, 10, 10);
            trigger.LocalPosition = new Coordinate2D(x, y);
            trigger.AddHandlers(
                new SetNearEntityOnTriggerEnter(entityBeenFelt.Set, entityBeenFelt.Get)
            );
            Colliders.Add(trigger);
            if (respectDirection)
                AddUpdateHandlers(new SetGrabColliderPosition(Inputs.Right_Pressed, Inputs.Left_Pressed, f => trigger.LocalPosition = f, () => trigger.LocalPosition));
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

