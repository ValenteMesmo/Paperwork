using System.Collections.Generic;

namespace GameCore.Collision
{
    public abstract class BaseCollider
    {
        public Entity ParentEntity { get; }
        public float Width { get; protected set; }
        public float Height { get; protected set; }
        public bool Disabled { get; set; }
        public Coordinate2D LocalPosition { get; set; }
        public Coordinate2D Position
        {
            get
            {
                var position = LocalPosition;
                position.X += ParentEntity.Position.X;
                position.Y += ParentEntity.Position.Y;
                return position;
            }
            set
            {
                var position = value;
                position.X -= ParentEntity.Position.X;
                position.Y -= ParentEntity.Position.Y;
                LocalPosition = position;
            }
        }
        private readonly IList<IHandleCollision> CollisionHandlers;
        private readonly IList<IHandleTriggers> TriggerHandlers;
        private readonly IList<BaseCollider> PreviousUpdateCollisions;
        private readonly IList<BaseCollider> CurrentUpdateCollisions;

        public BaseCollider(Entity ParentEntity, float Width, float Height)
        {
            this.Width = Width;
            this.Height = Height;
            this.ParentEntity = ParentEntity;
            CollisionHandlers = new List<IHandleCollision>();
            TriggerHandlers = new List<IHandleTriggers>();
            PreviousUpdateCollisions = new List<BaseCollider>();
            CurrentUpdateCollisions = new List<BaseCollider>();
        }

        internal virtual void Update() { }

        internal virtual void AfterUpdate() { }

        internal abstract void CollisionFromBelow(BaseCollider other);

        internal abstract void CollisionFromAbove(BaseCollider other);

        internal abstract void CollisionFromTheLeft(BaseCollider other);

        internal abstract void CollisionFromTheRight(BaseCollider other);
    }
}