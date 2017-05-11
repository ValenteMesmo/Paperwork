using GameCore;
using System;

namespace PaperWork.GameEntities.Papers
{
    public class FallDownWhenPossibel : IHandleEntityUpdates
    {
        GridPositions Grid;
        Func<bool> FollowingPlayer;

        public FallDownWhenPossibel(            
            GridPositions Grid,
            Func<bool> FollowingPlayer)
        {
            this.FollowingPlayer = FollowingPlayer;
            this.Grid = Grid;
        }

        public void Update(Entity ParentEntity)
        {
            if (FollowingPlayer())
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
