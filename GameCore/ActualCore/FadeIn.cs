using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameCore
{
    public class FadeIn : Animation
    {
        Texture Texture;
        World World;
        public FadeIn(World World)
        {
            this.World = World;
            Texture = new Texture("pixel", 0, 0, 14000, 14000) { ZIndex = 0, Color = Color.Black};
        }

        public bool Ended => true;

        public IEnumerable<Texture> GetTextures()
        {
            yield return Texture;
        }

        float fadeSpeed = -0.01f;
        float fadeValue = 1;

        public void Update()
        {
            fadeValue += fadeSpeed;

            if (fadeValue < 0)
            {
                fadeSpeed = 0;
                fadeValue = 0;
                World.Remove(this);
            }

            Texture.Color = new Color(Texture.Color.R, Texture.Color.G, Texture.Color.B, fadeValue);
        }
    }
}
