using System;
using System.Collections.Generic;
using GameCore;
using Microsoft.Xna.Framework;

namespace PaperWork
{
    public class PaperDestroyed : Animation, DimensionalThing
    {
        private World World;
        private int duration;
        private readonly SimpleAnimation Animation;

        public PaperDestroyed(World World, Color Color)
        {
            this.World = World;
            Animation = GeneratedContent.Create_trash_explosion_Explosion(
                - (int)(Paper.SIZE*0.8f)
                , -(int)(Paper.SIZE*1)
                , 0
                , (int)(Paper.SIZE*2.8f)
                , (int)(Paper.SIZE*2.8f));
            Animation.ChangeColor(Color);
            duration = 200;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        //TODO: remove
        public bool Ended => false;

        public IEnumerable<Texture> GetTextures()
        {
            return Animation.GetTextures();
        }

        public void Update()
        {
            Animation.Update();
            duration--;

            if (duration == 0)
            {
                World.Remove(this);
                return;
            }
        }
    }
}
