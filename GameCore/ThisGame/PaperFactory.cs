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

        public PaperFactory(Action<Collider> AddToWorld)
        {
            this.AddToWorld = AddToWorld;
            this.Random = new Random();
        }

        public void Update()
        {
            if (cooldownCount > 0)
            {
                cooldownCount--;
                return;
            }

            var x = (1000 * 13) + 12 * World.SPACE_BETWEEN_THINGS;

            var y = 2000 + World.SPACE_BETWEEN_THINGS;
            var paper = new Paper()
            {
                X = x,
                Y = y,
                DrawableX = x,
                DrawableY = y
            };

            Color newcolor = GetNewColor();
            paper.Color = newcolor;
            PreviousColor = paper.Color;
            AddToWorld(paper);
            cooldownCount = 70;

        }

        Color[] fakeColors = new Color[] { Color.Blue, Color.Red, Color.Red, Color.Green, Color.Blue };
        int fakeColorIndex = -1;
        private Color GetNewColor()
        {
            var usingFakeColorList = false;
            if (usingFakeColorList)
            {
                fakeColorIndex++;

                if (fakeColorIndex >= fakeColors.Length)
                    fakeColorIndex = 0;
                return fakeColors[fakeColorIndex];
            }
            else
            {
                var colors = Colors.Where(f => PreviousColor != f).ToArray();
                var newcolor = colors[Random.Next(0, colors.Length)];
                return newcolor;
            }
        }
    }
}