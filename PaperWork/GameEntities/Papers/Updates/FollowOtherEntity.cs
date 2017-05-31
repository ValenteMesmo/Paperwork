using GameCore;
using System;

namespace PaperWork.GameEntities.Collisions
{
    public class FollowOtherEntity : IHandleUpdates
    {
        private readonly Coordinate2D bonus;
        private readonly Func<Entity> GetTarget;
        private readonly Action<float> SetVerticalSpeed;
        private readonly Action<float> SetHorizontalSpeed;
        private readonly Entity entity;

        public FollowOtherEntity(
            Entity entity
            ,Coordinate2D bonus
            ,Func<Entity> GetTarget
            ,Action<float> SetVerticalSpeed
            ,Action<float> SetHorizontalSpeed)
        {
            this.entity = entity;
            this.bonus = bonus;
            this.GetTarget = GetTarget;
            this.SetVerticalSpeed = SetVerticalSpeed;
            this.SetHorizontalSpeed = SetHorizontalSpeed;
        }

        public void Update()
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
