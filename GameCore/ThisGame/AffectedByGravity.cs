using GameCore;

namespace PaperWork
{
    public class AffectedByGravity : SomethingThatHandleUpdates
    {
        private readonly Collider Parent;
        private const int GRAVITY = 5;

        public AffectedByGravity(Collider Parent)
        {
            this.Parent = Parent;
        }

        public void Update()
        {
            Parent.VerticalSpeed += GRAVITY;
        }
    }
}