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
            if (paper.RightNeighbor.HasValue()
                && paper.RightNeighbor.Get().VerticalSpeed.Get() == 0
                && paper.RightNeighbor.Get().Color == paper.Color)
            {
                Add(paper.RightNeighbor.Get());
                AddRightNeighbors(paper.RightNeighbor.Get(), Add);
            }
        }

        private void AddLeftNeighbors(PapersEntity paper, Action<PapersEntity> Add)
        {
            if (paper.LeftNeighbor.HasValue()
                && paper.LeftNeighbor.Get().VerticalSpeed.Get() == 0
                && paper.LeftNeighbor.Get().Color == paper.Color)
            {
                Add(paper.LeftNeighbor.Get());
                AddLeftNeighbors(paper.LeftNeighbor.Get(), Add);
            }
        }
    }

    public class VerticalNeighborChecker
    {
        public IEnumerable<PapersEntity> GetNeighborsCombo(PapersEntity paper)
        {
            var result = new List<PapersEntity> { paper };

            AddTopNeighbors(paper, result.Add);
            AddBotNeighbors(paper, result.Add);

            if (result.Count > 2)
                return result;
            else
                return Enumerable.Empty<PapersEntity>();
        }

        private void AddTopNeighbors(PapersEntity paper, Action<PapersEntity> Add)
        {
            if (paper.TopNeighbor.HasValue()
                && paper.TopNeighbor.Get().VerticalSpeed.Get() == 0
                && paper.TopNeighbor.Get().Color == paper.Color)
            {
                Add(paper.TopNeighbor.Get());
                AddTopNeighbors(paper.TopNeighbor.Get(), Add);
            }
        }

        private void AddBotNeighbors(PapersEntity paper, Action<PapersEntity> Add)
        {
            if (paper.BotNeighbor.HasValue()
                && paper.BotNeighbor.Get().VerticalSpeed.Get() == 0
                && paper.BotNeighbor.Get().Color == paper.Color)
            {
                Add(paper.BotNeighbor.Get());
                AddBotNeighbors(paper.BotNeighbor.Get(), Add);
            }
        }
    }
}
