using GameCore;
using System;
using System.Collections.Generic;

namespace PaperWork.GameEntities.Player.Updates
{
    public abstract class BaseDragHandler : IHandleUpdates
    {
        private readonly Action SetGrabOnCooldown;
        private readonly Action<PapersEntity> GivePaperToPlayer;
        private readonly Entity entity;

        public BaseDragHandler(
            Entity entity
            ,Action<PapersEntity> GivePaperToPlayer
            , Action SetGrabOnCooldown)
        {
            this.GivePaperToPlayer = GivePaperToPlayer;
            this.SetGrabOnCooldown = SetGrabOnCooldown;
            this.entity = entity;
        }

        protected abstract IEnumerable<Entity> GetEntityToGrab();

        public void Update()
        {
            var entities = GetEntityToGrab();
            if (entities == null)
                return;

            foreach (var papers in entities)
            {
                if (papers is PapersEntity == false)
                    continue;

                GivePaperToPlayer(papers as PapersEntity);
                SetGrabOnCooldown();
                papers.As<PapersEntity>().Target.Set(entity);

                foreach (var collider in papers.GetColliders())
                {
                    collider.Disabled = true;
                }

                break;
            }
        }
    }
}
