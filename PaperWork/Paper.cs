using GameCore;

namespace PaperWork
{
    public class Paper :
        ICollider
        , ICollisionHandler
        , IUpdateHandler
        , IPlayerMovementBlocker
    {
        private ICollisionHandler CollisionHandler;
        private IUpdateHandler UpdateHandler;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }

        public Paper()
        {
            Width = 100;
            Height = 100;

            CollisionHandler = new StopsWhenCollidingWith<IPlayerMovementBlocker>(this);
            UpdateHandler = new UpdateGroup(
               new AffectedByGravity(this)
               , new LimitSpeed(this, 3, 5)
           );
        }

        public void Update()
        {
            UpdateHandler.Update();
        }

        public void BotCollision(ICollider collider)
        {
            CollisionHandler.BotCollision(collider);
        }

        public void TopCollision(ICollider collider)
        {
            CollisionHandler.TopCollision(collider);
        }

        public void LeftCollision(ICollider collider)
        {
            CollisionHandler.LeftCollision(collider);
        }

        public void RightCollision(ICollider collider)
        {
            CollisionHandler.RightCollision(collider);
        }
    }
}
