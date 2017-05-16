using System;
using GameCore;

namespace PaperWork.GameEntities.Collisions
{
    public class DropThePapers : IHandleEntityUpdates
    {
        private readonly Func<bool> HoldingPapers;
        private readonly Func<bool> DropButtonPressed;
        private readonly Action ReleasePapers;
        private readonly Action SetDropOnCooldown;
        private readonly Func<bool> DropCooldownEnded;
        private readonly Action TellThePaperThatHeWasDropped;

        public DropThePapers(
            Func<bool> HoldingPapers,
            Func<bool> DropButtonPressed,
            Func<bool> DropCooldownEnded,
            Action SetDropOnCooldown,
            Action ReleasePapers,
            Action TellThePaperThatHeWasDropped)
        {
            this.HoldingPapers = HoldingPapers;
            this.DropButtonPressed = DropButtonPressed;
            this.DropCooldownEnded = DropCooldownEnded;
            this.SetDropOnCooldown = SetDropOnCooldown;
            this.ReleasePapers = ReleasePapers;
            this.TellThePaperThatHeWasDropped = TellThePaperThatHeWasDropped;
        }

        public void Update(Entity entity)
        {
            if (DropButtonPressed()
                && HoldingPapers()
                && DropCooldownEnded())
            {
                SetDropOnCooldown();                
                TellThePaperThatHeWasDropped();
                ReleasePapers();
            }
        }
    }
}
