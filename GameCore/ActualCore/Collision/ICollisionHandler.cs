namespace GameCore
{
    public interface ICollisionHandler
    {
        void RightCollision(Collider other);
        void LeftCollision(Collider other);
        void TopCollision(Collider other);
        void BotCollision(Collider other);
    }
}
