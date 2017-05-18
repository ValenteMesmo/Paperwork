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
        private readonly Property<Entity> NearEntity = new Property<Entity>();
        private readonly Property<Entity> AlternativeNearEntity = new Property<Entity>();
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
                    GetNearPaper: NearEntity.Get,
                    GrabButtonPressed: Inputs.Action1.Get,
                    GrabCooldownEnded: DragAndDropCooldown.CooldownEnded,
                    SetGrabOnCooldown: DragAndDropCooldown.TriggerCooldown,
                    PlayerHandsAreFree: DraggedEntity.IsNull,
                    GivePaperToPlayer: DraggedEntity.Set,
                    GetAlternativeNearPaper: AlternativeNearEntity.Get,
                    GetVerticalSpeed: VerticalSpeed.Get)
                , new GravityIncreasesVerticalSpeed(VerticalSpeed.Get, VerticalSpeed.Set)
                , new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get)
                , new ForbidJumpIfVerticalSpeedNotZero(SteppingOnTheFloor.Set, VerticalSpeed.Get)
                , new DropThePapers(DraggedEntity.Get, Inputs.Action1.Get, DragAndDropCooldown.CooldownEnded, DragAndDropCooldown.TriggerCooldown, DraggedEntity.SetDefaut, FacingRightDirection.Get, NearEntity.IsNull, AlternativeNearEntity.IsNull, () => VerticalSpeed.Get() != 0,Inputs.Down.Get)
            );

            CreateFeeler(Inputs, 30, 20, NearEntity);
            CreateFeeler(Inputs, 30, 75, AlternativeNearEntity);

            mainCollider.AddHandlers(
                new StopsWhenHitsPapers(SteppingOnTheFloor.Set, VerticalSpeed.Set)
                , new MoveBackWhenHittingWall()
            );

            Colliders.Add(mainCollider);
        }

        private void CreateFeeler(InputRepository Inputs, int x, int y, Property<Entity> entityBeenFelt)
        {
            var trigger = new Trigger(this, 10, 10);
            trigger.LocalPosition = new Coordinate2D(x, y);
            trigger.AddHandlers(
                new SetNearEntityOnTriggerEnter(entityBeenFelt.Set, entityBeenFelt.Get)
            );
            Colliders.Add(trigger);
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

