using GameCore;
using System;
using System.Collections.Generic;

namespace PaperWork.GameEntities.Player.Updates
{
    public class CheckIfGrounded : IHandleUpdates
    {
        private readonly Func<IEnumerable<Entity>> GetBotEntity;
        private readonly Action<Entity> SetGrounded;

        public CheckIfGrounded(
            Func<IEnumerable<Entity>> GetBotEntity
            , Action<Entity> SetGrounded
        )
        {
            this.GetBotEntity = GetBotEntity;
            this.SetGrounded = SetGrounded;
        }

        public void Update(Entity player)
        {
            SetGrounded(
                IsPlayerGrounded(player));
        }

        private Entity IsPlayerGrounded(Entity entity)
        {
            var otherEntities = GetBotEntity();
            if (otherEntities == null)
            {
                return null;
            }

            foreach (var other in otherEntities)
            {
                var difference = other.Position.Y - (entity.Position.Y + entity.Height);

                if (
                        (other is PapersEntity || other is SolidBlock)
                        && (difference < 5f && difference > -5f)
                    )
                    return other;
            }

            return null;
        }
    }
}
