using GameCore;
using System;

namespace PaperWork.GameEntities.Player.Updates
{
    public class GrabNearPaperOnInput : IHandleEntityUpdates
    {
        private readonly Func<PapersEntity> GetNearPaper;
        private readonly Func<bool> GrabButtonPressed;
        private readonly Func<bool> GrabCooldownEnded;
        private readonly Func<bool> PlayerHandsAreFree;
        private readonly Action SetGrabOnCooldown;
        private readonly Action<PapersEntity> GivePaperToPlayer;
        private readonly Func<PapersEntity> GetAlternativeNearPaper;
        private readonly Func<float> GetVerticalSpeed;

        public GrabNearPaperOnInput(
            Func<PapersEntity> GetNearPaper
            , Func<bool> GrabButtonPressed
            , Func<bool> GrabCooldownEnded
            , Action SetGrabOnCooldown
            , Func<bool> PlayerHandsAreFree
            , Action<PapersEntity> GivePaperToPlayer
            , Func<PapersEntity> GetAlternativeNearPaper
            , Func<float> GetVerticalSpeed)
        {
            this.GetNearPaper = GetNearPaper;
            this.GrabButtonPressed = GrabButtonPressed;
            this.GrabCooldownEnded = GrabCooldownEnded;
            this.PlayerHandsAreFree = PlayerHandsAreFree;
            this.SetGrabOnCooldown = SetGrabOnCooldown;
            this.GivePaperToPlayer = GivePaperToPlayer;
            this.GetAlternativeNearPaper = GetAlternativeNearPaper;
            this.GetVerticalSpeed = GetVerticalSpeed;
        }

        public void Update(Entity entity)
        {
            if (GrabButtonPressed()
                && PlayerHandsAreFree()
                && GrabCooldownEnded())
            {
                var papers = GetNearPaper();
                if (papers == null)
                {
                    if (GetVerticalSpeed() != 0)
                        return;

                        papers = GetAlternativeNearPaper();
                    if (papers == null)
                        return;
                }

                GivePaperToPlayer(papers);
                SetGrabOnCooldown();
                papers.Target.Set(entity);

                foreach (var collider in papers.GetColliders())
                {
                    collider.Disabled = true;
                }
            }
        }
    }
}
