using GameCore.Collision;
using System;

namespace PaperWork.GameEntities.Papers.CollisionHandlers
{
    //TODO:
    public class ZeroHorizontalSpeedWhenHitsLeft<T> : IHandleCollision
    {
        private readonly Action ZeroHorizontalSpeed;

        public ZeroHorizontalSpeedWhenHitsLeft(Action ZeroHorizontalSpeed)
        {
            this.ZeroHorizontalSpeed = ZeroHorizontalSpeed;
        }

        public void TopCollision(Collider Self, Collider Other) { }
        public void BotCollision(Collider Self, Collider Other) { }
        public void RightCollision(Collider Self, Collider Other) { }

        public void LeftCollision(Collider Self, Collider Other)
        {
            if (Other.ParentEntity is T)
            {
                Self.SetOnRight(Other);
                ZeroHorizontalSpeed();
            }
        }
    }

    public class ZeroHorizontalSpeedWhenHitsRight<T> : IHandleCollision
    {
        private readonly Action ZeroHorizontalSpeed;

        public ZeroHorizontalSpeedWhenHitsRight(Action ZeroHorizontalSpeed)
        {
            this.ZeroHorizontalSpeed = ZeroHorizontalSpeed;
        }

        public void TopCollision(Collider Self, Collider Other) { }
        public void BotCollision(Collider Self, Collider Other) { }
        public void LeftCollision(Collider Self, Collider Other) { }

        public void RightCollision(Collider Self, Collider Other)
        {
            if (Other.ParentEntity is T)
            {
                Self.SetOnLeft(Other);
                ZeroHorizontalSpeed();
            }
        }
    }

    public class ZeroVerticalSpeedWhenHitsTop<T> : IHandleCollision
    {
        private readonly Action ZeroVerticalSpeed;

        public ZeroVerticalSpeedWhenHitsTop(Action ZeroVerticalSpeed)
        {
            this.ZeroVerticalSpeed = ZeroVerticalSpeed;
        }

        public void TopCollision(Collider Self, Collider Other)
        {
            if (Other.ParentEntity is T)
            {
                Self.SetOnBot(Other);
                ZeroVerticalSpeed();
            }
        }

        public void BotCollision(Collider Self, Collider Other) { }
        public void LeftCollision(Collider Self, Collider Other) { }
        public void RightCollision(Collider Self, Collider Other) { }
    }

    public class ZeroVerticalSpeedWhenHitsBot<T> : IHandleCollision
    {
        private readonly Action ZeroVerticalSpeed;

        public ZeroVerticalSpeedWhenHitsBot(Action ZeroVerticalSpeed)
        {
            this.ZeroVerticalSpeed = ZeroVerticalSpeed;
        }

        public void BotCollision(Collider Self, Collider Other)
        {
            if (Other.ParentEntity is T)
            {
                Self.SetOnTop(Other);
                ZeroVerticalSpeed();
            }
        }

        public void LeftCollision(Collider Self, Collider Other) { }
        public void RightCollision(Collider Self, Collider Other) { }
        public void TopCollision(Collider Self, Collider Other) { }
    }
}
