using GameCore;
using System;
using System.Collections.Generic;

namespace PaperWork
{
    public class ChangeStateWhenCornered : IHandleUpdates
    {
        private readonly Func<IEnumerable<Entity>> ObjectInsideThePlayer;
        private readonly Action ChangeState;

        public ChangeStateWhenCornered(
            Func<IEnumerable<Entity>> ObjectInsideThePlayer
            , Action ChangeState)
        {
            this.ObjectInsideThePlayer = ObjectInsideThePlayer;
            this.ChangeState = ChangeState;
        }

        public void Update(Entity entity)
        {
            var otherEntities = ObjectInsideThePlayer();
            foreach (var other in otherEntities)
            {
                if (other == null || other is PapersEntity == false)
                    continue;
                ChangeState();
            }
        }
    }
}
