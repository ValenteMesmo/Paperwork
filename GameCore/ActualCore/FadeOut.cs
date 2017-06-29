using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameCore
{
    public class FadeOut : Animation, SomethingThatHandleUpdates
    {
        Texture Texture;
        Action OnFadeComplete;
        public FadeOut(Action OnFadeComplete)
        {
            this.OnFadeComplete = OnFadeComplete;
            Texture = new Texture("pixel", 0, 0, 14000, 14000) { ZIndex = 0, Color = Color.Black };
        }

        public IEnumerable<Texture> GetTextures()
        {
            yield return Texture;
        }

        float fadeSpeed = 0.01f;
        float fadeValue = 0;

        public void Update()
        {
            fadeValue += fadeSpeed;
            if (fadeValue > 1)
            {
                fadeSpeed = 0;
                fadeValue = 1;
                OnFadeComplete();
            }

            Texture.Color = new Color(
                Texture.Color.R,
                Texture.Color.G,
                Texture.Color.B,
                fadeValue);
        }
    }
}
