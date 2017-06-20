using System.Collections.Generic;
using GameCore;
using Microsoft.Xna.Framework;

namespace PaperWork
{
    public class PaperDestroyed : Animation, DimensionalThing
    {
        World World;
        private Texture Texture = new Texture("papers", 0, -Paper.SIZE, Paper.SIZE, Paper.SIZE * 2) { ZIndex = 0, Color = new Color(255,255,100,0.5f) };
        private int duration;


        public PaperDestroyed(World World)
        {
            this.World = World;
            duration = 100;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        public IEnumerable<Texture> GetTextures()
        {
            yield return Texture;
        }

        public void Update()
        {
            duration--;

            if (duration == 0)
            {
                World.Remove(this);
                return;
            }
        }
    }
}
