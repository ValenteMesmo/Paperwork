using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Player.Collisions;
using PaperWork.PlayerHandlers.Collisions;
using PaperWork.PlayerHandlers.Updates;
using System;

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

        public PlayerEntity(InputRepository PlayerInputs, Action<Entity> DestroyEntity) : base(DestroyEntity)
        {
            var width = 20;
            var height = 100;

            var mainCollider = new Collider(this, width, height);

            var rightGrab = new Collider(this, 30, 20);
            rightGrab.Position = new Coordinate2D(10, 50);
            rightGrab.AddHandlers(
                new GrabPapersOnCollision(PlayerInputs.Grab.Get, currentPapers.IsNull, DragAndDropCooldown.CooldownEnded, DragAndDropCooldown.TriggerCooldown, currentPapers.Set)
            );

            Textures.Add(new EntityTexture("char", 50, 100)
            {
                Offset = new Coordinate2D(-15, 0)
            });

            AddUpdateHandlers(
                new SpeedUpHorizontallyOnInput(HorizontalSpeed.Set, PlayerInputs.Left.Get, PlayerInputs.Right.Get)
                , new JumpOnInputDecreasesVerticalSpeed(SteppingOnTheFloor.Get, VerticalSpeed.Set, PlayerInputs.Jump.Get)
                , new GravityIncreasesVerticalSpeed(VerticalSpeed.Get, VerticalSpeed.Set)
                , new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get)
                , new ForbidJumpIfVerticalSpeedNotZero(SteppingOnTheFloor.Set, VerticalSpeed.Get)
                , new DropThePapers(currentPapers.HasValue, PlayerInputs.Grab.Get, DragAndDropCooldown.CooldownEnded, DragAndDropCooldown.TriggerCooldown, currentPapers.SetDefaut, () => currentPapers.Get().Drop())
            );

            mainCollider.AddHandlers(
                new StopsWhenHitsPapers(SteppingOnTheFloor.Set, VerticalSpeed.Set)
                , new MoveBackWhenHittingWall()
            );

            Colliders.Add(mainCollider);
            Colliders.Add(rightGrab);
        }
    }
}

