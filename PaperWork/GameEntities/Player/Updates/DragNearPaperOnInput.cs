using GameCore;
using System;

namespace PaperWork.GameEntities.Player.Updates
{
    public class DragNearPaperOnInput : IHandleEntityUpdates
    {
        private readonly Func<Entity> GetNearPaper;
        private readonly Func<bool> GrabButtonPressed;
        private readonly Func<bool> GrabCooldownEnded;
        private readonly Func<bool> PlayerHandsAreFree;
        private readonly Action SetGrabOnCooldown;
        private readonly Action<Entity> GivePaperToPlayer;
        private readonly Func<Entity> GetAlternativeNearPaper;
        private readonly Func<float> GetVerticalSpeed;

        public DragNearPaperOnInput(
            Func<Entity> GetNearPaper
            , Func<bool> GrabButtonPressed
            , Func<bool> GrabCooldownEnded
            , Action SetGrabOnCooldown
            , Func<bool> PlayerHandsAreFree
            , Action<Entity> GivePaperToPlayer
            , Func<Entity> GetAlternativeNearPaper
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

                if (papers is PapersEntity == false)
                    return;                

                GivePaperToPlayer(papers);
                SetGrabOnCooldown();
                papers.As<PapersEntity>().Target.Set(entity);

                foreach (var collider in papers.GetColliders())
                {
                    collider.Disabled = true;
                }
            }
        }
    }
}
