using GameCore;
using System;
using System.Collections.Generic;

namespace PaperWork.GameEntities.Player.Updates
{
    public class CheckIfNearRightWall : IHandleUpdates
    {
        private readonly Action<Entity> SetNearRightWall;
        private readonly Func<IEnumerable<Entity>> GetEntitiesOnTheRight;

        public CheckIfNearRightWall(
            Action<Entity> SetNearRightWall
            , Func<IEnumerable<Entity>> GetEntitiesOnTheRight)
        {
            this.SetNearRightWall = SetNearRightWall;
            this.GetEntitiesOnTheRight = GetEntitiesOnTheRight;
        }

        public void Update(Entity entity)
        {
            SetNearRightWall(IsNearLeftWall(entity));
        }

        private Entity IsNearLeftWall(Entity entity)
        {
            var others = GetEntitiesOnTheRight();
            if (others == null)
            {
                return null;
            }

            foreach (var other in others)
            {
                var difference = (entity.Position.X + entity.Width) - other.Position.X;
                if ((other is PapersEntity || other is SolidBlock)
                    && (difference < 5f && difference > -5f)
                    )
                    return other;
            }

            return null;
        }
    }
}
