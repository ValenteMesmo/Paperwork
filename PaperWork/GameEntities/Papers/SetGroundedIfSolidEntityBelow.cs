using GameCore;
using PaperWork.GameEntities;
using System;
using System.Collections.Generic;

namespace PaperWork
{
    public class SetGroundedIfSolidEntityBelow : IHandleUpdates
    {
        private readonly Func<IEnumerable<Entity>> GetEntitiesBelow;
        private readonly Action<bool> SetGrounded;

        public SetGroundedIfSolidEntityBelow(
            Func<IEnumerable<Entity>> GetEntitiesBelow
            , Action<bool> SetGrounded)
        {
            this.GetEntitiesBelow = GetEntitiesBelow;
            this.SetGrounded = SetGrounded;
        }

        public void Update(Entity entity)
        {
            foreach (var item in GetEntitiesBelow())
            {
                if (item is PapersEntity
                    || item is SolidBlock)
                {
                    SetGrounded(true);
                    return;
                }
            }

            SetGrounded(false);
        }
    }
}
