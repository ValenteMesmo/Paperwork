﻿using GameCore;
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
            Y = 125;
            X = 125;
        }

        public void BotCollision(Collider other)
        {
            if (other is Paper)
                PapersOnSpawnArea.Add(other);
        }

        public void LeftCollision(Collider other)
        {
            if (other is Paper)
                PapersOnSpawnArea.Add(other);
        }

        public void RightCollision(Collider other)
        {
            if (other is Paper)
                PapersOnSpawnArea.Add(other);
        }

        public void TopCollision(Collider other)
        {
            if (other is Paper)
                PapersOnSpawnArea.Add(other);
        }

        public void Update()
        {
            if (cooldownCount > 0)
            {
                cooldownCount--;
                return;
            }

            var x = (100 * 12) + 12;
            foreach (var item in PapersOnSpawnArea.OrderByDescending(f => f.X))
            {
                if (item is Paper == false)
                    continue;

                if (item.X == x)
                {
                    x -= item.Width + World.SPACE_BETWEEN_THINGS;
                }
            }

            //if (x == 50)
            //{
            //    Restart();
            //    return;
            //}

            var paper = new Paper()
            {
                X = x
                ,Y = 200
                ,DrawableX = x
                ,DrawableY = 100
            };
            paper.Color = Colors[Random.Next(0, Colors.Length)];

            AddToWorld(paper);
            cooldownCount = 200;

            PapersOnSpawnArea.Clear();
        }
    }
}