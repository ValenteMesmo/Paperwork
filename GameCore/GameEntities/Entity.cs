using GameCore.Collision;
using System;
using System.Collections.Generic;

namespace GameCore
{
    public class Entity
    {
        public string Id { get; }
        public Coordinate2D Position { get; set; }
        public Coordinate2D RenderPosition { get; set; }
        public IList<EntityTexture> Textures { get; }
        public IList<BaseCollider> Colliders { get; }
        private readonly IList<IHandleEntityUpdates> UpdateHandlers;
        public readonly Action SelfDestruct;

        public Entity(Action<Entity> Destroy)
        {
            Id = $"{GetType().Name} {Guid.NewGuid().ToString()}";
            Textures = new List<EntityTexture>();
            Colliders = new List<BaseCollider>();
            UpdateHandlers = new List<IHandleEntityUpdates>();
            this.SelfDestruct = ()=> Destroy(this);
        }

        public void AddUpdateHandlers(params IHandleEntityUpdates[] handlers)
        {
            foreach (var item in handlers)
            {
                UpdateHandlers.Add(item);
            }
        }

        internal void Update()
        {
            foreach (var item in Colliders)
            {
                item.Update();
            }

            foreach (var item in UpdateHandlers)
            {
                item.Update(this);
            }
        }

        public T As<T>() where T : Entity
        {
            return (T)this;
        }

        internal void AfterCollisions()
        {
            foreach (var item in Colliders)
            {
                item.AfterCollisions();
            }
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
