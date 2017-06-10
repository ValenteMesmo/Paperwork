using System;
using System.Collections.Generic;
using GameCore;
using Microsoft.Xna.Framework;

namespace PaperWork
{
    public class Block :
        Collider
        , Animation
        , IPlayerMovementBlocker
    {
        const int SIZE = 100;

        public Block()
        {
            Width = SIZE;
            Height = SIZE;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }
        public bool Disabled { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }
        private IEnumerable<Texture> Texture = new Texture[]{
        new Texture(
            "block"
            , -World.SPACE_BETWEEN_THINGS
            , -World.SPACE_BETWEEN_THINGS
            , SIZE + World.SPACE_BETWEEN_THINGS
            , SIZE + World.SPACE_BETWEEN_THINGS
            )
        };


        public IEnumerable<Texture> GetTextures()
        {
            return Texture;
        }

        //public int TextureOffSetX { get => -World.SPACE_BETWEEN_THINGS; }
        //public int TextureOffSetY { get => -World.SPACE_BETWEEN_THINGS; }
        //public int TextureWidth { get => (int)(Width + World.SPACE_BETWEEN_THINGS); }
        //public int TextureHeight { get => (int)(Height + World.SPACE_BETWEEN_THINGS); }
        //public string TextureName { get => "block"; }
        //public Color Color { get => Color.White; }
    }
}