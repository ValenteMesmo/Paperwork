using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class SimpleAnimation : Animation
    {
        private readonly AnimationFrame[] Frames;
        private int CurrentFrameIndex;
        private int UpdatesUntilNextFrame;
        private Texture[] CurrentTextures;

        public SimpleAnimation(params AnimationFrame[] Frames)
        {
            UpdatesUntilNextFrame = Frames[CurrentFrameIndex].DurationInUpdatesCount;
            this.Frames = Frames;
            CurrentTextures = Frames[CurrentFrameIndex].Textures;
        }

        public void ChangeColor(Color Color)
        {
            foreach (var texture in Frames.SelectMany(f => f.Textures))
            {
                texture.Color = Color;
            }
        }

        [Obsolete("Gambiarra")]
        public Color GetColor()
        {
            return Frames[0].Textures[0].Color;
        }

        public void Update()
        {
            if (UpdatesUntilNextFrame > 0)
            {
                UpdatesUntilNextFrame--;
                return;
            }

            CurrentFrameIndex++;
            if (CurrentFrameIndex >= Frames.Length)
                CurrentFrameIndex = 0;

            UpdatesUntilNextFrame = Frames[CurrentFrameIndex].DurationInUpdatesCount;
            CurrentTextures = Frames[CurrentFrameIndex].Textures;
        }

        public IEnumerable<Texture> GetTextures()
        {
            return CurrentTextures;
        }

        public void Restart()
        {
            CurrentFrameIndex = 0;
        }
    }
}
