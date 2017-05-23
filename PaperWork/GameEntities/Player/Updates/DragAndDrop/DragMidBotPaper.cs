using GameCore;
using System;

namespace PaperWork.GameEntities.Player.Updates
{
    public class DragMidBotPaper : BaseDragHandler
    {
        private readonly Func<bool> GrabButtonPressed;
        private readonly Func<bool> PlayerHandsAreFree;
        private readonly Func<bool> GrabCooldownEnded;
        private readonly Func<bool> PlayerGrounded;
        private readonly Func<bool> FacingRight;
        private readonly Func<Entity> GetBotRight;
        private readonly Func<Entity> GetBotLeft;

        public DragMidBotPaper(
            Action<PapersEntity> GivePaperToPlayer
            , Action SetGrabOnCooldown
            , Func<bool> GrabButtonPressed
            , Func<bool> PlayerHandsAreFree
            , Func<bool> GrabCooldownEnded
            , Func<bool> PlayerGrounded
            , Func<bool> FacingRight
            , Func<Entity> GetBotRight
            , Func<Entity> GetBotLeft) : base(GivePaperToPlayer, SetGrabOnCooldown)
        {
            this.GrabButtonPressed = GrabButtonPressed;
            this.PlayerHandsAreFree = PlayerHandsAreFree;
            this.GrabCooldownEnded = GrabCooldownEnded;
            this.PlayerGrounded = PlayerGrounded;
            this.FacingRight = FacingRight;
            this.GetBotRight = GetBotRight;
            this.GetBotLeft = GetBotLeft;
        }

        protected override Entity GetEntityToGrab()
        {
            if (GrabButtonPressed()
                && PlayerHandsAreFree()
                && GrabCooldownEnded()
                && PlayerGrounded())
            {
                if (FacingRight())
                    return GetBotRight();
                else
                    return GetBotLeft();
            }

            return null;
        }
    }
}
