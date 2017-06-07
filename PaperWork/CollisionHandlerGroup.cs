using GameCore;

namespace PaperWork
{
    public class CollisionHandlerGroup : ICollisionHandler
    {
        private readonly ICollisionHandler[] Handlers;

        public CollisionHandlerGroup(params ICollisionHandler[] Handlers)
        {
            this.Handlers = Handlers;
        }

        public void BotCollision(Collider other)
        {
            foreach (var item in Handlers)
            {
                item.BotCollision(other);
            }
        }

        public void LeftCollision(Collider other)
        {
            foreach (var item in Handlers)
            {
                item.LeftCollision(other);
            }
        }

        public void RightCollision(Collider other)
        {
            foreach (var item in Handlers)
            {
                item.RightCollision(other);
            }
        }

        public void TopCollision(Collider other)
        {
            foreach (var item in Handlers)
            {
                item.TopCollision(other);
            }
        }
    }
}
