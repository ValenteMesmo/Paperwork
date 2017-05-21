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
        Cooldown cooldown = new Cooldown(2000);
        private readonly Action<Entity> AddToWorld;
        Color[] Colors = new Color[]
        {
            Color.Yellow,
            new Color(255, 100, 100),
            new Color(100, 255, 100),
            new Color(150, 150, 255)
        };
        Random Random = new Random();
        private readonly Trigger feeler;

        public PapersFactory(Action<Entity> AddToWorld)
        {
            this.AddToWorld = AddToWorld;
            feeler = new Trigger(this, (50 * 12) - 4, 10);
            feeler.Position = new Coordinate2D(52, 0);
            Colliders.Add(feeler);
        }

        protected override void OnUpdate()
        {
            if (cooldown.Ended())
            {
                var coordenates = new List<Coordinate2D>();
                var others = feeler.GetEntities();

                var x = 50 * 12;
                foreach (var item in others.OrderByDescending(f => f.Position.X))
                {
                    if (item is PapersEntity == false)
                        continue;

                    if (item.Position.X == x)
                    {
                        x -= 50;
                    }
                }

                var paper = new PapersEntity(50)
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
