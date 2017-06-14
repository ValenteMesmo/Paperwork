using GameCore.ActualCore;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameCore
{
    public class TouchButton :
        Animation
        , Touchable
    {
        Texture Texture;
        private readonly Action<bool> SetValue;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        public TouchButton(int X,int Y, int Width, int Height, Action<bool> SetValue)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.SetValue = SetValue;
            Texture = new Texture("block", 0, 0, Width, Height) { Color = Color.BlueViolet, ZIndex=0 };
        }

        public IEnumerable<Texture> GetTextures()
        {
            yield return Texture;
        }

        public void TouchBegin()
        {
            AndroidStuff.Vibrate( new long[] { 0, 20 });
            SetValue(true);
        }

        public void TouchContinue()
        {
            SetValue(true);
        }

        public void TouchEnded()
        {
            AndroidStuff.Vibrate(new long[] { 0, 10, 20, 10 });
            SetValue(false);
        }

        public void Update()
        {

        }
    }
}
