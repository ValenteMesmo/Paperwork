using System;
using GameCore;

namespace PaperWork.GameEntities.Collisions
{
    public class DropThePapers : IHandleUpdates
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
        private readonly Func<bool> DownButtonPressed;

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
            , Func<bool> DownButtonPressed
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
            this.DownButtonPressed = DownButtonPressed;
        }

        public void Update(Entity entity)
        {
            if (DropButtonPressed()
                && DropCooldownEnded())
            {
                var papers = HoldingPapers();
                if (papers == null
                    || papers is PapersEntity == false)
                    return;

                if (DownButtonPressed())
                    HandlePaperJump(papers as PapersEntity, entity);
                else
                    HandlePaperDrop(papers as PapersEntity);
            }
        }

        private void HandlePaperDrop(PapersEntity papers)
        {
            if (NoNearEntity()
               && (NoAlternativeNearEntity() || PlayerJumping()))
            {

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

                SetDropOnCooldown();

                RemovePaperReferenceFromPlayer();
            }
        }

        private void HandlePaperJump(PapersEntity papers , Entity player)
        {
            foreach (var collider in papers.GetColliders())
                collider.Disabled = false;
            papers.As<PapersEntity>().Target.SetDefaut();

            var x = MathHelper.RoundUp(papers.Position.X , 50);
            if (x > 50 * 12)
                x = 50 * 12;
            var y = player.Position.Y;
            if (y < 50)
                y = 50;

            papers.Position = new Coordinate2D(x, y);
            player.Position = new Coordinate2D(x, y -102);

            SetDropOnCooldown();

            RemovePaperReferenceFromPlayer();
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
