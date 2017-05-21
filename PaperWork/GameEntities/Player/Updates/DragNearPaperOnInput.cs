using GameCore;
using System;

namespace PaperWork.GameEntities.Player.Updates
{
    public class DragNearPaperOnInput : IHandleUpdates
    {
        private readonly Func<bool> GrabButtonPressed;
        private readonly Func<bool> PlayerHandsAreFree;
        private readonly Func<bool> GrabCooldownEnded;
        private readonly Func<bool> DownButtonPressed;
        private readonly Func<bool> PlayerFacingRight;
        private readonly Func<Entity> GetRightEntity;
        private readonly Func<float> GetVerticalSpeed;
        private readonly Func<Entity> GetBotRightEntity;
        private readonly Func<Entity> GetLeftEntity;
        private readonly Func<Entity> GetBotLeftEntity;
        private readonly Action<PapersEntity> GivePaperToPlayer;
        private readonly Action SetGrabOnCooldown;
        private readonly Func<Entity> GetBotEntity;

        public DragNearPaperOnInput(
            Func<bool> GrabButtonPressed
            , Func<bool> PlayerHandsAreFree
            , Func<bool> GrabCooldownEnded
            , Func<bool> DownButtonPressed
            , Func<bool> PlayerFacingRight
            , Func<Entity> GetRightEntity
            , Func<float> GetVerticalSpeed
            , Func<Entity> GetBotRightEntity
            , Func<Entity> GetLeftEntity
            , Func<Entity> GetBotLeftEntity
            , Action<PapersEntity> GivePaperToPlayer
            , Action SetGrabOnCooldown
            , Func<Entity> GetBotEntity)
        {
            this.GrabButtonPressed = GrabButtonPressed;
            this.PlayerHandsAreFree = PlayerHandsAreFree;
            this.GrabCooldownEnded = GrabCooldownEnded;
            this.DownButtonPressed = DownButtonPressed;
            this.PlayerFacingRight = PlayerFacingRight;
            this.GetRightEntity = GetRightEntity;
            this.GetVerticalSpeed = GetVerticalSpeed;
            this.GetBotRightEntity = GetBotRightEntity;
            this.GetLeftEntity = GetLeftEntity;
            this.GetBotLeftEntity = GetBotLeftEntity;
            this.GivePaperToPlayer = GivePaperToPlayer;
            this.SetGrabOnCooldown = SetGrabOnCooldown;
            this.GetBotEntity = GetBotEntity;
        }

        public void Update(Entity entity)
        {
            if (GrabButtonPressed()
                && PlayerHandsAreFree()
                && GrabCooldownEnded())
            {
                if (DownButtonPressed())
                {
                    HandleDragFromBelow(entity);
                }
                else
                {
                    HandleDragNear(entity);
                }
            }
        }

        private PapersEntity GetPaperToGrab()
        {
            Entity result = null;
            if (PlayerFacingRight())
            {
                result = GetRightEntity();

                if (result == null || result is PapersEntity == false)
                {
                    if (GetVerticalSpeed() != 0)
                        return null;

                    result = GetBotRightEntity();
                }
            }
            else
            {
                result = GetLeftEntity();
                if (result == null || result is PapersEntity == false)
                {
                    if (GetVerticalSpeed() != 0)
                        return null;

                    result = GetBotLeftEntity();
                }
            }

            if (result is PapersEntity)
                return result as PapersEntity;

            return null;
        }

        private void HandleDragNear(Entity entity)
        {
            var papers = GetPaperToGrab();

            if (papers == null)
            {
                return;
            }

            GivePaperToPlayer(papers);
            SetGrabOnCooldown();
            papers.As<PapersEntity>().Target.Set(entity);

            foreach (var collider in papers.GetColliders())
            {
                collider.Disabled = true;
            }
        }

        private void HandleDragFromBelow(Entity entity)
        {
            var papers = GetBotEntity();

            if (papers == null)
                return;

            if (papers is PapersEntity == false)
                return;

            GivePaperToPlayer(papers as PapersEntity);
            SetGrabOnCooldown();
            papers.As<PapersEntity>().Target.Set(entity);

            foreach (var collider in papers.GetColliders())
            {
                collider.Disabled = true;
            }
        }
    }
}
