using System;
using GameCore;

namespace PaperWork.GameEntities.Collisions
{
    public class DropThePapers : IHandleEntityUpdates
    {
        private Func<bool> HoldingPapers;
        private Func<bool> DropButtonPressed;
        private readonly Action ReleasePapers;
        private readonly Action SetDropOnCooldown;
        private readonly Func<bool> DropCooldownEnded;
        private readonly Func<PapersEntity> GetCurrentPapers;

        public DropThePapers(
            Func<bool> HoldingPapers,
            Func<bool> DropButtonPressed,
            Func<bool> DropCooldownEnded,
            Action SetDropOnCooldown,
            Func<PapersEntity> GetCurrentPapers,
            Action ReleasePapers)
        {
            this.HoldingPapers = HoldingPapers;
            this.DropButtonPressed = DropButtonPressed;
            this.DropCooldownEnded = DropCooldownEnded;
            this.SetDropOnCooldown = SetDropOnCooldown;
            this.ReleasePapers = ReleasePapers;
            this.GetCurrentPapers = GetCurrentPapers;
        }

        public void Update(Entity entity)
        {
            if (DropButtonPressed()
                && HoldingPapers()
                && DropCooldownEnded())
            {
                SetDropOnCooldown();                
                GetCurrentPapers().DroppedBy(entity);
                ReleasePapers();
            }
        }
    }
}
