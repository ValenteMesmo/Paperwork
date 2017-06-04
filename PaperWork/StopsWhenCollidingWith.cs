using System;
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

    public class CollisionChecker<T> :
        ICollisionHandler
        , IBeforeCollisionHandler where T : ICollider
    {
        public bool Colliding { get; private set; }

        public void BeforeCollision()
        {
            Colliding = false;
        }

        public void BotCollision(ICollider other)
        {
            if (other is T)
            {
                Colliding = true;
            }
        }

        public void LeftCollision(ICollider other)
        {
            if (other is T)
            {
                Colliding = true;
            }
        }

        public void RightCollision(ICollider other)
        {
            if (other is T)
            {
                Colliding = true;
            }
        }

        public void TopCollision(ICollider other)
        {
            if (other is T)
            {
                Colliding = true;
            }
        }
    }

    public class StopsWhenCollidingWith<T> : ICollisionHandler where T : ICollider
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

        public void TopCollision(ICollider other)
        {
            if (other is T)
            {
                Parent.Y = other.Bottom()
                    + World.SPACE_BETWEEN_THINGS;
                Parent.VerticalSpeed = 0;
            }
        }
    }
}