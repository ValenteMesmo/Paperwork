using GameCore;

namespace PaperWork
{
    public class AffectedByGravity : IUpdateHandler
    {
        private readonly ICollider Parent;
        private const float GRAVITY = 0.2f;

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