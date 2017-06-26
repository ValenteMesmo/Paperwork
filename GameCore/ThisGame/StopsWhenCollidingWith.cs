using GameCore;
using System;

namespace PaperWork
{
    public class StopsWhenBotCollidingWith<T> : ICollisionHandler
    {
        private readonly Collider Parent;
        private readonly Action<string> PlayAudio;

        public StopsWhenBotCollidingWith(Collider Parent, Action<string> PlayAudio = null)
        {
            this.Parent = Parent;
            if (PlayAudio == null)
                PlayAudio = f => { };
            this.PlayAudio = PlayAudio;
        }

        public void BotCollision(Collider other)
        {
            if (other is T)
            {
                Parent.Y = other.Top()
                    - Parent.Height
                    - World.SPACE_BETWEEN_THINGS;
                if (Parent.VerticalSpeed > 50)
                    PlayAudio("landing");
                Parent.VerticalSpeed = 0;
            }
        }

        public void TopCollision(Collider other) { }

        public void LeftCollision(Collider other) { }

        public void RightCollision(Collider other) { }
    }

    public class StopsWhenTopCollidingWith<T> : ICollisionHandler
    {
        private readonly Collider Parent;

        public StopsWhenTopCollidingWith(Collider Parent)
        {
            this.Parent = Parent;
        }

        public void TopCollision(Collider other)
        {
            if (other is T)
            {
                Parent.Y = other.Bottom()
                    + World.SPACE_BETWEEN_THINGS;
                Parent.VerticalSpeed = 0;
            }
        }

        public void BotCollision(Collider other) { }

        public void LeftCollision(Collider other) { }

        public void RightCollision(Collider other) { }
    }

    public class StopsWhenLeftCollidingWith<T> : ICollisionHandler
    {
        private readonly Collider Parent;

        public StopsWhenLeftCollidingWith(Collider Parent)
        {
            this.Parent = Parent;
        }

        public void BotCollision(Collider other) { }

        public void TopCollision(Collider other) { }

        public void RightCollision(Collider other) { }

        public void LeftCollision(Collider other)
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
        private readonly Collider Parent;

        public StopsWhenRightCollidingWith(Collider Parent)
        {
            this.Parent = Parent;
        }

        public void BotCollision(Collider other) { }

        public void TopCollision(Collider other) { }

        public void LeftCollision(Collider other) { }

        public void RightCollision(Collider other)
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