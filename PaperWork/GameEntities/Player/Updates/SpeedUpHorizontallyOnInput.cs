using GameCore;
using System;

namespace PaperWork
{
    public class SpeedUpHorizontallyOnInput : IHandleEntityUpdates
    {
        private readonly Action<float> SetHorizontalSpeed;
        private readonly Func<bool> LeftButtonPressed;
        private readonly Func<bool> RightButtonPressed;

        public SpeedUpHorizontallyOnInput(
            Action<float> SetHorizontalSpeed,
            Func<bool> LeftButtonPressed,
            Func<bool> RightButtonPressed)
        {
            this.SetHorizontalSpeed = SetHorizontalSpeed;
            this.LeftButtonPressed = LeftButtonPressed;
            this.RightButtonPressed = RightButtonPressed;
        }

        public void Update(Entity ParentEntity)
        {
            if (RightButtonPressed())
            {
                SetHorizontalSpeed(1);
            }
            else if (LeftButtonPressed())
            {
                SetHorizontalSpeed(-1);
            }
            else
            {
                SetHorizontalSpeed(0);
            }
        }
    }
}
