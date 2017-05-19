using GameCore.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.Collision
{
    public class Trigger : BaseCollider
    {
        private readonly IList<IHandleTriggers> TriggerHandlers;
        private readonly IList<BaseCollider> PreviousUpdateCollisions;
        private readonly IList<BaseCollider> CurrentUpdateCollisions;

        public Trigger(Entity ParentEntity, float Width, float Height) : base(ParentEntity, Width, Height)
        {
            TriggerHandlers = new List<IHandleTriggers>();
            PreviousUpdateCollisions = new List<BaseCollider>();
            CurrentUpdateCollisions = new List<BaseCollider>();
        }

        public IEnumerable<Entity> GetEntities()
        {
            return PreviousUpdateCollisions.Select(f=>f.ParentEntity).Distinct();
        }

        internal override void Update()
        {
            PreviousUpdateCollisions.Clear();
            PreviousUpdateCollisions.AddRange(CurrentUpdateCollisions);
            CurrentUpdateCollisions.Clear();
        }

        internal override void AfterCollisions()
        {
            foreach (var collider in CurrentUpdateCollisions)
            {
                if (PreviousUpdateCollisions.Contains(collider))
                    continue;

                foreach (var handler in TriggerHandlers)
                {
                    handler.TriggerEnter(this, collider);
                }
            }

            foreach (var collider in PreviousUpdateCollisions)
            {
                if (CurrentUpdateCollisions.Contains(collider))
                    continue;

                foreach (var handler in TriggerHandlers)
                {
                    handler.TriggerExit(this, collider);
                }
            }
        }

        internal override void CollisionFromBelow(BaseCollider other)
        {
            if (other is Collider)
                if (CurrentUpdateCollisions.Contains(other) == false)
                {
                    CurrentUpdateCollisions.Add(other);
                }
        }

        internal override void CollisionFromAbove(BaseCollider other)
        {
            if (other is Collider)
                if (CurrentUpdateCollisions.Contains(other) == false)
                {
                    CurrentUpdateCollisions.Add(other);
                }
        }

        internal override void CollisionFromTheLeft(BaseCollider other)
        {
            if (other is Collider)
                if (CurrentUpdateCollisions.Contains(other) == false)
                {
                    CurrentUpdateCollisions.Add(other);
                }
        }

        internal override void CollisionFromTheRight(BaseCollider other)
        {
            if (other is Collider)
                if (CurrentUpdateCollisions.Contains(other) == false)
                {
                    CurrentUpdateCollisions.Add(other);
                }
        }

        public void AddHandlers(params IHandleTriggers[] handlers)
        {
            foreach (var item in handlers)
            {
                TriggerHandlers.Add(item);
            }
        }

        public override string ToString()
        {
            return $"{ParentEntity}'s Trigger";
        }
    }
}