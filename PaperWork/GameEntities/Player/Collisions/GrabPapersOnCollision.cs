using GameCore;
using GameCore.Collision;
using System;

namespace PaperWork.GameEntities.Player.Collisions
{
    class GrabPapersOnCollision : IHandleCollision
    {
        private readonly Func<bool> GrabButtonPressed;
        private readonly Func<bool> PlayerHandsAreFree;
        private readonly Func<bool> GrabCooldownEnded;
        private readonly Action SetGrabOnCooldown;
        private readonly Action<PapersEntity> GivePaperToPlayer;

        public GrabPapersOnCollision(
             Func<bool> GrabButtonPressed
            , Func<bool> PlayerHandsAreFree
            , Func<bool> GrabCooldownEnded
            , Action SetGrabOnCooldown
            , Action<PapersEntity> GivePaperToPlayer)
        {
            this.GrabButtonPressed = GrabButtonPressed;
            this.PlayerHandsAreFree = PlayerHandsAreFree;
            this.GrabCooldownEnded = GrabCooldownEnded;
            this.SetGrabOnCooldown = SetGrabOnCooldown;
            this.GivePaperToPlayer = GivePaperToPlayer;
        }

        public void CollisionFromAbove(BaseCollider collider, BaseCollider other)
        {
        }

        public void CollisionFromBelow(BaseCollider collider, BaseCollider other)
        {
        }

        public void CollisionFromTheLeft(BaseCollider collider, BaseCollider other)
        {
            if (other.ParentEntity is PapersEntity
             && GrabButtonPressed()
             && PlayerHandsAreFree()
             && GrabCooldownEnded())
            {
                var papers = other.ParentEntity as PapersEntity;
                GivePaperToPlayer(papers);
                SetGrabOnCooldown();
                papers.Grab(collider.ParentEntity);
            }
        }

        public void CollisionFromTheRight(BaseCollider collider, BaseCollider other)
        {
            if (other.ParentEntity is PapersEntity
                && GrabButtonPressed()
                && PlayerHandsAreFree()
                && GrabCooldownEnded())
            {
                var papers = other.ParentEntity as PapersEntity;
                GivePaperToPlayer(papers);
                SetGrabOnCooldown();
                papers.Grab(collider.ParentEntity);
            }
        }
    }
}
