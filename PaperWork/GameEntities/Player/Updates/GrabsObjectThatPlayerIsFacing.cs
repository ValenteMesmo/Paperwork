using GameCore;
using GameCore.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperWork.GameEntities.Player.Updates
{
    class GrabsObjectThatPlayerIsFacing : UpdateHandler
    {
        private GridPositions Grid;
        InputRepository Inpusts;
        Action<PapersEntity> GrabPapers;
        private Func<bool> HoldingPapers;

        public GrabsObjectThatPlayerIsFacing(
            Entity ParentCollider,
            InputRepository Inpusts,
            Action<PapersEntity> GrabPapers,
            Func<bool> HoldingPapers
            , GridPositions Grid) : base(ParentCollider)
        {
            this.Grid = Grid;
            this.Inpusts = Inpusts;
            this.HoldingPapers = HoldingPapers;
            this.GrabPapers = GrabPapers;
        }

        public override void Update()
        {
            if (HoldingPapers() == false
                && Inpusts.Grab.GetStatus() == ButtomStatus.Click)
            {
                var paper = Grid.GetNearObject(ParentEntity.Position);
                if (paper == null && !(paper is PapersEntity))
                    return;

                GrabPapers((PapersEntity)paper);
            }
        }
    }
}
