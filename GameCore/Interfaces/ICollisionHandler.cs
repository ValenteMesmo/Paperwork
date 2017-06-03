namespace GameCore
{
    public interface ICollisionHandler
    {
        void RightCollision(ICollider other);
        void LeftCollision(ICollider other);
        void TopCollision(ICollider other);
        void BotCollision(ICollider other);
    }
}
