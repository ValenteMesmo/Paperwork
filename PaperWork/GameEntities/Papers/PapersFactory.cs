using GameCore;
using Microsoft.Xna.Framework;
using PaperWork.GameEntities.Collisions;
using System;
using GameCore.Collision;
using System.Collections.Generic;
using System.Linq;

namespace PaperWork
{
    public class PapersFactory : Entity
    {
        public PapersFactory(Action<Entity> RemoveFromWorld, Action<Entity> AddToWorld) : base(RemoveFromWorld)
        {
            var feeler = new Trigger(this, (50 * 12) - 4, 10);
            feeler.Position = new Coordinate2D(52, 0);
            Colliders.Add(feeler);
            AddUpdateHandlers(
                new GeneratePapers(RemoveFromWorld, AddToWorld, feeler.GetEntities)
            );
        }
    }

    class GeneratePapers : IHandleEntityUpdates
    {
        private readonly Action<Entity> RemoveFromWorld;
        Cooldown cooldown = new Cooldown(2000);
        private readonly Action<Entity> AddToWorld;
        Color[] Colors = new Color[] { Color.White, new Color(255, 100, 100), new Color(100, 255, 100), new Color(100, 100, 255) };
        Random Random = new Random();
        private readonly Func<IEnumerable<Entity>> GetEntitiesOnTheTrigger;

        public GeneratePapers(
            Action<Entity> RemoveFromWorld
            , Action<Entity> AddToWorld
            , Func<IEnumerable<Entity>>GetEntitiesOnTheTrigger)
        {
            this.RemoveFromWorld = RemoveFromWorld;
            this.AddToWorld = AddToWorld;
            this.GetEntitiesOnTheTrigger = GetEntitiesOnTheTrigger;
        }

        public void Update(Entity entity)
        {
            if (cooldown.CooldownEnded())
            {
                var coordenates = new List<Coordinate2D>();
                var others = GetEntitiesOnTheTrigger();

                var x = 50*12;
                foreach (var item in others.OrderByDescending(f=> f.Position.X))
                {
                    if (item is PapersEntity == false)
                        continue;

                    if (item.Position.X == x)
                    {
                        x -= 50;
                    }
                }

                var paper = new PapersEntity(50, RemoveFromWorld)
                {
                    Position = new Coordinate2D(x, 5)
                };
                paper.Color = Colors[Random.Next(0, Colors.Length)];

                AddToWorld(paper);
                cooldown.TriggerCooldown();
            }
        }
    }
}
