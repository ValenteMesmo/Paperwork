using GameCore;
using GameCore.Collision;
using GameCore.Extensions;
using PaperWork.GameEntities.Collisions;
using PaperWork.GameEntities.Player.Updates;
using PaperWork.PlayerHandlers.Collisions;
using PaperWork.PlayerHandlers.Updates;
using System.Linq;
using System;

namespace PaperWork
{
    public class PlayerEntity : Entity
    {
        PapersEntity holdingPapers;
        GridPositions Grid;
        private Property<float> HorizontalSpeed = new Property<float>();
        private Property<float> VerticalSpeed = new Property<float>();
        private Property<bool> AbleToJump = new Property<bool>();

        public PlayerEntity(InputRepository PlayerInputs, GridPositions Grid)
        {
            this.Grid = Grid;

            Textures.Add(new EntityTexture("char", 50, 100));

            UpdateHandlers.Add(new SpeedUpHorizontallyOnInput(this, PlayerInputs, HorizontalSpeed.Set));
            UpdateHandlers.Add(new JumpOnInputDecreasesVerticalSpeed(this, PlayerInputs, AbleToJump.Get, VerticalSpeed.Set));
            UpdateHandlers.Add(new GravityIncreasesVerticalSpeed(this, VerticalSpeed.Get, VerticalSpeed.Set));
            UpdateHandlers.Add(new UsesSpeedToMove(this, HorizontalSpeed.Get, VerticalSpeed.Get));
            UpdateHandlers.Add(new ForbidJumpIfVerticalSpeedNotZero(this, AbleToJump.Set, VerticalSpeed.Get));

            UpdateHandlers.Add(new GrabsObjectThatPlayerIsFacing(
            this,
            PlayerInputs,
            Grab,
            holding,
            Grid));

            UpdateHandlers.Add(new DropThePapers(
                this
                , IsHoldingPapers
                , Drop
                , PlayerInputs));

            var mainCollider = new GameCollider(this, 50, 100);
            mainCollider.CollisionHandlers.Add(new StopsWhenHitsPapers(mainCollider, AbleToJump.Set, VerticalSpeed.Set));
            mainCollider.CollisionHandlers.Add(new MoveOnCollision(mainCollider));
            Colliders.Add(mainCollider);
        }

        private void Drop()
        {
            holdingPapers = null;
        }

        private bool holding()
        {
            return holdingPapers != null;
        }

        private void Grab(PapersEntity papers)
        {
            holdingPapers = papers;
        }

        private bool IsHoldingPapers()
        {
            return holdingPapers != null;
        }

        //public void Hold(GameCollider PaperCollider)
        //{
        //    Grid.RemoveFromTheGrid(PaperCollider.ParentEntity);
        //    PaperCollider.ParentEntity
        //        .UpdateHandlers.OfType<FollowOtherEntity>()
        //        .First().Target = this;

        //    PaperCollider.Disabled = true;
        //    holdingPapers = (PapersEntity)PaperCollider.ParentEntity;
        //}

        //public void Drop()
        //{
        //    var paperNewPosition = new Coordinate2D(
        //        holdingPapers.Position.X + 50,
        //        holdingPapers.Position.Y);

        //    if (Grid.CanSet(paperNewPosition))
        //    {
        //        holdingPapers.Position = Grid.Set(holdingPapers, paperNewPosition);

        //        holdingPapers.UpdateHandlers
        //            .OfType<FollowOtherEntity>()
        //            .First()
        //            .Target = null;

        //        holdingPapers.Colliders.Enable();
        //        holdingPapers = null;
        //    }
        //}
    }
}
