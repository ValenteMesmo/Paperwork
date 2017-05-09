using System.Collections.Generic;

namespace GameCore.Collision
{
    public class GameCollider
    {
        public Entity ParentEntity { get; }
        public float Width { get; protected set; }
        public float Height { get; protected set; }
        public Coordinate2D LocalPosition { get; set; }
        public IList<CollisionHandler> CollisionHandlers { get; }
        public bool Disabled { get; set; }
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

        public GameCollider(Entity ParentEntity, float Width, float Height)
        {
            this.Width = Width;
            this.Height = Height;
            this.ParentEntity = ParentEntity;
            this.CollisionHandlers = new List<CollisionHandler>();
        }

        public void CollisionFromBelow(GameCollider other)
        {
            foreach (var item in CollisionHandlers)
            {
                item.CollisionFromBelow(other);
            }
        }

        public void CollisionFromAbove(GameCollider other)
        {
            foreach (var item in CollisionHandlers)
            {
                item.CollisionFromAbove(other);
            }
        }

        public void CollisionFromTheLeft(GameCollider other)
        {
            foreach (var item in CollisionHandlers)
            {
                item.CollisionFromTheLeft(other);
            }
        }

        public void CollisionFromTheRight(GameCollider other)
        {
            foreach (var item in CollisionHandlers)
            {
                item.CollisionFromTheRight(other);
            }
        }
    }
}