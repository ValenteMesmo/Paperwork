using Microsoft.Xna.Framework;

namespace GameCore
{
    public interface Texture : DimensionalThing
    {
        //should i move x,y,w and h to other interface? dimentionalthing?
        int TextureOffSetX { get; }
        int TextureOffSetY { get; }
        int TextureWidth { get; }
        int TextureHeight { get; }
        string TextureName { get; }
        Color Color { get; }
    }
}
