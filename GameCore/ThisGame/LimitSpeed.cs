using GameCore;

namespace PaperWork
{
    public class LimitSpeed : SomethingThatHandleUpdates
    {
        private readonly Collider Parent;
        private readonly int MaxHorizontal;
        private readonly int MaxVertical;

        public LimitSpeed(Collider Parent, int MaxHorizontal, int MaxVertical)
        {
            this.Parent = Parent;
            this.MaxHorizontal = MaxHorizontal;
            this.MaxVertical = MaxVertical;
        }

        public void Update()
        {
            Parent.VerticalSpeed = Parent.VerticalSpeed.Range(-MaxVertical, MaxVertical);
            Parent.HorizontalSpeed = Parent.HorizontalSpeed.Range(-MaxHorizontal, MaxHorizontal);
        }
    }
}