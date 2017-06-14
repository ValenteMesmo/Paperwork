using System.Collections.Generic;

namespace GameCore
{
    public interface Animation : Thing
    {
        IEnumerable<Texture> GetTextures();
        void Update();
    }
}
