﻿using GameCore;
using System;
using System.Collections.Generic;

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

        protected abstract IEnumerable<Entity> GetEntityToGrab();

        public void Update(Entity entity)
        {
            var papers = GetEntityToGrab();
            if (papers == null)
                return;

            if (papers is PapersEntity == false)
                return;

            foreach (var item in papers)
            {
                if (item is PapersEntity == false)
                    continue;

                GivePaperToPlayer(item as PapersEntity);
                SetGrabOnCooldown();
                item.As<PapersEntity>().Target.Set(entity);

                foreach (var collider in item.GetColliders())
                {
                    collider.Disabled = true;
                }

                break;
            }
        }
    }
}
