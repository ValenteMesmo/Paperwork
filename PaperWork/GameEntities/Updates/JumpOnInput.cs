using GameCore;
using System;

namespace PaperWork
{
    public class JumpOnInput : UpdateHandler
    {
        private InputRepository Inputs;
        private Func<bool> CanJump;

        public JumpOnInput(
            Entity ParentEntity,
            InputRepository Inputs,
            Func<bool> CanJump) : base(ParentEntity)
        {
            this.CanJump = CanJump;
            this.Inputs = Inputs;
        }

        public override void Update()
        {
            if (
                (
                    Inputs.Jump.GetStatus() == ButtomStatus.Click
                    || Inputs.Jump.GetStatus() == ButtomStatus.Hold
                ) && CanJump())
            {
                ParentEntity.Speed = new Coordinate2D(ParentEntity.Speed.X, -2);
            }
        }
    }
}
