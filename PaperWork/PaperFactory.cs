using GameCore;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaperWork
{
    class PaperFactory :
        Collider
        , IUpdateHandler
        , ICollisionHandler
    {
        private int cooldownCount = 0;
        private HashSet<Collider> PapersOnSpawnArea;
        private Random Random;
        private Color PreviousColor;
        public bool Disabled { get; set; }

        Color[] Colors = new Color[]
        {
            Color.Yellow,
            new Color(255, 100, 100),
            new Color(100, 255, 100),
            new Color(150, 150, 255)
        };
        private readonly Action<Collider> AddToWorld;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        public PaperFactory(Action<Collider> AddToWorld)
        {
            this.AddToWorld = AddToWorld;
            PapersOnSpawnArea = new HashSet<Collider>();
            Random = new Random();
            Width = (100 * 12) - 25;
            Height = 50;
            Y = 225;
            X = 125;
        }

        public void BotCollision(Collider other)
        {
            if (other is Paper || other is Player)
                PapersOnSpawnArea.Add(other);
        }

        public void LeftCollision(Collider other)
        {
            if (other is Paper || other is Player)
                PapersOnSpawnArea.Add(other);
        }

        public void RightCollision(Collider other)
        {
            if (other is Paper || other is Player)
                PapersOnSpawnArea.Add(other);
        }

        public void TopCollision(Collider other)
        {
            if (other is Paper || other is Player)
                PapersOnSpawnArea.Add(other);
        }

        public void Update()
        {
            //foreach (var item in PapersOnSpawnArea.OfType<Paper>())
            //{
            //    if (item.X > (100 * 11) + 12)
            //    {
            //        item.HorizontalSpeed = -2;
            //    }
            //    else
            //        item.HorizontalSpeed = 0;
            //}
            if (cooldownCount > 0)
            {
                cooldownCount--;
                return;
            }

            var x = (100 * 12) + 12 * World.SPACE_BETWEEN_THINGS;
            //foreach (var item in PapersOnSpawnArea.OrderByDescending(f => f.X))
            //{
            //    //if (item is Paper == false)
            //    //    continue;

            //    if (item.X == x || item is Player)
            //    {
            //        // x -= 100 + World.SPACE_BETWEEN_THINGS;
            //    }
            //}

            //if (x == 50)
            //{
            //    Restart();
            //    return;
            //}
            var y = 200 + World.SPACE_BETWEEN_THINGS;
            var paper = new Paper()
            {
                X = x,
                Y = y,
                DrawableX = x,
                DrawableY = y
            };

            var colors = Colors.Where(f=> PreviousColor != f).ToArray();
            paper.Color = colors[Random.Next(0, colors.Length)];
            PreviousColor = paper.Color;
            AddToWorld(paper);
            cooldownCount = 70;

            PapersOnSpawnArea.Clear();
        }
    }
}