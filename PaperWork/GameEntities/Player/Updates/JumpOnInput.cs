using GameCore;
using System;

namespace PaperWork
{
    public class JumpOnInputDecreasesVerticalSpeed : IHandleUpdates
    {
        private readonly Func<bool> StepingOnTheFloor;
        private readonly Action<float> SetVerticalSpeed;
        private readonly Func<bool> JumpButtonPressed;

        public JumpOnInputDecreasesVerticalSpeed(            
            Func<bool> StepingOnTheFloor,
            Action<float> SetVerticalSpeed,
            Func<bool> JumpButtonPressed)
        {
            this.StepingOnTheFloor = StepingOnTheFloor;
            this.SetVerticalSpeed = SetVerticalSpeed;
            this.JumpButtonPressed = JumpButtonPressed;
        }

        public void Update(Entity ParentEntity)
        {
            if (JumpButtonPressed() && StepingOnTheFloor())
            {
                SetVerticalSpeed(-10);
            }
        }
    }
}
