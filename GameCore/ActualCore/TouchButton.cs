using GameCore.ActualCore;
using System;

namespace GameCore
{
    public class TouchButton :Touchable
    {
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
        }

        public void TouchBegin()
        {
            AndroidStuff.Vibrate( new long[] { 0, 15 });
            SetValue(true);
        }

        public void TouchContinue()
        {
            SetValue(true);
        }

        public void TouchEnded()
        {
            //todo: prevent player and paper from reaching known limits( stage borders)
            AndroidStuff.Vibrate(new long[] { 0, 20 });
            SetValue(false);
        }
    }
}
