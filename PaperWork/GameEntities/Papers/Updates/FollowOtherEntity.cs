using GameCore;
using System;

namespace PaperWork.GameEntities.Collisions
{
    public class FollowOtherEntity : IHandleEntityUpdates
    {
        private readonly Coordinate2D bonus;
        private readonly Func<Entity> GetTarget;
        private readonly Action<float> SetVerticalSpeed;
        private readonly Action<float> SetHorizontalSpeed;

        public FollowOtherEntity(
            Coordinate2D bonus
            ,Func<Entity> GetTarget
            ,Action<float> SetVerticalSpeed
            ,Action<float> SetHorizontalSpeed)
        {
            this.bonus = bonus;
            this.GetTarget = GetTarget;
            this.SetVerticalSpeed = SetVerticalSpeed;
            this.SetHorizontalSpeed = SetHorizontalSpeed;
        }

        public void Update(Entity entity)
        {
            var target = GetTarget();

            if (target == null)
                return;

            SetVerticalSpeed(0);
            SetHorizontalSpeed(0);

            entity.Position = new Coordinate2D(
                target.Position.X + bonus.X,
                target.Position.Y + bonus.Y);
        }
    }
}
