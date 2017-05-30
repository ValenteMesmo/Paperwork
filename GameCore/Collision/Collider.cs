using System;

namespace GameCore.Collision
{
    public abstract class Collider
    {
        internal readonly int OffsetX;
        internal readonly int OffsetY;

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
            Entity ParentEntity,
            int OffsetX,
            int OffsetY,
            float Width,
            float Height)
        {
            this.OffsetX = OffsetX;
            this.OffsetY = OffsetY;
            this.ParentEntity = ParentEntity;
            this.Width = Width;
            this.Height = Height;
        }

        internal virtual void HandleBotCollision(Collider Source, float Difference) { }
        internal virtual void HandleTopCollision(Collider Source, float Difference) { }
        internal virtual void HandleLeftCollision(Collider Source, float Difference) { }
        internal virtual void HandleRightCollision(Collider Source, float Difference) { }
        internal virtual void Update() { }


        public void SetOnBot(Collider Other)
        {
            ParentEntity.Position = new Coordinate2D(
                ParentEntity.Position.X
                , Other.ParentEntity.Position.Y + Other.ParentEntity.Height
            );
        }

        public void SetOnTop(Collider Other)
        {
            ParentEntity.Position = new Coordinate2D(
                ParentEntity.Position.X
                , Other.ParentEntity.Position.Y - ParentEntity.Height
            );
        }

        public void SetOnRight(Collider Other)
        {
            ParentEntity.Position = new Coordinate2D(
                 Other.ParentEntity.Position.X + Other.ParentEntity.Width
                , ParentEntity.Position.Y
            );
        }

        public void SetOnLeft(Collider Other)
        {
            ParentEntity.Position = new Coordinate2D(
                 Other.ParentEntity.Position.X - ParentEntity.Width
                , ParentEntity.Position.Y
            );
        }
    }
}