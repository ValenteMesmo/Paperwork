using GameCore.ActualCore;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameCore
{
    public class TouchButton : Touchable, Animation
    {
        private readonly Action<bool> SetValue;

        private SimpleAnimation buttonUp;
        private SimpleAnimation Current;
        private SimpleAnimation buttonDown;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        public TouchButton(int X, int Y, int Width, int Height, Action<bool> SetValue)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.SetValue = SetValue;

            var offsetX = (Width / 100) * 5;
            var offsetY = (Height / 100) * 5;

            buttonUp = GeneratedContent.Create_touch_inputs_button(-offsetX, -offsetY, 0.9f, Width + offsetX, Height + offsetY);
            buttonUp.ChangeColor(new Color(253, 205, 1));
            buttonDown = GeneratedContent.Create_touch_inputs_button_pressed(0, 0, 0.9f, Width, Height);
            buttonDown.ChangeColor(new Color(253, 205, 1));

            Current = buttonUp;
        }

        public void TouchBegin()
        {
            AndroidStuff.Vibrate(10);
            SetValue(true);
            Current = buttonDown;
        }

        public void TouchContinue()
        {
            SetValue(true);
            Current = buttonDown;
        }

        public void TouchEnded()
        {
            //todo: prevent player and paper from reaching known limits( stage borders)
            AndroidStuff.Vibrate(20);
            SetValue(false);
            Current = buttonUp;
        }

        public IEnumerable<Texture> GetTextures()
        {
            return Current.GetTextures();
        }

        public void Update()
        {
        }
    }
}
