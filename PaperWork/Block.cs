using GameCore;
using Microsoft.Xna.Framework;

namespace PaperWork
{
    public class Block :
        ICollider
        , ITexture
    {
        public Block()
        {
            Width = 50;
            Height = 50;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float HorizontalSpeed { get; set; }
        public float VerticalSpeed { get; set; }

        public float TextureOffSetX { get => -World.SPACE_BETWEEN_THINGS; }
        public float TextureOffSetY { get => -World.SPACE_BETWEEN_THINGS; }
        public int TextureWidth { get => (int)(Width + World.SPACE_BETWEEN_THINGS); }
        public int TextureHeight { get => (int)(Height + World.SPACE_BETWEEN_THINGS); }
        public string TextureName { get => "block"; }
        public Color TextureColor { get => Color.White; }
    }
}