using GameCore;
using System;

namespace PaperWork.GameEntities.Papers
{
    public class FallDownWhenPossibel : UpdateHandler
    {
        GridPositions Grid;
        Func<bool> FollowingPlayer;

        public FallDownWhenPossibel(
            Entity ParentEntity, 
            GridPositions Grid,
            Func<bool> FollowingPlayer) : base(ParentEntity)
        {
            this.FollowingPlayer = FollowingPlayer;
            this.Grid = Grid;
        }

        public override void Update()
        {
            if (FollowingPlayer() == false)
                return;

            var newPosition = new Coordinate2D(
                    ParentEntity.Position.X,
                    ParentEntity.Position.Y + Grid.cellSize
            );

            //TODO: move this position set to grid method
            if (Grid.CanSet(newPosition))
            {
                ParentEntity.Position = newPosition;
            }
        }
    }
}
