using GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperWork.GameEntities.Player.Updates
{
    public class ZeroVerticalSpeedIfGrounded : IHandleUpdates
    {
        private readonly Func<bool> Grounded;
        private readonly Action<float> SetVerticalSpeed;

        public ZeroVerticalSpeedIfGrounded(
            Func<bool> Grounded
            , Action<float> SetVerticalSpeed)
        {
            this.Grounded = Grounded;
            this.SetVerticalSpeed = SetVerticalSpeed;
        }

        public void Update(Entity entity)
        {
            if (Grounded())
                SetVerticalSpeed(0);
        }
    }
}
