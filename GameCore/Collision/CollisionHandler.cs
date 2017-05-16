using GameCore.Collision;

namespace GameCore
{
    public abstract class CollisionHandler
    {
        public virtual void CollisionFromBelow(GameCollider collider, GameCollider other) { }
        public virtual void CollisionFromAbove(GameCollider collider, GameCollider other) { }
        public virtual void CollisionFromTheLeft(GameCollider collider, GameCollider other) { }
        public virtual void CollisionFromTheRight(GameCollider collider, GameCollider other) { }
    }
}