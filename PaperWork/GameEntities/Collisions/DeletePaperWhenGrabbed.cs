using GameCore;
using GameCore.Collision;
using GameCore.Extensions;

namespace PaperWork.GameEntities.Collisions
{
    public class DeletePaperWhenGrabbed : CollisionHandler
    {
        InputRepository Inpusts;

        public DeletePaperWhenGrabbed(GameCollider ParentCollider, InputRepository Inpusts) : base(ParentCollider)
        {
            this.Inpusts = Inpusts;
        }

        public override void CollisionFromTheLeft(GameCollider other)
        {
            HidePapers(other);
        }

        private void HidePapers(GameCollider other)
        {
            if (other.ParentEntity is Papers && Inpusts.GrabbPressed)
            {
                other.ParentEntity .Textures.Disable();
                other.ParentEntity.Colliders.Disable();
            }
        }

        public override void CollisionFromTheRight(GameCollider other)
        {
            HidePapers(other);
        }
    }
}
