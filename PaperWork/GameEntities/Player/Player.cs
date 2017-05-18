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

            var feeller = new Trigger(this, 10, 10);
            feeller.LocalPosition = new Coordinate2D(30, 20);
            feeller.AddHandlers(
                new SetNearEntityOnTriggerEnter(NearEntity.Set, NearEntity.Get)
            );
            Colliders.Add(feeller);

            var alternativeFeeller = new Trigger(this, 10, 10);
            alternativeFeeller.LocalPosition = new Coordinate2D(30, 75);
            alternativeFeeller.AddHandlers(
                new SetNearEntityOnTriggerEnter(AlternativeNearEntity.Set, AlternativeNearEntity.Get)
            );
            Colliders.Add(alternativeFeeller);
            
            AddUpdateHandlers(
                new SpeedUpHorizontallyOnInput(HorizontalSpeed.Set, Inputs.Left.Get, Inputs.Right.Get)
                , new SetDirectionOnInput(Inputs.Right.Get, Inputs.Left.Get, FacingRightDirection.Set)
                , new JumpOnInputDecreasesVerticalSpeed(SteppingOnTheFloor.Get, VerticalSpeed.Set, Inputs.Jump.Get)
                , new DragNearPaperOnInput(NearEntity.Get, Inputs.Grab.Get, DragAndDropCooldown.CooldownEnded, DragAndDropCooldown.TriggerCooldown, DraggedEntity.IsNull, DraggedEntity.Set, AlternativeNearEntity.Get,VerticalSpeed.Get)
                , new GravityIncreasesVerticalSpeed(VerticalSpeed.Get, VerticalSpeed.Set)
                , new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get)
                , new ForbidJumpIfVerticalSpeedNotZero(SteppingOnTheFloor.Set, VerticalSpeed.Get)
                , new DropThePapers(DraggedEntity.Get, Inputs.Grab.Get, DragAndDropCooldown.CooldownEnded, DragAndDropCooldown.TriggerCooldown, DraggedEntity.SetDefaut, FacingRightDirection.Get, NearEntity.IsNull, AlternativeNearEntity.IsNull,()=> VerticalSpeed.Get() != 0)
                , new SetGrabColliderPosition(Inputs.Right.Get, Inputs.Left.Get, f => feeller.LocalPosition = f, ()=>feeller.LocalPosition)
                , new SetGrabColliderPosition(Inputs.Right.Get, Inputs.Left.Get, f => alternativeFeeller.LocalPosition = f, () => alternativeFeeller.LocalPosition)
            );

            mainCollider.AddHandlers(
                new StopsWhenHitsPapers(SteppingOnTheFloor.Set, VerticalSpeed.Set)
                , new MoveBackWhenHittingWall()
            );

            Colliders.Add(mainCollider);
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

