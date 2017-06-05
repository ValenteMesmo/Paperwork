using GameCore;

namespace PaperWork
{
    public class FollowOtherCollider : IUpdateHandler
    {
        private readonly ICollider Source;
        private readonly ICollider Target;
        private readonly float OffsetX;
        private readonly float OffsetY;

        FollowOtherCollider(ICollider Source, ICollider Target, float OffsetX, float OffsetY)
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