using System.Collections.Generic;

namespace GameCore
{
    public interface SoundPlayer
    {
        void PlaySound(string soundName);
    }

    public interface Animation : IUpdateHandler//Thing
    {
        IEnumerable<Texture> GetTextures();
        bool Ended { get; }
    }
}
