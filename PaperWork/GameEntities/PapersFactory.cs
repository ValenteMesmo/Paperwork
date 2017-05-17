using GameCore;
using Microsoft.Xna.Framework;
using PaperWork.GameEntities.Collisions;
using System;
using GameCore.Collision;
using System.Collections.Generic;

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
        Color[] Colors = new Color[] { Color.Red, Color.Green, Color.Blue };
        Random Random = new Random();

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
                var paper = new PapersEntity(50, RemoveFromWorld) { Position = entity.Position };
                paper.Color = Colors[Random.Next(0, Colors.Length )];

                AddToWorld(paper);
                cooldown.TriggerCooldown();
            }
        }
    }
}
