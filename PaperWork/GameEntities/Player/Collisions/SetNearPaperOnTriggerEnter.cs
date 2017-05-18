using GameCore;
using GameCore.Collision;
using System;

namespace PaperWork.GameEntities.Player.Collisions
{
    public class SetNearEntityOnTriggerEnter : IHandleTriggers
    {
        private readonly Action<Entity> SetNearPaper;
        private readonly Func<Entity> GetNearPaper;

        public SetNearEntityOnTriggerEnter(
            Action<Entity> SetNearPaper
            , Func<Entity> GetNearPaper)
        {
            this.SetNearPaper = SetNearPaper;
            this.GetNearPaper = GetNearPaper;
        }

        public void TriggerEnter(BaseCollider triggerCollider, BaseCollider other)
        {
            if (other.ParentEntity is PapersEntity
                || other.ParentEntity is SolidBlock)
                SetNearPaper(other.ParentEntity);
        }

        public void TriggerExit(BaseCollider triggerCollider, BaseCollider other)
        {
            if (GetNearPaper() == other.ParentEntity)
                SetNearPaper(null);
        }
    }
}
