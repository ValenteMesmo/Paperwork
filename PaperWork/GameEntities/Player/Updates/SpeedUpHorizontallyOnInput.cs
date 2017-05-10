using GameCore;
using System;

namespace PaperWork
{
    public class SpeedUpHorizontallyOnInput : UpdateHandler
    {
        private InputRepository Inputs;
        Action<float> SetHorizontalSpeed;

        public SpeedUpHorizontallyOnInput(
            Entity ParentEntity,
            InputRepository Inputs,
            Action<float> SetHorizontalSpeed) : base(ParentEntity)
        {
            this.SetHorizontalSpeed = SetHorizontalSpeed;
            this.Inputs = Inputs;
        }

        public override void Update()
        {
            if (Inputs.Right.GetStatus() == ButtomStatus.Click
                || Inputs.Right.GetStatus() == ButtomStatus.Hold)
            {
                SetHorizontalSpeed(1);
            }
            else if (Inputs.Left.GetStatus() == ButtomStatus.Click
                || Inputs.Left.GetStatus() == ButtomStatus.Hold)
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
