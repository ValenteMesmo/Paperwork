using GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperWork.GameEntities.Papers
{
    public class FallDownWhenPossibel : UpdateHandler
    {
        GridPositions Grid;
        Func<bool> PlayerHolding;

        public FallDownWhenPossibel(
            Entity ParentEntity, 
            GridPositions Grid,
            Func<bool> PlayerHolding) : base(ParentEntity)
        {
            this.PlayerHolding = PlayerHolding;
            this.Grid = Grid;
        }

        public override void Update()
        {
            if (PlayerHolding())
                return;

            var newPosition = new Coordinate2D(
                    ParentEntity.Position.X,
                    ParentEntity.Position.Y + Grid.cellSize
            );

            if (Grid.CanSet(newPosition))
            {
                ParentEntity.Position = newPosition;
            }
        }
    }
}
