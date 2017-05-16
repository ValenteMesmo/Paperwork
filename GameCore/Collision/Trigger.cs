﻿using GameCore.Extensions;
using System.Collections.Generic;

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

        internal override void Update()
        {
            PreviousUpdateCollisions.Clear();
            PreviousUpdateCollisions.AddRange(CurrentUpdateCollisions);
            CurrentUpdateCollisions.Clear();
        }

        internal override void AfterUpdate()
        {
            foreach (var collider in CurrentUpdateCollisions)
            {
                if (PreviousUpdateCollisions.Contains(collider))
                    continue;

                foreach (var handler in TriggerHandlers)
                {
                    handler.TriggerEnter(this, collider);
                }

                PreviousUpdateCollisions.Remove(collider);
            }

            foreach (var collider in PreviousUpdateCollisions)
            {
                foreach (var handler in TriggerHandlers)
                {
                    handler.TriggerExit(this, collider);
                }
            }
        }

        internal override void CollisionFromBelow(BaseCollider other)
        {
            if (CurrentUpdateCollisions.Contains(other) == false)
            {
                CurrentUpdateCollisions.Add(other);
            }
        }

        internal override void CollisionFromAbove(BaseCollider other)
        {
            if (CurrentUpdateCollisions.Contains(other) == false)
            {
                CurrentUpdateCollisions.Add(other);
            }
        }

        internal override void CollisionFromTheLeft(BaseCollider other)
        {
            if (CurrentUpdateCollisions.Contains(other) == false)
            {
                CurrentUpdateCollisions.Add(other);
            }
        }

        internal override void CollisionFromTheRight(BaseCollider other)
        {
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
    }
}