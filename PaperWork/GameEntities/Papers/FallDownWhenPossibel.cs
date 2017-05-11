using GameCore;
using System;

namespace PaperWork.GameEntities.Papers
{
    public class FallDownWhenPossibel : IHandleEntityUpdates
    {
        private readonly Func<bool> FollowingPlayer;
        private readonly Func<int> GetGridCellSize;
        private readonly Func<Coordinate2D, bool> GridCellEmpty;

        public FallDownWhenPossibel(
            Func<bool> FollowingPlayer,
            Func<int> GetGridCellSize,
            Func<Coordinate2D, bool> GridCellEmpty)
        {
            this.FollowingPlayer = FollowingPlayer;
            this.GetGridCellSize = GetGridCellSize;
            this.GridCellEmpty = GridCellEmpty;
        }

        public void Update(Entity ParentEntity)
        {
            if (FollowingPlayer())
                return;

            var newPosition = new Coordinate2D(
                    ParentEntity.Position.X,
                    ParentEntity.Position.Y + GetGridCellSize()
            );

            if (GridCellEmpty(newPosition))
            {
                //TODO: move this position set to grid method
                ParentEntity.Position = newPosition;
            }
        }
    }
}
