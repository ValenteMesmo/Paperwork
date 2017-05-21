using GameCore;
using System;

namespace PaperWork.PlayerHandlers.Updates
{
    public class ForbidJumpIfVerticalSpeedNotZero : IHandleUpdates
    {
        private readonly Action<bool> SetJumpEnabled;
        private readonly Func<bool> PlayerGrounded;

        public ForbidJumpIfVerticalSpeedNotZero(            
            Action<bool> SetJumpEnabled
            ,Func<bool> PlayerGrounded
            ) 
        {
            this.PlayerGrounded = PlayerGrounded;
            this.SetJumpEnabled = SetJumpEnabled;
        }

        public void Update(Entity entity)
        {
            if (PlayerGrounded())
                SetJumpEnabled(false);
        }
    }
}
