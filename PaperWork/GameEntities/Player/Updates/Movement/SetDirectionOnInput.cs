using GameCore;
using System;

namespace PaperWork
{
    class SetDirectionOnInput : IHandleUpdates
    {
        private readonly Func<bool> RightPressed;
        private readonly Func<bool> LeftPressed;
        private readonly Action<bool> SetFacingRight;

        public SetDirectionOnInput(
            Func<bool> RightPressed
            , Func<bool> LeftPressed
            , Action<bool> SetFacingRight)
        {
            this.RightPressed = RightPressed;
            this.LeftPressed = LeftPressed;
            this.SetFacingRight = SetFacingRight;
        }

        public void Update()
        {
            if (RightPressed())
                SetFacingRight(true);
            if (LeftPressed())
                SetFacingRight(false);
        }
    }
}

