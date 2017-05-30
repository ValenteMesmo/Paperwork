namespace GameCore.Collision
{
    public interface IHandleCollision
    {
        void BotCollision(Collider Self, Collider Other);
        void TopCollision(Collider Self, Collider Other);
        void LeftCollision(Collider Self, Collider Other);
        void RightCollision(Collider Self, Collider Other);
    }
}