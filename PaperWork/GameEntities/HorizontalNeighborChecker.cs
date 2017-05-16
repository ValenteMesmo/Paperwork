using GameCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaperWork
{
    public class HorizontalNeighborChecker
    {
        public IEnumerable<PapersEntity> GetNeighborsCombo(PapersEntity paper)
        {
            var result = new List<PapersEntity> { paper };

            AddRightNeighbors(paper, result.Add);
            AddLeftNeighbors(paper, result.Add);

            if (result.Count > 2)
                return result;
            else
                return Enumerable.Empty<PapersEntity>();
        }

        private void AddRightNeighbors(PapersEntity paper, Action<PapersEntity> Add)
        {
            if (paper.RightNeighbor.HasValue() && paper.RightNeighbor.Get().VerticalSpeed.Get() == 0)
            {
                Add(paper.RightNeighbor.Get());
                AddRightNeighbors(paper.RightNeighbor.Get(), Add);
            }
        }

        private void AddLeftNeighbors(PapersEntity paper, Action<PapersEntity> Add)
        {
            if (paper.LeftNeighbor.HasValue() && paper.LeftNeighbor.Get().VerticalSpeed.Get() == 0)
            {
                Add(paper.LeftNeighbor.Get());
                AddLeftNeighbors(paper.LeftNeighbor.Get(), Add);
            }
        }
    }
}
