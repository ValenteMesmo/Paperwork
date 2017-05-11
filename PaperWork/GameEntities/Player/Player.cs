using System;
using GameCore;
using GameCore.Collision;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Player.Updates;
using PaperWork.PlayerHandlers.Collisions;
using PaperWork.PlayerHandlers.Updates;

namespace PaperWork
{
    public class PlayerEntity : Entity
    {
        private Property<PapersEntity> currentPapers = new Property<PapersEntity>();
        GridPositions Grid;
        private Property<float> HorizontalSpeed = new Property<float>();
        private Property<float> VerticalSpeed = new Property<float>();
        private Property<bool> SteppingOnTheFloor = new Property<bool>();
        private Cooldown DragAndDropCooldown = new Cooldown(500);

        public PlayerEntity(InputRepository PlayerInputs, GridPositions Grid)
        {
            this.Grid = Grid;

            Textures.Add(new EntityTexture("char", 50, 100));

            UpdateHandlers.Add(new SpeedUpHorizontallyOnInput(HorizontalSpeed.Set, PlayerInputs.Left.Get, PlayerInputs.Right.Get));
            UpdateHandlers.Add(new JumpOnInputDecreasesVerticalSpeed(SteppingOnTheFloor.Get, VerticalSpeed.Set, PlayerInputs.Jump.Get));
            UpdateHandlers.Add(new GravityIncreasesVerticalSpeed(VerticalSpeed.Get, VerticalSpeed.Set));
            UpdateHandlers.Add(new UsesSpeedToMove(HorizontalSpeed.Get, VerticalSpeed.Get));
            UpdateHandlers.Add(new ForbidJumpIfVerticalSpeedNotZero(SteppingOnTheFloor.Set, VerticalSpeed.Get));
            UpdateHandlers.Add(new GrabsObjectThatPlayerIsFacing(PlayerInputs.Grab.Get, currentPapers.IsNull, currentPapers.Set, DragAndDropCooldown.CooldownEnded, Grid.GetNearObject, DragAndDropCooldown.TriggerCooldown));
            UpdateHandlers.Add(new DropThePapers(currentPapers.IsNotNull, PlayerInputs.Grab.Get, DragAndDropCooldown.CooldownEnded, DragAndDropCooldown.TriggerCooldown, currentPapers.Get, currentPapers.RemoveValue));

            var mainCollider = new GameCollider(this, 50, 100);
            mainCollider.CollisionHandlers.Add(new StopsWhenHitsPapers(mainCollider, SteppingOnTheFloor.Set, VerticalSpeed.Set));
            mainCollider.CollisionHandlers.Add(new MoveBackWhenHittingWall(mainCollider));
            Colliders.Add(mainCollider);
        }
    }
}

