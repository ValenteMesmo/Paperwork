using GameCore;
using System;

namespace PaperWork
{
    class SetGrabColliderPosition : IHandleEntityUpdates
    {
        private readonly Func<bool> RightPressed;
        private readonly Func<bool> LeftPressed;
        private readonly Action<Coordinate2D> SetColliderPosition;
        private readonly Func<Coordinate2D> GetColliderPosition;

        public SetGrabColliderPosition(
            Func<bool> RightPressed
            , Func<bool> LeftPressed
            , Action<Coordinate2D> SetColliderPosition
            , Func<Coordinate2D> GetColliderPosition)
        {
            this.RightPressed = RightPressed;
            this.LeftPressed = LeftPressed;
            this.SetColliderPosition = SetColliderPosition;
            this.GetColliderPosition = GetColliderPosition;
        }

        public void Update(Entity entity)
        {
            if (RightPressed())
            {
                SetColliderPosition(new Coordinate2D(30, GetColliderPosition().Y));
            }
            else if (LeftPressed())
            {
                SetColliderPosition(new Coordinate2D(-20, GetColliderPosition().Y));
            }
        }
    }
}

