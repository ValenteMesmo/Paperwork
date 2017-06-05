using GameCore;

namespace PaperWork
{
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
}