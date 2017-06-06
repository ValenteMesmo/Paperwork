using GameCore;

namespace PaperWork
{
    public class StopsWhenBotCollidingWith<T> : ICollisionHandler
    {
        private readonly ICollider Parent;

        public StopsWhenBotCollidingWith(ICollider Parent)
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

        public void TopCollision(ICollider other) { }

        public void LeftCollision(ICollider other) { }

        public void RightCollision(ICollider other) { }
    }

    public class StopsWhenTopCollidingWith<T> : ICollisionHandler
    {
        private readonly ICollider Parent;

        public StopsWhenTopCollidingWith(ICollider Parent)
        {
            this.Parent = Parent;
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

        public void BotCollision(ICollider other) { }

        public void LeftCollision(ICollider other) { }

        public void RightCollision(ICollider other) { }
    }

    public class StopsWhenLeftCollidingWith<T> : ICollisionHandler
    {
        private readonly ICollider Parent;

        public StopsWhenLeftCollidingWith(ICollider Parent)
        {
            this.Parent = Parent;
        }

        public void BotCollision(ICollider other) { }

        public void TopCollision(ICollider other) { }

        public void RightCollision(ICollider other) { }

        public void LeftCollision(ICollider other)
        {
            if (other is T)
            {
                Parent.X = other.Right()
                    + World.SPACE_BETWEEN_THINGS;
                Parent.HorizontalSpeed = 0;
            }
        }
    }

    public class StopsWhenRightCollidingWith<T> : ICollisionHandler
    {
        private readonly ICollider Parent;

        public StopsWhenRightCollidingWith(ICollider Parent)
        {
            this.Parent = Parent;
        }

        public void BotCollision(ICollider other) { }

        public void TopCollision(ICollider other) { }

        public void LeftCollision(ICollider other) { }

        public void RightCollision(ICollider other)
        {
            if (other is T)
            {
                Parent.X = other.Left()
                       - Parent.Width
                       - World.SPACE_BETWEEN_THINGS;
                Parent.HorizontalSpeed = 0;
            }
        }
    }
}