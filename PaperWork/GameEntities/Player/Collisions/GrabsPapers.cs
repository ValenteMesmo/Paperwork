using System;
using GameCore;
using GameCore.Collision;
using GameCore.Extensions;

namespace PaperWork.GameEntities.Collisions
{
    public class DELEEEEEEEEEEEEEEEEEEEEEEEEEEEETE : CollisionHandler
    {
        InputRepository Inpusts;
        Action<GameCollider> GrabPapers;
        private Func<bool> HoldingPapers;

        public DELEEEEEEEEEEEEEEEEEEEEEEEEEEEETE(
            GameCollider ParentCollider,
            InputRepository Inpusts,
            Action<GameCollider> GrabPapers,
            Func<bool> HoldingPapers) : base(ParentCollider)
        {
            this.Inpusts = Inpusts;
            this.HoldingPapers = HoldingPapers;
            this.GrabPapers = GrabPapers;
        }

        public override void CollisionFromTheLeft(GameCollider other)
        {
            HidePapers(other);
        }

        private void HidePapers(GameCollider other)
        {
            if (HoldingPapers() == false
                && other.ParentEntity is PapersEntity
                && Inpusts.Grab.GetStatus() == ButtomStatus.Click)
            {
                GrabPapers(other);
            }
        }

        public override void CollisionFromTheRight(GameCollider other)
        {
            HidePapers(other);
        }
    }
}
