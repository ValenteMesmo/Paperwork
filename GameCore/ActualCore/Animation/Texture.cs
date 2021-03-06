﻿using Microsoft.Xna.Framework;

namespace GameCore
{
    public class Texture
    {
        public readonly Rectangle? PositionOnSpriteSheet;

        public Texture(string Name, int X, int Y, int Width, int Height, Rectangle? PositionOnSpriteSheet = null)
        {
            ZIndex = 1;
            Color = Color.White;
            this.Name = Name;
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.PositionOnSpriteSheet = PositionOnSpriteSheet;
        }

        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public string Name { get; }
        public Color Color { get; set; }
        public bool Flipped { get; set; }
        public float ZIndex { get; set; }
    }
}
