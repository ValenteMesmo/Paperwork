using System.Collections.Generic;

namespace GameCore
{
    public interface Animation : IUpdateHandler//Thing
    {
        IEnumerable<Texture> GetTextures();
        bool Ended { get; }
    }
}
