using GameCore;
using System;

namespace PaperWork.GameEntities.Player.Updates
{
    class GrabsObjectThatPlayerIsFacing : IHandleEntityUpdates
    {
        private readonly Func<bool> PlayerIsNotHoldingPapers;
        private readonly Action<PapersEntity> GivePaperToPlayer;
        private readonly Func<bool> GrabButtonPressed;
        private readonly Func<bool> GrabCooldownEnded;
        private readonly Func<Coordinate2D, Entity> EntityThatThePlayerIsTryingToGrab;
        private readonly Action PutGrabActionOnCooldown;

        public GrabsObjectThatPlayerIsFacing(
             Func<bool> GrabButtonPressed
            , Func<bool> PlayerIsNotHoldingPapers
            , Action<PapersEntity> GivePaperToPlayer
            , Func<bool> GrabCooldownEnded
            , Func<Coordinate2D, Entity> EntityThatThePlayerIsTryingToGrab
            , Action PutGrabActionOnCooldown)
        {
            this.GrabButtonPressed = GrabButtonPressed;
            this.PlayerIsNotHoldingPapers = PlayerIsNotHoldingPapers;
            this.GivePaperToPlayer = GivePaperToPlayer;
            this.GrabCooldownEnded = GrabCooldownEnded;
            this.EntityThatThePlayerIsTryingToGrab = EntityThatThePlayerIsTryingToGrab;
            this.PutGrabActionOnCooldown = PutGrabActionOnCooldown;
        }

        public void Update(Entity ParentEntity)
        {
            if (PlayerIsNotHoldingPapers()
                && GrabButtonPressed()
                && GrabCooldownEnded())
            {
                var paper = EntityThatThePlayerIsTryingToGrab(ParentEntity.Position);
                if (paper == null && !(paper is PapersEntity))
                    return;

                PutGrabActionOnCooldown();

                paper.As<PapersEntity>().GrabbedBy(ParentEntity);
                GivePaperToPlayer(paper.As<PapersEntity>());
            }
        }
    }
}
