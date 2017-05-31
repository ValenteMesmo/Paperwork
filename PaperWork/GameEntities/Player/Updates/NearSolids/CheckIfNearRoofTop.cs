using GameCore;
using System;
using System.Collections.Generic;

namespace PaperWork.GameEntities.Player.Updates
{

    public class CheckIfNearRoofTop : IHandleUpdates
    {
        private readonly Func<IEnumerable<Entity>> GetTopEntity;
        private readonly Action<Entity> SetRoofTop;
        private readonly Entity player;

        public CheckIfNearRoofTop(
            Entity player
            , Func<IEnumerable<Entity>> GetTopEntity
            , Action<Entity> SetRoofTop
        )
        {
            this.GetTopEntity = GetTopEntity;
            this.SetRoofTop = SetRoofTop;
            this.player = player;
        }

        public void Update()
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
