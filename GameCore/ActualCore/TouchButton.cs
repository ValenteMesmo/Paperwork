using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameCore
{
    public class TouchButton :
        Animation
        , Touchable
    {
        Texture Texture;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        public TouchButton(int X,int Y, int Width, int Height)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;

            Texture = new Texture("block", 0, 0, Width, Height) { Color = Color.BlueViolet, ZIndex=0 };
        }

        public IEnumerable<Texture> GetTextures()
        {
            yield return Texture;
        }

        public void TouchBegin()
        {
            Debug.WriteLine("TouchBegin");
        }

        public void TouchContinue()
        {
            Debug.WriteLine("TouchContinue");
        }

        public void TouchEnded()
        {
            Debug.WriteLine("TouchEnded");
        }

        public void Update()
        {

        }
    }
}
