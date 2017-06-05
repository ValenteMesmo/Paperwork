using System;
using GameCore;

namespace PaperWork
{
    public class StopsWhenCollidingWith<T> : ICollisionHandler
    {
        private readonly ICollider Parent;

        public StopsWhenCollidingWith(ICollider Parent)
        {
            this.Parent = Parent;
        }

        public void BotCollision(ICollider other)
        {
            if (other is T)
            {
                Parent.Y = other.Top()
                    - Parent.Height
                    - World.SPACE_BETWEEN_THINGS;
                Parent.VerticalSpeed = 0;
            }
        }

        public void TopCollision(ICollider other)
        {
            if (other is T)
            {
                Parent.Y = other.Bottom()
                    + World.SPACE_BETWEEN_THINGS;
                Parent.VerticalSpeed = 0;
            }
        }

        public void LeftCollision(ICollider other)
        {
            if (other is T)
            {
                Parent.X = other.Right()
                    + World.SPACE_BETWEEN_THINGS;
                Parent.HorizontalSpeed = 0;
            }
        }

        public void RightCollision(ICollider other)
        {
            if (other is T)
            {
                Parent.X = other.Left()
                       - other.Width
                       - World.SPACE_BETWEEN_THINGS;
                Parent.HorizontalSpeed = 0;
            }
        }
    }
}