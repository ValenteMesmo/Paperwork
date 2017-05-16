using GameCore;
using System;

namespace PaperWork.GameEntities.Collisions
{
    public class FollowOtherEntity : IHandleEntityUpdates
    {
        Coordinate2D bonus;
        private readonly Func<Entity> GetTarget;

        public FollowOtherEntity(
            Coordinate2D bonus,
            Func<Entity> GetTarget)
        {
            this.bonus = bonus;
            this.GetTarget = GetTarget;
        }

        public void Update(Entity entity)
        {
            var target = GetTarget();

            if (target == null)
                return;

            entity.Position = new Coordinate2D(
                target.Position.X + bonus.X,
                target.Position.Y + bonus.Y);
        }
    }
}
