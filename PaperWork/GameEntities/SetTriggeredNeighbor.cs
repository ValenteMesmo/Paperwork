using GameCore;
using GameCore.Collision;
using System;

namespace PaperWork
{
    class SetTriggeredNeighbor : IHandleTriggers
    {
        private readonly Action<PapersEntity> SaveNeighbor;
        private readonly Func<bool> HasNoNeighbor;
        private readonly Action DeleteNeighbor;

        public SetTriggeredNeighbor(
             Action<PapersEntity> SaveNeighbor
            , Action DeleteNeighbor
             ,Func<bool> HasNoNeighbor)
        {
            this.SaveNeighbor = SaveNeighbor;
            this.DeleteNeighbor = DeleteNeighbor;
            this.HasNoNeighbor = HasNoNeighbor;
        }

        public void TriggerEnter(BaseCollider triggerCollider, BaseCollider other)
        {
            if (other.ParentEntity is PapersEntity)
            {
                if(HasNoNeighbor())
                SaveNeighbor(other.ParentEntity as PapersEntity);
            }
        }

        public void TriggerExit(BaseCollider triggerCollider, BaseCollider other)
        {
            if (other.ParentEntity is PapersEntity)
            {
                DeleteNeighbor();
            }
        }
    }
}
