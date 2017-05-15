using GameCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaperWork
{
    class MakePapersFall : IHandleEntityUpdates
    {
        private readonly Func<IEnumerable<Entity>> GetPapers;
        private readonly Func<Coordinate2D, bool> PositionBelowAvailable;
        private readonly Action<Entity> MoveToCellBelow;

        public MakePapersFall(
            Func<IEnumerable<Entity>> GetPapers
            , Func<Coordinate2D, bool> PositionBelowAvailable
            , Action<Entity> MoveToCellBelow)
        {
            this.GetPapers = GetPapers;
            this.PositionBelowAvailable = PositionBelowAvailable;
            this.MoveToCellBelow = MoveToCellBelow;
        }

        public void Update(Entity entity)
        {
            foreach (var item in GetPapers().ToList())
            {
                if (PositionBelowAvailable(item.Position))
                {
                    MoveToCellBelow(item);
                }
            }
        }
    }
}
