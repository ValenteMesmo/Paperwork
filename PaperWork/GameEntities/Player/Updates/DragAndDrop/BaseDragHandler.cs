using GameCore;
using System;

namespace PaperWork.GameEntities.Player.Updates
{
    public abstract class BaseDragHandler : IHandleUpdates
    {
        private readonly Action SetGrabOnCooldown;
        private readonly Action<PapersEntity> GivePaperToPlayer;

        public BaseDragHandler(
            Action<PapersEntity> GivePaperToPlayer
            , Action SetGrabOnCooldown)
        {
            this.GivePaperToPlayer = GivePaperToPlayer;
            this.SetGrabOnCooldown = SetGrabOnCooldown;
        }

        protected abstract Entity GetEntityToGrab();

        public void Update(Entity entity)
        {
            var papers = GetEntityToGrab();
            if (papers == null)
                return;

            if (papers is PapersEntity == false)
                return;

            GivePaperToPlayer(papers as PapersEntity);
            SetGrabOnCooldown();
            papers.As<PapersEntity>().Target.Set(entity);

            foreach (var collider in papers.GetColliders())
            {
                collider.Disabled = true;
            }
        }
    }
}
