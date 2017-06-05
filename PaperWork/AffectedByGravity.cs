using GameCore;

namespace PaperWork
{
    public class AffectedByGravity : IUpdateHandler
    {
        private readonly ICollider Parent;
        private const int GRAVITY = 1;

        public AffectedByGravity(ICollider Parent)
        {
            this.Parent = Parent;
        }

        public void Update()
        {
            Parent.VerticalSpeed += GRAVITY;
        }
    }
}