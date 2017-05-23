using GameCore;
using System;
using System.Collections.Generic;

namespace PaperWork.GameEntities.Player.Updates
{
    public class CheckIfNearLeftWall : IHandleUpdates
    {
        private readonly Action<Entity> SetNearLeftWall;
        private readonly Func<IEnumerable<Entity>> GetEntitiesOnTheLeft;

        public CheckIfNearLeftWall(
            Action<Entity> SetNearLeftWall
            , Func<IEnumerable<Entity>> GetEntitiesOnTheLeft)
        {
            this.SetNearLeftWall = SetNearLeftWall;
            this.GetEntitiesOnTheLeft = GetEntitiesOnTheLeft;
        }

        public void Update(Entity entity)
        {
            SetNearLeftWall(IsNearLeftWall(entity));
        }

        private Entity IsNearLeftWall(Entity entity)
        {
            var others = GetEntitiesOnTheLeft();
            if (others == null)
            {
                return null;
            }

            foreach (var other in others)
            {
                var difference = (other.Position.X + other.Width)
                            - entity.Position.X;
                if (
                        (other is PapersEntity || other is SolidBlock)
                    &&
                        (difference < 5f && difference > -5f)
                    )
                {
                    return other;
                }
            }

            return null;
        }
    }
}
