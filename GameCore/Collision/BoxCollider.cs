namespace GameCore.Collision
{
    public class BoxCollider : Collider
    {
        private readonly IHandleCollision[] Handlers;
        private int Id;

        public BoxCollider(
            Entity ParentEntity
            , float Width
            , float Height
            , int OffsetX
            , int OffsetY
            , params IHandleCollision[] Handlers
            ) : base(ParentEntity, OffsetX, OffsetY, Width, Height)
        {
            this.Handlers = Handlers;
            Id = ParentEntity.GetColliders().Count;
        }

        public override string ToString()
        {
            return $"{nameof(BoxCollider)} {Id} ({ParentEntity.ToString()})";
        }

        internal override void HandleBotCollision(Collider Source, float Difference)
        {
            foreach (var item in Handlers)
            {
                item.BotCollision(this, Source);
            }
        }

        internal override void HandleTopCollision(Collider Source, float Difference)
        {
            foreach (var item in Handlers)
            {
                item.TopCollision(this, Source);
            }
        }

        internal override void HandleLeftCollision(Collider Source, float Difference)
        {
            foreach (var item in Handlers)
            {
                item.LeftCollision(this, Source);
            }
        }

        internal override void HandleRightCollision(Collider Source, float Difference)
        {
            foreach (var item in Handlers)
            {
                item.RightCollision(this, Source);
            }
        }

    }
}