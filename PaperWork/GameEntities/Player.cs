using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Player.Collisions;
using PaperWork.PlayerHandlers.Collisions;
using PaperWork.PlayerHandlers.Updates;
using System;
using System.Collections.Generic;

namespace PaperWork
{
    public class PlayerEntity : Entity
    {
        private readonly Property<PapersEntity> currentPapers = new Property<PapersEntity>();
        private readonly Property<bool> SteppingOnTheFloor = new Property<bool>();
        private readonly Property<bool> FacingRightDirection = new Property<bool>();
        private readonly Property<float> HorizontalSpeed = new Property<float>();
        private readonly Property<float> VerticalSpeed = new Property<float>();
        private readonly Cooldown DragAndDropCooldown = new Cooldown(200);
        private readonly List<EntityTexture> TextureLeft = new List<EntityTexture>();

        public PlayerEntity(InputRepository Inputs, Action<Entity> DestroyEntity) : base(DestroyEntity)
        {
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
            var rightGrab = new Collider(this, 30, 20);
            rightGrab.Position = new Coordinate2D(10, 50);
            rightGrab.AddHandlers(
                new GrabPapersOnCollision(Inputs.Grab.Get, currentPapers.IsNull, DragAndDropCooldown.CooldownEnded, DragAndDropCooldown.TriggerCooldown, currentPapers.Set)
            );

            Colliders.Add(rightGrab);
            AddUpdateHandlers(
                new SpeedUpHorizontallyOnInput(HorizontalSpeed.Set, Inputs.Left.Get, Inputs.Right.Get)
                , new SetDirectionOnInput(Inputs.Right.Get, Inputs.Left.Get, FacingRightDirection.Set)
                , new JumpOnInputDecreasesVerticalSpeed(SteppingOnTheFloor.Get, VerticalSpeed.Set, Inputs.Jump.Get)
                , new GravityIncreasesVerticalSpeed(VerticalSpeed.Get, VerticalSpeed.Set)
                , new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get)
                , new ForbidJumpIfVerticalSpeedNotZero(SteppingOnTheFloor.Set, VerticalSpeed.Get)
                , new DropThePapers(currentPapers.HasValue, Inputs.Grab.Get, DragAndDropCooldown.CooldownEnded, DragAndDropCooldown.TriggerCooldown, currentPapers.SetDefaut, () => currentPapers.Get().Drop())
                , new SetGrabColliderPosition(Inputs.Right.Get,Inputs.Left.Get, f=>rightGrab.LocalPosition =f)
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

    class SetGrabColliderPosition : IHandleEntityUpdates
    {
        private readonly Func<bool> RightPressed;
        private readonly Func<bool> LeftPressed;
        private readonly Action<Coordinate2D> SetColliderPosition;

        public SetGrabColliderPosition(
            Func<bool> RightPressed
            , Func<bool> LeftPressed
            , Action<Coordinate2D> SetColliderPosition)
        {
            this.RightPressed = RightPressed;
            this.LeftPressed = LeftPressed;
            this.SetColliderPosition = SetColliderPosition;
        }

        public void Update(Entity entity)
        {
            if (RightPressed())
            {
                SetColliderPosition(new Coordinate2D(10, 50));
            }
            else if (LeftPressed())
            {
               SetColliderPosition(new Coordinate2D(-20,50));
            }
        }
    }

    class SetDirectionOnInput : IHandleEntityUpdates
    {
        private readonly Func<bool> RightPressed;
        private readonly Func<bool> LeftPressed;
        private readonly Action<bool> SetFacingRight;

        public SetDirectionOnInput(
            Func<bool> RightPressed
            , Func<bool> LeftPressed
            , Action<bool> SetFacingRight)
        {
            this.RightPressed = RightPressed;
            this.LeftPressed = LeftPressed;
            this.SetFacingRight = SetFacingRight;
        }

        public void Update(Entity entity)
        {
            if (RightPressed())
                SetFacingRight(true);
            if (LeftPressed())
                SetFacingRight(false);
        }
    }
}

