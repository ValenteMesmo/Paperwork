using GameCore;
using GameCore.Collision;
using System;

namespace PaperWork
{
    public partial class PapersEntity : Entity
    {
        class MyClass : IHandleTriggers
        {
            private readonly Action<PapersEntity> SaveNeighbor;
            private readonly Action DeleteNeighbor;
            private readonly Action Vefify3Chain;

            public MyClass(
                 Action<PapersEntity> SaveNeighbor
                , Action DeleteNeighbor
                , Action Vefify3Chain)
            {
                this.SaveNeighbor = SaveNeighbor;
                this.DeleteNeighbor = DeleteNeighbor;
                this.Vefify3Chain = Vefify3Chain;
            }

            public void TriggerEnter(BaseCollider triggerCollider, BaseCollider other)
            {
                if (other.ParentEntity is PapersEntity)
                {
                    SaveNeighbor(other.ParentEntity as PapersEntity);
                    Vefify3Chain();
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
}
