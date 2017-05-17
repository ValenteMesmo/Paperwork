﻿using GameCore.Collision;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public abstract class Entity
    {
        public string Id { get; }
        public Coordinate2D Position { get; set; }
        //todo
        public virtual IEnumerable<EntityTexture> GetTextures() { return Textures; }
        public virtual IList<BaseCollider> GetColliders() { return Colliders; }
        private readonly IList<IHandleEntityUpdates> UpdateHandlers;
        public readonly Action SelfDestruct;

        protected List<BaseCollider> Colliders = new List<BaseCollider>();
        protected List<EntityTexture> Textures= new List<EntityTexture>();
        

        public Entity(Action<Entity> Destroy)
        {
            Id = $"{GetType().Name} {Guid.NewGuid().ToString()}";
            UpdateHandlers = new List<IHandleEntityUpdates>();
            SelfDestruct = ()=> Destroy(this);
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
            foreach (var item in GetColliders())
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
            foreach (var item in GetColliders())
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
