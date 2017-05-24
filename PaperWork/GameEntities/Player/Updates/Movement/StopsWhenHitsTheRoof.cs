using GameCore;
using System;

namespace PaperWork
{
    public class StopsWhenHitsTheRoof<T> : IHandleUpdates where T : Entity
    {
        private readonly Func<Entity> NearRoof;
        private readonly Action<float> SetVerticalSpeed;

        public StopsWhenHitsTheRoof(
            Func<Entity> NearRoof
            , Action<float> SetVerticalSpeed)
        {
            this.NearRoof = NearRoof;
            this.SetVerticalSpeed = SetVerticalSpeed;
        }

        public void Update(Entity entity)
        {
            var roof = NearRoof();
            if (roof == null
                || roof is T == false)
                return;

            if (roof.Position.Y + roof.Height - entity.Position.Y > 1)
            {
                SetVerticalSpeed(0);
                entity.Position = new Coordinate2D(
                    entity.Position.X
                    , roof.Position.Y + roof.Height);
            }
        }
    }
}
