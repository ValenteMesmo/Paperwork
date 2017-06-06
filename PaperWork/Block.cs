using GameCore;
using Microsoft.Xna.Framework;

namespace PaperWork
{
    public class Block :
        Collider
        , Texture
        , IPlayerMovementBlocker
    {
        public Block()
        {
            Width = 100;
            Height = 100;
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

        public int TextureOffSetX { get => -World.SPACE_BETWEEN_THINGS; }
        public int TextureOffSetY { get => -World.SPACE_BETWEEN_THINGS; }
        public int TextureWidth { get => (int)(Width + World.SPACE_BETWEEN_THINGS); }
        public int TextureHeight { get => (int)(Height + World.SPACE_BETWEEN_THINGS); }
        public string TextureName { get => "block"; }
        public Color Color { get => Color.White; }
    }
}