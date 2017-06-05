﻿using GameCore;

namespace PaperWork
{
    public class LimitSpeed : IUpdateHandler
    {
        private readonly ICollider Parent;
        private readonly int MaxHorizontal;
        private readonly int MaxVertical;

        public LimitSpeed(ICollider Parent, int MaxHorizontal, int MaxVertical)
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