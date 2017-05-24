using GameCore;
using System;
using System.Collections.Generic;

namespace PaperWork.GameEntities.Player.Updates
{

    public class CheckIfNearRoofTop : IHandleUpdates
    {
        private readonly Func<IEnumerable<Entity>> GetTopEntity;
        private readonly Action<Entity> SetRoofTop;

        public CheckIfNearRoofTop(
            Func<IEnumerable<Entity>> GetTopEntity
            , Action<Entity> SetGrounded
        )
        {
            this.GetTopEntity = GetTopEntity;
            this.SetRoofTop = SetGrounded;
        }

        public void Update(Entity player)
        {
            SetRoofTop(
                IsPlayerNearRoofTop(player));
        }

        private Entity IsPlayerNearRoofTop(Entity entity)
        {
            var otherEntities = GetTopEntity();
            if (otherEntities == null)
            {
                return null;
            }

            foreach (var other in otherEntities)
            {
                var difference = other.Position.Y + other.Height - entity.Position.Y ;

                if (
                        (other is SolidBlock || other is PapersEntity)
                        && (difference < 5f && difference > -5f)
                    )
                    return other;
            }

            return null;
        }
    }
}
