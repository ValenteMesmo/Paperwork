using GameCore;
using GameCore.Collision;
using GameCore.Extensions;
using PaperWork.GameEntities.Collisions;
using PaperWork.PlayerHandlers.Collisions;
using PaperWork.PlayerHandlers.Updates;
using System;
using System.Linq;

namespace PaperWork
{
    public class GridPositions
    {
        Entity[,] matrix;
        int cellSize;
        int rows;
        int columns;
        public GridPositions(int rows, int columns, int cellSize)
        {
            this.rows = rows;
            this.columns = columns;
            this.cellSize = cellSize;
            matrix = new Entity[rows, columns];
        }

        public bool CanSet(Coordinate2D position)
        {
            var gridPosition = new Coordinate2D(
                RoundUp(position.X, cellSize)/cellSize,
                RoundUp(position.Y, cellSize) / cellSize);

            if (gridPosition.X >= rows || gridPosition.Y >= columns)
                return false;

            return matrix[(int)gridPosition.X,(int) gridPosition.Y] == null;
        }

        public void ClearGridCell(Coordinate2D position)
        {
            var gridPosition = new Coordinate2D(
               RoundUp(position.X, cellSize) / cellSize,
               RoundUp(position.Y, cellSize) / cellSize);

            matrix[(int)gridPosition.X, (int)gridPosition.Y] = null;
        }

        public Coordinate2D Set(Entity entity, Coordinate2D position)
        {
            var newPosition = new Coordinate2D(
                RoundUp(position.X, cellSize),
                RoundUp(position.Y, cellSize));

            var gridPosition = new Coordinate2D(
                newPosition.X / cellSize,
                newPosition.Y/ cellSize);

            matrix[(int)gridPosition.X, (int)gridPosition.Y] = entity;

            return newPosition;
        }

        int RoundUp(float numToRound, int multiple)
        {
            return (int)Math.Round(numToRound / multiple) * multiple;
        }
    }

    public class Player : Entity
    {
        Papers holdingPapers;
        GridPositions Grid;

        public Player(InputRepository PlayerInputs, GridPositions Grid)
        {
            this.Grid = Grid;
            var canJump = true;

            Textures.Add(new EntityTexture("char", 50, 100));

            UpdateHandlers.Add(new SpeedUpHorizontallyOnInput(this, PlayerInputs));
            UpdateHandlers.Add(new JumpOnInput(this, PlayerInputs, () => canJump));
            UpdateHandlers.Add(new GravityFall(this));
            UpdateHandlers.Add(new UsesSpeedToMove(this));
            UpdateHandlers.Add(new ForbidJumpIfVerticalSpeedNotZero(this, () => canJump = false));
            
            UpdateHandlers.Add(new DropThePapers(
                this
                , IsHoldingPapers
                , Drop
                , PlayerInputs));

            var mainCollider = new GameCollider(this, 50, 100);
            mainCollider.CollisionHandlers.Add(new StopsWhenHitsPapers(mainCollider, () => canJump = true));
            mainCollider.CollisionHandlers.Add(new MoveOnCollision(mainCollider));
            Colliders.Add(mainCollider);

            var colliderThatGrabsThePaper = new GameCollider(this, 25, 25)
            {
                LocalPosition = new Coordinate2D(25, 50)
            };

            colliderThatGrabsThePaper.CollisionHandlers.Add(
                new GrabsPapers(
                    colliderThatGrabsThePaper
                    , PlayerInputs
                    , Hold));

            Colliders.Add(colliderThatGrabsThePaper);
        }

        private bool IsHoldingPapers()
        {
            return holdingPapers != null;
        }

        public void Hold(GameCollider PaperCollider)
        {
            Grid.ClearGridCell(PaperCollider.ParentEntity.Position);

            PaperCollider.ParentEntity.UpdateHandlers.Add(
             new FollowOtherEntity(
                 PaperCollider.ParentEntity,
                 this,
                 new Coordinate2D(0, -PaperCollider.Height)));

            PaperCollider.Disabled = true;
            holdingPapers = (Papers)PaperCollider.ParentEntity;
        }

        public void Drop()
        {
            var paperNewPosition = new Coordinate2D(
                holdingPapers.Position.X + 50,
                holdingPapers.Position.Y);

            if (Grid.CanSet(paperNewPosition))
            {                
                holdingPapers.Position = Grid.Set(holdingPapers, paperNewPosition);

                foreach (var item in holdingPapers.UpdateHandlers.OfType<FollowOtherEntity>().ToList())
                    holdingPapers.UpdateHandlers.Remove(item);
                
                holdingPapers.Colliders.Enable();
                holdingPapers = null;
            }
        }
    }
}
