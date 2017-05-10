using System;
using GameCore;
using GameCore.Collision;
using GameCore.Extensions;

namespace PaperWork.GameEntities.Collisions
{
    public class GrabsPapers : CollisionHandler
    {
        InputRepository Inpusts;
        Action<GameCollider> GrabPapers;

        public GrabsPapers(
            GameCollider ParentCollider,
            InputRepository Inpusts,
            Action<GameCollider> GrabPapers) : base(ParentCollider)
        {
            this.Inpusts = Inpusts;
            this.GrabPapers = GrabPapers;
        }

        public override void CollisionFromTheLeft(GameCollider other)
        {
            HidePapers(other);
        }

        private void HidePapers(GameCollider other)
        {
            if (other.ParentEntity is Papers
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
