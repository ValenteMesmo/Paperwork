using GameCore;
using System;

namespace PaperWork.GameEntities.Papers
{
    public class FrictionSpeedLoss : IHandleEntityUpdates
    {
        private readonly Action<float> SetHorizontalSpeed;
        private readonly Func<float> GetHorizontalSpeed;

        public FrictionSpeedLoss(
            Action<float> SetHorizontalSpeed
            , Func<float> GetHorizontalSpeed)
        {
            this.SetHorizontalSpeed = SetHorizontalSpeed;
            this.GetHorizontalSpeed = GetHorizontalSpeed;
        }

        public void Update(Entity entity)
        {
            if (GetHorizontalSpeed() > 0)
            {
                SetHorizontalSpeed(GetHorizontalSpeed() - 0.002f);
            }
            else if (GetHorizontalSpeed() < 0)
            {
                SetHorizontalSpeed(GetHorizontalSpeed() + 0.002f);
            }
        }
    }
}
