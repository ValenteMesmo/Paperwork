using System.Collections.Generic;

namespace GameCore.Collision
{
    public class Trigger : Collider
    {
        private List<Entity> Entities = new List<Entity>();
        private List<Entity> PreviousEntities = new List<Entity>();

        private readonly int Id;

        public Trigger(
            Entity ParentEntity, 
            int OffsetX, 
            int OffsetY, 
            float Width, 
            float Height) : base(ParentEntity, OffsetX, OffsetY, Width, Height)
        {
            Id = ParentEntity.GetColliders().Count;
        }

        public IEnumerable<Entity> GetEntities()
        {
            return PreviousEntities;
        }

        public override string ToString()
        {
            return $"{nameof(Trigger)} {Id} ({ParentEntity.ToString()})";
        }

        internal override void Update()
        {
            PreviousEntities.Clear();
            PreviousEntities.AddRange(Entities);
            Entities.Clear();
        }

        internal override void HandleBotCollision(Collider Source, float Difference)
        {
            if (Entities.Contains(Source.ParentEntity) == false)
                Entities.Add(Source.ParentEntity);
        }

        internal override void HandleTopCollision(Collider Source, float Difference)
        {
            if (Entities.Contains(Source.ParentEntity) == false)
                Entities.Add(Source.ParentEntity);
        }

        internal override void HandleLeftCollision(Collider Source, float Difference)
        {
            if (Entities.Contains(Source.ParentEntity) == false)
                Entities.Add(Source.ParentEntity);
        }

        internal override void HandleRightCollision(Collider Source, float Difference)
        {
            if (Entities.Contains(Source.ParentEntity) == false)
                Entities.Add(Source.ParentEntity);
        }
    }
}