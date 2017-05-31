using GameCore;
using System;
using System.Collections.Generic;

namespace PaperWork.GameEntities.Player.Updates
{
    public class DragBotPaper : BaseDragHandler
    {
        private readonly Func<bool> GrabButtonPressed;
        private readonly Func<bool> PlayerHandsAreFree;
        private readonly Func<bool> GrabCooldownEnded;
        private readonly Func<bool> DownButtonPressed;
        private readonly Func<IEnumerable<Entity>> GetBotEntity;

        public DragBotPaper(
            Entity entity
            ,Action<PapersEntity> GivePaperToPlayer
            , Action SetGrabOnCooldown
            , Func<bool> GrabButtonPressed
            , Func<bool> PlayerHandsAreFree
            , Func<bool> GrabCooldownEnded
            , Func<bool> DownButtonPressed
            , Func<IEnumerable<Entity>> GetBotEntity
            ) : base(entity, GivePaperToPlayer, SetGrabOnCooldown)
        {
            this.GrabButtonPressed = GrabButtonPressed;
            this.PlayerHandsAreFree = PlayerHandsAreFree;
            this.GrabCooldownEnded = GrabCooldownEnded;
            this.DownButtonPressed = DownButtonPressed;
            this.GetBotEntity = GetBotEntity;
        }

        protected override IEnumerable<Entity> GetEntityToGrab()
        {
            if (GrabButtonPressed()
               && PlayerHandsAreFree()
               && GrabCooldownEnded()
               && DownButtonPressed())
            {
                return GetBotEntity();
            }

            return null;
        }
    }
}
