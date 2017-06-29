using System.Collections.Generic;

namespace GameCore
{
    public interface Animation : Something
    {
        IEnumerable<Texture> GetTextures();
    }
}
