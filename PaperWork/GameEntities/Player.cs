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
        GridPositions Grid;
        private readonly Property<PapersEntity> currentPapers = new Property<PapersEntity>();
        //private readonly Property<float> HorizontalSpeed = new Property<float>();
        //private readonly Property<float> VerticalSpeed = new Property<float>();
        private readonly Property<bool> SteppingOnTheFloor = new Property<bool>();
        private readonly Cooldown DragAndDropCooldown = new Cooldown(500);

        public PlayerEntity(InputRepository PlayerInputs, GridPositions Grid)
        {
            this.Grid = Grid;

            var mainCollider = new GameCollider(this, 50, 100);            

            Func<float> GetHorizontalSpeed = () => mainCollider.Speed.X;
            Func<float> GetVerticalSpeed = () => mainCollider.Speed.Y;
            Action<float> SetHorizontalSpeed = f => mainCollider.Speed = new Coordinate2D(f, mainCollider.Speed.Y);
            Action<float> SetVerticalSpeed = f => mainCollider.Speed = new Coordinate2D(mainCollider.Speed.X, f);

            Textures.Add(new EntityTexture("char", 50, 100));

            UpdateHandlers.Add(new SpeedUpHorizontallyOnInput(SetHorizontalSpeed, PlayerInputs.Left.Get, PlayerInputs.Right.Get));
            UpdateHandlers.Add(new JumpOnInputDecreasesVerticalSpeed(SteppingOnTheFloor.Get, SetVerticalSpeed, PlayerInputs.Jump.Get));
            UpdateHandlers.Add(new GravityIncreasesVerticalSpeed(GetVerticalSpeed, SetVerticalSpeed));
            UpdateHandlers.Add(new UsesSpeedToMove(GetHorizontalSpeed, GetVerticalSpeed));
            UpdateHandlers.Add(new ForbidJumpIfVerticalSpeedNotZero(SteppingOnTheFloor.Set, GetVerticalSpeed));
            UpdateHandlers.Add(new GrabsObjectThatPlayerIsFacing(PlayerInputs.Grab.Get, currentPapers.IsNull, currentPapers.Set, DragAndDropCooldown.CooldownEnded, Grid.Pop, DragAndDropCooldown.TriggerCooldown));
            UpdateHandlers.Add(new DropThePapers(currentPapers.IsNotNull, PlayerInputs.Grab.Get, DragAndDropCooldown.CooldownEnded, DragAndDropCooldown.TriggerCooldown, currentPapers.SetDefaut, () => currentPapers.Get().Drop(), Grid.PositionAvailable));

            mainCollider.CollisionHandlers.Add(new StopsWhenHitsPapers(mainCollider, SteppingOnTheFloor.Set, SetVerticalSpeed, GetVerticalSpeed));
            mainCollider.CollisionHandlers.Add(new MoveBackWhenHittingWall(mainCollider));
            Colliders.Add(mainCollider);
        }
    }
}

