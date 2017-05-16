using System;

namespace PaperWork.GameEntities.Collisions
{
    public class Cooldown
    {
        private readonly int milliseconds;
        private DateTime lastSet;

        public Cooldown(int milliseconds)
        {
            this.milliseconds = milliseconds;
        }

        public void TriggerCooldown()
        {
            lastSet = DateTime.Now.AddMilliseconds(milliseconds);
        }

        public bool OnCooldown()
        {
            return DateTime.Now < lastSet;
        }

        public bool CooldownEnded()
        {
            return OnCooldown() == false;
        }
    }
}
