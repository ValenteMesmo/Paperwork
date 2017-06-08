using GameCore;

namespace PaperWork
{
    public class FollowOtherCollider : IUpdateHandler
    {
        private readonly Collider Source;
        private readonly Collider Target;
        private readonly int OffsetX;
        private readonly int OffsetY;

        FollowOtherCollider(Collider Source, Collider Target, int OffsetX, int OffsetY)
        {
            this.Source = Source;
            this.Target = Target;
            this.OffsetX = OffsetX;
            this.OffsetY = OffsetY;
        }

        public void Update()
        {
            Source.X = Target.X + OffsetX;
            Source.Y = Target.Y + OffsetY;
        }
    }
}