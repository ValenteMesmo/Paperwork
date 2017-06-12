using System.Collections.Generic;

namespace GameCore
{
    public interface Animation //: DimensionalThing
    {
        IEnumerable<Texture> GetTextures();
        void Update();
    }
}
