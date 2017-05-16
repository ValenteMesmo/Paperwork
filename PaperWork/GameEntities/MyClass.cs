using GameCore;
using GameCore.Collision;
using System;

namespace PaperWork
{
    class SetTriggeredNeighbor : IHandleTriggers
    {
        private readonly Action<PapersEntity> SaveNeighbor;
        private readonly Action DeleteNeighbor;

        public SetTriggeredNeighbor(
             Action<PapersEntity> SaveNeighbor
            , Action DeleteNeighbor)
        {
            this.SaveNeighbor = SaveNeighbor;
            this.DeleteNeighbor = DeleteNeighbor;
        }

        public void TriggerEnter(BaseCollider triggerCollider, BaseCollider other)
        {
            if (other.ParentEntity is PapersEntity)
            {
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
