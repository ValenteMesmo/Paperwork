using System;
using GameCore;

namespace PaperWork.GameEntities.Collisions
{
    public class DropThePapers : IHandleEntityUpdates
    {
        private readonly Func<Entity> HoldingPapers;
        private readonly Func<bool> DropButtonPressed;
        private readonly Action SetDropOnCooldown;
        private readonly Func<bool> DropCooldownEnded;
        private readonly Action RemovePaperReferenceFromPlayer;
        private readonly Func<bool> FacingRightDirection;
        private readonly Func<bool> NoNearEntity;
        private readonly Func<bool> NoAlternativeNearEntity;
        private readonly Func<bool> PlayerJumping;

        public DropThePapers(
            Func<Entity> HoldingPapers
            , Func<bool> DropButtonPressed
            , Func<bool> DropCooldownEnded
            , Action SetDropOnCooldown
            , Action RemovePaperReferenceFromPlayer
            , Func<bool> FacingRightDirection
            , Func<bool> NoNearEntity
            , Func<bool> NoAlternativeNearEntity
            , Func<bool> PlayerJumping
            )
        {
            this.HoldingPapers = HoldingPapers;
            this.DropButtonPressed = DropButtonPressed;
            this.DropCooldownEnded = DropCooldownEnded;
            this.SetDropOnCooldown = SetDropOnCooldown;
            this.RemovePaperReferenceFromPlayer = RemovePaperReferenceFromPlayer;
            this.FacingRightDirection = FacingRightDirection;
            this.NoNearEntity = NoNearEntity;
            this.NoAlternativeNearEntity = NoAlternativeNearEntity;
            this.PlayerJumping = PlayerJumping;
        }

        public void Update(Entity entity)
        {
            if (DropButtonPressed()
                && DropCooldownEnded()
                && NoNearEntity()
                && (NoAlternativeNearEntity() || PlayerJumping()))
            {
                var papers = HoldingPapers();                
                if (papers == null
                    || papers is PapersEntity == false)
                    return;

                SetDropOnCooldown();
                foreach (var collider in papers.GetColliders())
                    collider.Disabled = false;

                papers.As<PapersEntity>().Target.SetDefaut();

                var bonus = 0;
                if (FacingRightDirection())
                    bonus = 50;
                else
                    bonus = -40;

                var x = MathHelper.RoundUp(papers.Position.X + bonus, 50);
                if (x > 50 * 12)
                    x = 50 * 12;
                var y = papers.Position.Y + 25;
                if (y < 50)
                    y = 50;

                papers.Position = new Coordinate2D(x, y);

                RemovePaperReferenceFromPlayer();
            }
        }
    }

    static class MathHelper
    {
        public static int RoundUp(float numToRound, int multiple)
        {
            return (int)Math.Round(numToRound / multiple) * multiple;
        }

    }
}
