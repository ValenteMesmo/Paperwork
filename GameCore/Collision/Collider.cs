using System.Collections.Generic;

namespace GameCore.Collision
{
    public class Collider : BaseCollider
    {
        private readonly IList<IHandleCollision> CollisionHandlers;

        public Collider(Entity ParentEntity, float Width, float Height) : base(ParentEntity, Width, Height)
        {
            CollisionHandlers = new List<IHandleCollision>();
        }

        internal override void CollisionFromBelow(BaseCollider other)
        {
            foreach (var item in CollisionHandlers)
            {
                item.CollisionFromBelow(this, other);
            }
        }

        internal override void CollisionFromAbove(BaseCollider other)
        {
            foreach (var item in CollisionHandlers)
            {
                item.CollisionFromAbove(this, other);
            }
        }

        internal override void CollisionFromTheLeft(BaseCollider other)
        {
            foreach (var item in CollisionHandlers)
            {
                item.CollisionFromTheLeft(this, other);
            }
        }

        internal override void CollisionFromTheRight(BaseCollider other)
        {
            foreach (var item in CollisionHandlers)
            {
                item.CollisionFromTheRight(this, other);
            }
        }

        public void AddHandlers(params IHandleCollision[] handlers)
        {
            foreach (var item in handlers)
            {
                CollisionHandlers.Add(item);
            }
        }
    }
}