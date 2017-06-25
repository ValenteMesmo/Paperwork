using GameCore.ActualCore;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameCore
{
    public class ButtonAnimation : Animation, IUpdateHandler, DimensionalThing
    {
        private SimpleAnimation buttonUp;
        private SimpleAnimation Current;
        private SimpleAnimation buttonDown;
        private readonly Func<bool> Pressed;

        public ButtonAnimation(int X, int Y, int Width, int Height, Func<bool> Pressed)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.Pressed = Pressed;

            var offsetX = (Width / 100) * 5;
            var offsetY = (Height / 100) * 5;

            var z = 0.8f;
            buttonUp = GeneratedContent.Create_touch_inputs_button(-offsetX, -offsetY, z, Width + offsetX, Height + offsetY);
            buttonUp.ChangeColor(new Color(253, 205, 1));
            buttonDown = GeneratedContent.Create_touch_inputs_button_pressed(0, 0, z, Width, Height);
            buttonDown.ChangeColor(new Color(253, 205, 1));

            Current = buttonUp;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }
        //TODO: remove
        public bool Ended => false;

        public void Update()
        {
            if (Pressed())
                Current = buttonDown;
            else
                Current = buttonUp;

            Current.Update();
        }

        public IEnumerable<Texture> GetTextures()
        {
            return Current.GetTextures();
        }
    }

    public class TouchArea : Touchable
    {
        private readonly Action<bool> SetValue;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        public TouchArea(int X, int Y, int Width, int Height, Action<bool> SetValue)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.SetValue = SetValue;
        }

        public void TouchBegin()
        {
            AndroidStuff.Vibrate(14);
            SetValue(true);
        }

        public void TouchContinue()
        {
            SetValue(true);
        }

        public void TouchEnded()
        {
            //AndroidStuff.Vibrate(20);
            SetValue(false);
        }
    }
}
