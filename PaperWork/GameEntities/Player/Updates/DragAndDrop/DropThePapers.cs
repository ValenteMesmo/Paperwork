using System;
using GameCore;

namespace PaperWork.GameEntities.Collisions
{
    public class DropThePapers : IHandleUpdates
    {
        private readonly Func<PapersEntity> HoldingPapers;
        private readonly Func<bool> DropButtonPressed;
        private readonly Action SetDropOnCooldown;
        private readonly Func<bool> DropCooldownEnded;
        private readonly Action RemovePaperReferenceFromPlayer;
        private readonly Func<bool> MidPositionFree;
        private readonly Func<bool> DownButtonPressed;
        private readonly Func<bool> FacingOtherWay;
        private readonly int bonus;
        private readonly Action<float> SetHorizontalSpeed;
        private readonly Func<bool> TopPositionFree;

        public DropThePapers(
            Func<PapersEntity> HoldingPapers
            , Func<bool> DropButtonPressed
            , Func<bool> DropCooldownEnded
            , Action SetDropOnCooldown
            , Action RemovePaperReferenceFromPlayer
            , Func<bool> MidPositionFree
            , Func<bool> DownButtonPressed
            , Func<bool> FacingOtherWay
            , Action<float> SetHorizontalSpeed
            , Func<bool> TopPositionFree
            , int bonus
        )
        {
            this.HoldingPapers = HoldingPapers;
            this.DropButtonPressed = DropButtonPressed;
            this.DropCooldownEnded = DropCooldownEnded;
            this.SetDropOnCooldown = SetDropOnCooldown;
            this.RemovePaperReferenceFromPlayer = RemovePaperReferenceFromPlayer;
            this.MidPositionFree = MidPositionFree;
            this.DownButtonPressed = DownButtonPressed;
            this.FacingOtherWay = FacingOtherWay;
            this.bonus = bonus;
            this.SetHorizontalSpeed = SetHorizontalSpeed;
            this.TopPositionFree = TopPositionFree;
        }

        public void Update(Entity entity)
        {
            if (FacingOtherWay())
                return;

            if (DropButtonPressed()
                && DropCooldownEnded())
            {
                var papers = HoldingPapers();
                if (papers == null)
                    return;

                if (DownButtonPressed())
                    HandlePaperJump(papers, entity);
                else
                    HandlePaperDrop(papers);
            }
        }

        private void HandlePaperDrop(PapersEntity papers)
        {
            if (MidPositionFree())
            {
                foreach (var collider in papers.GetColliders())
                    collider.Disabled = false;
                papers.As<PapersEntity>().Target.SetDefaut();

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

        private void HandlePaperJump(PapersEntity papers, Entity player)
        {
            if (TopPositionFree() == false)
                return;

            foreach (var collider in papers.GetColliders())
                collider.Disabled = false;
            papers.As<PapersEntity>().Target.SetDefaut();

            var x = MathHelper.RoundUp(papers.Position.X, 50);
            if (x > 50 * 12)
                x = 50 * 12;
            var y = player.Position.Y;
            if (y < 50)
                y = 50;

            papers.Position = new Coordinate2D(x, y);
            player.Position = new Coordinate2D(x, y - 102);

            SetDropOnCooldown();
            SetHorizontalSpeed(0);
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
