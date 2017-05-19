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
                new SpeedUpHorizontallyOnInput(HorizontalSpeed.Set, Inputs.Left.Get, Inputs.Right.Get)
                , new SetDirectionOnInput(Inputs.Right.Get, Inputs.Left.Get, FacingRightDirection.Set)
                , new JumpOnInputDecreasesVerticalSpeed(SteppingOnTheFloor.Get, VerticalSpeed.Set, Inputs.Up.Get)
                , new DragNearPaperOnInput(
                    GetNearPaper: EntityNearFace.Get
                    , GeEntityBelowFeet: EntityBelowFeet.Get
                    , GrabButtonPressed: Inputs.Action1.Get
                    , GrabCooldownEnded: DragAndDropCooldown.CooldownEnded
                    , SetGrabOnCooldown: DragAndDropCooldown.TriggerCooldown
                    , PlayerHandsAreFree: DraggedEntity.IsNull
                    , GivePaperToPlayer: DraggedEntity.Set
                    , GetAlternativeNearPaper: EntityNearBelly.Get
                    , GetVerticalSpeed: VerticalSpeed.Get
                    , DownButtonPressed: Inputs.Down.Get)
                , new GravityIncreasesVerticalSpeed(VerticalSpeed.Get, VerticalSpeed.Set)
                , new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get)
                , new ForbidJumpIfVerticalSpeedNotZero(SteppingOnTheFloor.Set, VerticalSpeed.Get)
                , new DropThePapers(DraggedEntity.Get, Inputs.Action1.Get, DragAndDropCooldown.CooldownEnded, DragAndDropCooldown.TriggerCooldown, DraggedEntity.SetDefaut, FacingRightDirection.Get, EntityNearFace.IsNull, EntityNearBelly.IsNull, () => VerticalSpeed.Get() != 0, Inputs.Down.Get)
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
                AddUpdateHandlers(new SetGrabColliderPosition(Inputs.Right.Get, Inputs.Left.Get, f => trigger.LocalPosition = f, () => trigger.LocalPosition));
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

