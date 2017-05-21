using GameCore;
using System;

namespace PaperWork.PlayerHandlers.Updates
{
    class UsesSpeedToMove : IHandleUpdates
    {
        Func<float> GetHorizontalSpeed;
        Func<float> GetVerticalSpeed;
        public UsesSpeedToMove(
            Func<float> GetHorizontalSpeed
            , Func<float> GetVerticalSpeed)
        {
            this.GetHorizontalSpeed = GetHorizontalSpeed;
            this.GetVerticalSpeed = GetVerticalSpeed;
        }

        public void Update(Entity ParentEntity)
        {
            ParentEntity.Position = new Coordinate2D(
                ParentEntity.Position.X + GetHorizontalSpeed(),
                ParentEntity.Position.Y + GetVerticalSpeed()
            );
        }
    }
}
