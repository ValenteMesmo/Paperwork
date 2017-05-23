using GameCore;
using System;
using System.Collections.Generic;

namespace PaperWork.GameEntities.Player.Updates
{
    public class CheckIfGrounded : IHandleUpdates
    {
        private readonly Func<IEnumerable<Entity>> GetBotEntity;
        private readonly Action<bool> SetGrounded;
        private readonly float PlayerHeight;

        public CheckIfGrounded(
            Func<IEnumerable<Entity>> GetBotEntity
            , Action<bool> SetGrounded
            , float PlayerHeight
        )
        {
            this.GetBotEntity = GetBotEntity;
            this.SetGrounded = SetGrounded;
            this.PlayerHeight = PlayerHeight;
        }

        public void Update(Entity player)
        {
            SetGrounded(ShouldEnableJump(player));
        }

        private bool ShouldEnableJump(Entity player)
        {
            var other = GetBotEntity();
            if (other == null)
            {
                return false;
            }

            foreach (var item in other)
            {
                if ((item is PapersEntity || item is SolidBlock)
                    && item.Position.Y - (player.Position.Y + PlayerHeight) < 1)
                    return true;
            }

            return false;
        }
    }
}
