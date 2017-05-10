using GameCore;
using System;

namespace PaperWork
{
    public class JumpOnInputDecreasesVerticalSpeed : UpdateHandler
    {
        private InputRepository Inputs;
        private Func<bool> CanJump;
        Action<float> SetVerticalSpeed;

        public JumpOnInputDecreasesVerticalSpeed(
            Entity ParentEntity,
            InputRepository Inputs,
            Func<bool> CanJump,
            Action<float> SetVerticalSpeed) : base(ParentEntity)
        {
            this.CanJump = CanJump;
            this.Inputs = Inputs;
            this.SetVerticalSpeed = SetVerticalSpeed;
        }

        public override void Update()
        {
            if (
                (
                    Inputs.Jump.GetStatus() == ButtomStatus.Click
                    || Inputs.Jump.GetStatus() == ButtomStatus.Hold
                )
                && CanJump())
            {
                SetVerticalSpeed(-4);
            }
        }
    }
}
