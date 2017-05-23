﻿using GameCore;
using System;

namespace PaperWork.GameEntities.Player.Updates
{
    public class DragMidPaper : BaseDragHandler
    {
        private readonly Func<bool> GrabButtonPressed;
        private readonly Func<bool> PlayerHandsAreFree;
        private readonly Func<bool> GrabCooldownEnded;
        private readonly Func<bool> PlayerFacingRight;
        private readonly Func<Entity> GetRightEntity;
        private readonly Func<Entity> GetLeftEntity;

        public DragMidPaper(
            Action<PapersEntity> GivePaperToPlayer
            , Action SetGrabOnCooldown
            , Func<bool> GrabButtonPressed
            , Func<bool> PlayerHandsAreFree
            , Func<bool> GrabCooldownEnded
            , Func<bool> PlayerFacingRight
            , Func<Entity> GetRightEntity
            , Func<Entity> GetLeftEntity) : base(GivePaperToPlayer, SetGrabOnCooldown)
        {
            this.GrabButtonPressed = GrabButtonPressed;
            this.PlayerHandsAreFree = PlayerHandsAreFree;
            this.GrabCooldownEnded = GrabCooldownEnded;
            this.PlayerFacingRight = PlayerFacingRight;
            this.GetRightEntity = GetRightEntity;
            this.GetLeftEntity = GetLeftEntity;
        }

        protected override Entity GetEntityToGrab()
        {
            if (GrabButtonPressed()
                && PlayerHandsAreFree()
                && GrabCooldownEnded())
            {
                if (PlayerFacingRight())
                    return GetRightEntity();
                else
                    return GetLeftEntity();
            }

            return null;
        }
    }
}
