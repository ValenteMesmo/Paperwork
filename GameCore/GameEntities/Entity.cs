using GameCore.Collision;
using System;
using System.Collections.Generic;

namespace GameCore
{
    public abstract class Entity
    {
        public string Id { get; }
        public Coordinate2D Position { get; set; }
        public Action Destroy;
        protected List<BaseCollider> Colliders = new List<BaseCollider>();
        protected List<EntityTexture> Textures = new List<EntityTexture>();
        private readonly static Dictionary<Type, int> instancesCount = new Dictionary<Type, int>();

        public Entity()
        {
            Destroy = () => { };

            var type = GetType();
            if (instancesCount.ContainsKey(type) == false)
                instancesCount.Add(type, 0);
            instancesCount[GetType()]++;

            Id = $"{GetType().Name} {instancesCount[GetType()]}";
        }

        public virtual IEnumerable<EntityTexture> GetTextures()
        {
            return Textures;
        }

        public virtual IList<BaseCollider> GetColliders()
        {
            return Colliders;
        }

        internal void Update()
        {
            foreach (var item in GetColliders())
            {
                item.Update();
            }

            OnUpdate();
        }

        protected virtual void OnUpdate()
        {

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
