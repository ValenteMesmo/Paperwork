using Microsoft.Xna.Framework;

namespace GameCore
{
    public interface ITexture
    {
        float TextureOffSetX { get; }
        float TextureOffSetY { get; }
        int TextureWidth { get; }
        int TextureHeight { get; }
        string TextureName { get; }
        Color TextureColor { get; }
    }
}
