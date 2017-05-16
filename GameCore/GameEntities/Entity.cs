using GameCore.Collision;
using System;
using System.Collections.Generic;

namespace GameCore
{
    public class Entity
    {
        public string Id { get; protected set; }
        public Coordinate2D Position { get; set; }
        public Coordinate2D RenderPosition { get; set; }
        public IList<EntityTexture> Textures { get; }
        public IList<GameCollider> Colliders { get; }
        private readonly IList<IHandleEntityUpdates> UpdateHandlers;

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
            Textures = new List<EntityTexture>();
            Colliders = new List<GameCollider>();
            UpdateHandlers = new List<IHandleEntityUpdates>();
        }

        public void AddHandlers(params IHandleEntityUpdates[] handlers)
        {
            foreach (var item in handlers)
            {
                UpdateHandlers.Add(item);
            }
        }

        public void Update()
        {
            foreach (var item in UpdateHandlers)
            {
                item.Update(this);
            }
        }

        public T As<T>() where T : Entity
        {
            return (T)this;
        }
    }
}
