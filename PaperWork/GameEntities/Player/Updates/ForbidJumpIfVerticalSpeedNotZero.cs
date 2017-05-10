using GameCore;
using System;

namespace PaperWork.PlayerHandlers.Updates
{
    public class ForbidJumpIfVerticalSpeedNotZero : UpdateHandler
    {
        private Action<bool> SetJumpEnabled;
        private Func<float> GetVerticalSpeed;

        public ForbidJumpIfVerticalSpeedNotZero(
            Entity ParentEntity, 
            Action<bool> SetJumpEnabled,
            Func<float> GetVerticalSpeed) : base(ParentEntity)
        {
            this.SetJumpEnabled = SetJumpEnabled;
            this.GetVerticalSpeed = GetVerticalSpeed;
        }

        public override void Update()
        {
            if (GetVerticalSpeed() != 0)
                SetJumpEnabled(false);
        }
    }
}
