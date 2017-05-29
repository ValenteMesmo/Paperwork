using System.Collections.Generic;

namespace GameCore.Collision
{
    public class Trigger : Collider
    {
        List<Entity> Entities = new List<Entity>();
        List<Entity> PreviousEntities = new List<Entity>();

        public Trigger(
            Entity ParentEntity,              
            float Width, 
            float Height,
            int OffsetX,
            int OffsetY) : base(
                ParentEntity, 
                OffsetX, 
                OffsetY, 
                Width, 
                Height)
        {
        }

        public IEnumerable<Entity> GetEtities()
        {
            return PreviousEntities;
        }

        public void BotCollision(Collider Self, Collider Other)
        {
            if (Entities.Contains(Other.ParentEntity) == false)
                Entities.Add(Other.ParentEntity);
        }

        public void LeftCollision(Collider Self, Collider Other)
        {
            if (Entities.Contains(Other.ParentEntity) == false)
                Entities.Add(Other.ParentEntity);
        }

        public void RightCollision(Collider Self, Collider Other)
        {
            if (Entities.Contains(Other.ParentEntity) == false)
                Entities.Add(Other.ParentEntity);
        }

        public void TopCollision(Collider Self, Collider Other)
        {
            if (Entities.Contains(Other.ParentEntity) == false)
                Entities.Add(Other.ParentEntity);
        }

        internal override void Update()
        {
            PreviousEntities.Clear();
            PreviousEntities.AddRange(Entities);
            Entities.Clear();
        }
    }


    public class Collider
    {
        internal readonly int OffsetX;
        internal readonly int OffsetY;
        private readonly IHandleCollision[] Handlers;

        public Entity ParentEntity { get; }

        internal float Width { get; set; }
        internal float Height { get; set; }
        public bool Disabled { get; set; }

        internal Coordinate2D ColliderPosition
        {
            get
            {
                return new Coordinate2D(
                    ParentEntity.Position.X + OffsetX
                    , ParentEntity.Position.Y + OffsetY
                );
            }
        }

        public Collider(
            Entity ParentEntity
            , int OffsetX
            , int OffsetY
            , float Width
            , float Height
            , params IHandleCollision[] Handlers
            )
        {
            this.Width = Width;
            this.Height = Height;
            this.OffsetX = OffsetX;
            this.OffsetY = OffsetY;
            this.ParentEntity = ParentEntity;
            this.Handlers = Handlers;
        }

        public override string ToString()
        {
            return $"Collider";
        }

        internal virtual void Update()
        {
        }

        internal void HandleBotCollision(Collider Source, float Difference)
        {
            foreach (var item in Handlers)
            {
                item.BotCollision(this, Source);
            }
        }

        internal void HandleTopCollision(Collider Source, float Difference)
        {
            foreach (var item in Handlers)
            {
                item.TopCollision(this, Source);
            }
        }

        internal void HandleLeftCollision(Collider Source, float Difference)
        {
            foreach (var item in Handlers)
            {
                item.LeftCollision(this, Source);
            }
        }

        internal void HandleRightCollision(Collider Source, float Difference)
        {
            foreach (var item in Handlers)
            {
                item.RightCollision(this, Source);
            }
        }
    }

    public interface IHandleCollision
    {
        void BotCollision(Collider Self, Collider Other);
        void TopCollision(Collider Self, Collider Other);
        void LeftCollision(Collider Self, Collider Other);
        void RightCollision(Collider Self, Collider Other);
    }
}