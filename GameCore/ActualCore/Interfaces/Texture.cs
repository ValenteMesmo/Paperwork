﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameCore
{
    public class TextureClass
    {
        public TextureClass(string Name, int X, int Y, int Width, int Height)
        {
            this.Color = Color.White;
            this.Name = Name;
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
        }

        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public string Name { get; }
        public Color Color { get; set; }
        public bool Flipped { get; set; }
        public int ZIndex { get; set; }
    }

    //public interface Texture : DimensionalThing
    //{
    //    //should i move x,y,w and h to other interface? dimentionalthing?
    //    int DrawableX { get; set; }
    //    int DrawableY { get; set; }
    //    int TextureOffSetX { get; }
    //    int TextureOffSetY { get; }
    //    int TextureWidth { get; }
    //    int TextureHeight { get; }
    //    string TextureName { get; }
    //    Color Color { get; }
    //}

    public interface Animation : DimensionalThing
    {
        IEnumerable<TextureClass> GetTextures();
    }

    public class AnimationFrame
    {
        public TextureClass[] Textures { get; set; }
        public int DurationInUpdatesCount { get; set; }

        public AnimationFrame(int DurationInUpdatesCount, params TextureClass[] Textures)
        {
            this.DurationInUpdatesCount = DurationInUpdatesCount;
            this.Textures = Textures;
        }
    }

    public class SimpleAnimation
    {
        private readonly AnimationFrame[] Frames;
        private int CurrentFrame;
        private int UpdatesUntilNextFrame;

        public SimpleAnimation(params AnimationFrame[] Frames)
        {
            UpdatesUntilNextFrame = Frames[CurrentFrame].DurationInUpdatesCount;
            this.Frames = Frames;
        }

        public void Update()
        {
            if (UpdatesUntilNextFrame > 0)
            {
                UpdatesUntilNextFrame--;
                return;
            }

            CurrentFrame++;
            if (CurrentFrame >= Frames.Length)
                CurrentFrame = 0;

            UpdatesUntilNextFrame = Frames[CurrentFrame].DurationInUpdatesCount;
        }

        public IEnumerable<TextureClass> GetTextures()
        {
            return Frames[CurrentFrame].Textures;
        }
    }
}
