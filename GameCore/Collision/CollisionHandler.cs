using GameCore.Collision;

namespace GameCore
{
    public abstract class CollisionHandler
    {
        protected GameCollider ParentCollider { get; }
        protected Entity ParentEntity { get { return ParentCollider.ParentEntity; } }

        public CollisionHandler(GameCollider ParentCollider)
        {
            this.ParentCollider = ParentCollider;
        }

        public virtual void CollisionFromBelow(GameCollider other) { }
        public virtual void CollisionFromAbove(GameCollider other) { }
        public virtual void CollisionFromTheLeft(GameCollider other) { }
        public virtual void CollisionFromTheRight(GameCollider other) { }
    }
}