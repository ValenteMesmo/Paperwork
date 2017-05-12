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
        private readonly Func<Coordinate2D, bool> PlaceToDropIsEmpty;

        public DropThePapers(
            Func<bool> HoldingPapers,
            Func<bool> DropButtonPressed,
            Func<bool> DropCooldownEnded,
            Action SetDropOnCooldown,
            Action ReleasePapers,
            Action TellThePaperThatHeWasDropped,
            Func<Coordinate2D, bool> PlaceToDropIsEmpty)
        {
            this.HoldingPapers = HoldingPapers;
            this.DropButtonPressed = DropButtonPressed;
            this.DropCooldownEnded = DropCooldownEnded;
            this.SetDropOnCooldown = SetDropOnCooldown;
            this.ReleasePapers = ReleasePapers;
            this.TellThePaperThatHeWasDropped = TellThePaperThatHeWasDropped;
            this.PlaceToDropIsEmpty = PlaceToDropIsEmpty;
        }

        public void Update(Entity entity)
        {
            if (DropButtonPressed()
                && HoldingPapers()
                && DropCooldownEnded()
                && PlaceToDropIsEmpty(new Coordinate2D(entity.Position.X + 50, entity.Position.Y + 50)))
            {
                SetDropOnCooldown();
                TellThePaperThatHeWasDropped();
                ReleasePapers();
            }
        }
    }
}
