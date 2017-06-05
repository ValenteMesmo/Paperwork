using Microsoft.Xna.Framework;

namespace GameCore
{
    public interface ITexture
    {
        int TextureOffSetX { get; }
        int TextureOffSetY { get; }
        int TextureWidth { get; }
        int TextureHeight { get; }
        string TextureName { get; }
        Color TextureColor { get; }
    }
}
