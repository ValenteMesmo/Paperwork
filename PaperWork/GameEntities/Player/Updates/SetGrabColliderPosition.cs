using GameCore;
using System;

namespace PaperWork
{
    class SetGrabColliderPosition : IHandleEntityUpdates
    {
        private readonly Func<bool> RightPressed;
        private readonly Func<bool> LeftPressed;
        private readonly Action<Coordinate2D> SetColliderPosition;

        public SetGrabColliderPosition(
            Func<bool> RightPressed
            , Func<bool> LeftPressed
            , Action<Coordinate2D> SetColliderPosition)
        {
            this.RightPressed = RightPressed;
            this.LeftPressed = LeftPressed;
            this.SetColliderPosition = SetColliderPosition;
        }

        public void Update(Entity entity)
        {
            if (RightPressed())
            {
                SetColliderPosition(new Coordinate2D(30, 20));
            }
            else if (LeftPressed())
            {
                SetColliderPosition(new Coordinate2D(-20, 20));
            }
        }
    }
}

