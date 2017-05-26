using GameCore;
using PaperWork.GameEntities;
using System;
using System.Collections.Generic;

namespace PaperWork
{
    public class MoveWhenPaperInsidePlayer : IHandleUpdates
    {
        private readonly Action<float> SetVerticalSpeed;
        private readonly Action<float> SetHorizontalSpeed;
        private readonly Func<IEnumerable<Entity>> GetEntitiesInsideThePlayer;
        private readonly Func<Entity> LeftWall;
        private readonly Func<Entity> BotWall;

        public MoveWhenPaperInsidePlayer(
             Action<float> SetVerticalSpeed
            , Action<float> SetHorizontalSpeed
            , Func<IEnumerable<Entity>> GetEntitiesInsideThePlayer
            ,Func<Entity> LeftWall
            , Func<Entity> BotWall)
        {
            this.SetVerticalSpeed = SetVerticalSpeed;
            this.SetHorizontalSpeed = SetHorizontalSpeed;
            this.GetEntitiesInsideThePlayer = GetEntitiesInsideThePlayer;
            this.LeftWall = LeftWall;
            this.BotWall = BotWall;
        }

        public void Update(Entity entity)
        {
            var items = GetEntitiesInsideThePlayer();
            if (items == null)
                return;

            foreach (var item in items)
            {
                if (item is PapersEntity == false)
                    continue;

                var wall = LeftWall();
                var botWall = BotWall();
                if (
                    (wall != null && (wall is SolidBlock))
                    ||
                    (botWall != null && (botWall is SolidBlock))
                    )
                {
                    SetVerticalSpeed(-6);
                }
                else
                {
                    SetVerticalSpeed(0);
                    SetHorizontalSpeed(-2);
                }
            }
        }
    }
}
