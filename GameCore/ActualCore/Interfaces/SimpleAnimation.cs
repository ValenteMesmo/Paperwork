using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class SimpleAnimation : Animation
    {
        private readonly AnimationFrame[] Frames;
        private int CurrentFrame;
        private int UpdatesUntilNextFrame;

        public SimpleAnimation(params AnimationFrame[] Frames)
        {
            UpdatesUntilNextFrame = Frames[CurrentFrame].DurationInUpdatesCount;
            this.Frames = Frames;
        }

        public void Update()
        {
            if (UpdatesUntilNextFrame > 0)
            {
                UpdatesUntilNextFrame--;
                return;
            }

            CurrentFrame++;
            if (CurrentFrame >= Frames.Length)
                CurrentFrame = 0;

            UpdatesUntilNextFrame = Frames[CurrentFrame].DurationInUpdatesCount;
        }

        public IEnumerable<Texture> GetTextures()
        {
            try
            {
                return Frames[CurrentFrame].Textures;
            }
            catch 
            {
                return Enumerable.Empty<Texture>();
            }
        }

        public void Restart()
        {
            CurrentFrame = 0;
        }
    }
}
