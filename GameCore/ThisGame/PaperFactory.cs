using GameCore;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace PaperWork
{
    class PaperFactory :
         IUpdateHandler
    {
        private int cooldownCount = 0;
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
            Random = new Random();
            Width = (100 * 12) - 25;
            Height = 50;
            Y = 125;
            X = 125;
        }

        public void Update()
        {
            if (cooldownCount > 0)
            {
                cooldownCount--;
                return;
            }

            var x = (100 * 12) + 12 * World.SPACE_BETWEEN_THINGS;
 
            var y = 100 + World.SPACE_BETWEEN_THINGS;
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
            
        }
    }
}