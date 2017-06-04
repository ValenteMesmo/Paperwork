using GameCore;

namespace PaperWork
{
    public class LimitSpeed : IUpdateHandler
    {
        private readonly ICollider Parent;
        private readonly float MaxHorizontal;
        private readonly float MaxVertical;

        public LimitSpeed(ICollider Parent, float MaxHorizontal, float MaxVertical)
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