using GameCore.Collision;
using System;
using System.Collections.Generic;

namespace GameCore
{
    public abstract class Entity
    {
        public string Id { get; }
        public int Width { get; }
        public int Height { get; }
        public Coordinate2D Position { get; set; }
        public Action Destroy;
        protected List<Collider> Colliders = new List<Collider>();
        protected List<EntityTexture> Textures = new List<EntityTexture>();
        private readonly static Dictionary<Type, int> instancesCount = new Dictionary<Type, int>();

        public Entity(int Width = 0, int Height = 0)
        {
            this.Width = Width;
            this.Height = Height;

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

        public virtual IList<Collider> GetColliders()
        {
            return Colliders;
        }

        internal void Update()
        {
            OnUpdate();
            foreach (var item in GetColliders())
            {
                item.Update();
            }
        }

        protected virtual void OnUpdate()
        {

        }

        public T As<T>() where T : Entity
        {
            return (T)this;
        }
        
        public override string ToString()
        {
            return Id;
        }
    }
}
