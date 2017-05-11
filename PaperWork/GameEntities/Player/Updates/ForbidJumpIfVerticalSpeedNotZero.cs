using GameCore;
using System;

namespace PaperWork.PlayerHandlers.Updates
{
    public class ForbidJumpIfVerticalSpeedNotZero : IHandleEntityUpdates
    {
        private Action<bool> SetJumpEnabled;
        private Func<float> GetVerticalSpeed;

        public ForbidJumpIfVerticalSpeedNotZero(            
            Action<bool> SetJumpEnabled,
            Func<float> GetVerticalSpeed) 
        {
            this.SetJumpEnabled = SetJumpEnabled;
            this.GetVerticalSpeed = GetVerticalSpeed;
        }

        public void Update(Entity entity)
        {
            if (GetVerticalSpeed() != 0)
                SetJumpEnabled(false);
        }
    }
}
