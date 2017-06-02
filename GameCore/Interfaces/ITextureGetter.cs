using Microsoft.Xna.Framework;

namespace GameCore
{
    public interface ITexture
    {
        float TextureOffSetX { get; set; }
        float TextureOffSetY { get; set; }
        int TextureWidth { get; set; }
        int TextureHeight { get; set; }
        string TextureName { get; set; }
        Color TextureColor { get; set; }
    }
}
