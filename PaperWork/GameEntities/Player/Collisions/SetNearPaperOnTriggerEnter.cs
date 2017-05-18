using GameCore.Collision;
using System;

namespace PaperWork.GameEntities.Player.Collisions
{
    public class SetNearPaperOnTriggerEnter : IHandleTriggers
    {
        private readonly Action<PapersEntity> SetNearPaper;
        private readonly Func<PapersEntity> GetNearPaper;

        public SetNearPaperOnTriggerEnter(
            Action<PapersEntity> SetNearPaper
            , Func<PapersEntity> GetNearPaper)
        {
            this.SetNearPaper = SetNearPaper;
            this.GetNearPaper = GetNearPaper;
        }

        public void TriggerEnter(BaseCollider triggerCollider, BaseCollider other)
        {
            if (other.ParentEntity is PapersEntity)
                SetNearPaper(other.ParentEntity as PapersEntity);
        }

        public void TriggerExit(BaseCollider triggerCollider, BaseCollider other)
        {
            if (GetNearPaper() == other.ParentEntity)
                SetNearPaper(null);
        }
    }
}
