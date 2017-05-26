using GameCore.Collision;

namespace GameCore
{
    public interface IHandleCollision
    {
        void CollisionFromBelow(BaseCollider collider, BaseCollider other);
        void CollisionFromAbove(BaseCollider collider, BaseCollider other);
        void CollisionFromTheLeft(BaseCollider collider, BaseCollider other);
        void CollisionFromTheRight(BaseCollider collider, BaseCollider other);
        void CollisionFromWithin(BaseCollider collider, BaseCollider other);
    }
}