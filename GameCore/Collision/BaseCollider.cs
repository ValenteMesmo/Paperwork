using System.Collections.Generic;
using System.Linq;

namespace GameCore.Collision
{
    public struct Collision
    {
        public readonly Collider Source;
        public readonly float Difference;

        public Collision(
            Collider Source
            , float Difference)
        {
            this.Source = Source;
            this.Difference = Difference;
        }
    }

    public class Collider
    {
        internal readonly int OffsetX;
        internal readonly int OffsetY;
        public readonly bool IsTrigger;

        public Entity ParentEntity { get; }

        internal float Width { get; set; }
        internal float Height { get; set; }
        public bool Disabled { get; set; }

        internal readonly List<Collision> TopCollisions;
        internal readonly List<Collision> LeftCollisions;
        internal readonly List<Collision> RightCollisions;
        internal readonly List<Collision> BotCollisions;

        public IEnumerable<Collision> GetTopCollisions()
        {
            return TopCollisions;
        }

        public IEnumerable<Collision> GetBotCollisions()
        {
            return BotCollisions;
        }

        public IEnumerable<Collision> GetLeftCollisions()
        {
            return LeftCollisions;
        }

        public IEnumerable<Collision> GetRightCollisions()
        {
            return RightCollisions;
        }

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
            , float Width
            , float Height
            , int OffsetX = 0
            , int OffsetY = 0
            , bool IsTrigger = false
            )
        {
            this.Width = Width;
            this.Height = Height;
            this.OffsetX = OffsetX;
            this.OffsetY = OffsetY;
            this.ParentEntity = ParentEntity;
            this.IsTrigger = IsTrigger;

            TopCollisions = new List<Collision>();
            LeftCollisions = new List<Collision>();
            RightCollisions = new List<Collision>();
            BotCollisions = new List<Collision>();
        }

        public override string ToString()
        {
            return $"{ParentEntity}'s Collider";
        }

        public IEnumerable<Entity> GetEntities()
        {
            var result = new List<Entity>();
            result.AddRange(BotCollisions.Select(f => f.Source.ParentEntity));
            result.AddRange(TopCollisions.Select(f => f.Source.ParentEntity));
            result.AddRange(LeftCollisions.Select(f => f.Source.ParentEntity));
            result.AddRange(RightCollisions.Select(f => f.Source.ParentEntity));
            return result.Distinct();
        }
    }
}