using GameCore;
using System;

namespace PaperWork.GameEntities.Collisions
{
    public class FollowOtherEntity : UpdateHandler
    {
        Coordinate2D bonus;
        private readonly Func<Entity> GetTarget;

        public FollowOtherEntity(
            Entity ParentEntity,
            Coordinate2D bonus,
            Func<Entity> GetTarget) : base(ParentEntity)
        {
            this.bonus = bonus;
            this.GetTarget = GetTarget;
        }

        public override void Update()
        {
            var target = GetTarget();

            if (target == null)
                return;

            ParentEntity.Position = new Coordinate2D(
                target.Position.X + bonus.X,
                target.Position.Y + bonus.Y);
        }
    }
}
