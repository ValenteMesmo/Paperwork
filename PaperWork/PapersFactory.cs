using GameCore;
using PaperWork.GameEntities.Collisions;
using System;

namespace PaperWork
{
    class PapersFactory : Entity
    {
        public PapersFactory(Action<Entity> RemoveFromWorld, Action<Entity> AddToWorld) : base(RemoveFromWorld)
        {
            AddUpdateHandlers(
                new GeneratePapers(RemoveFromWorld, AddToWorld)
            );

        }
    }

    class GeneratePapers : IHandleEntityUpdates
    {
        private readonly Action<Entity> RemoveFromWorld;
        Cooldown cooldown = new Cooldown(2000);
        private readonly Action<Entity> AddToWorld;

        public GeneratePapers(
            Action<Entity> RemoveFromWorld
            , Action<Entity> AddToWorld)
        {
            this.RemoveFromWorld = RemoveFromWorld;
            this.AddToWorld = AddToWorld;
        }

        public void Update(Entity entity)
        {
            if (cooldown.CooldownEnded())
            {
                AddToWorld(new PapersEntity(50, RemoveFromWorld) { Position = entity.Position });
                cooldown.TriggerCooldown();
            }
        }
    }
}
