using GameCore;
using PaperWork.GameEntities.Collisions;
using System;

namespace PaperWork.GameEntities.Player.Updates
{
    class DragAndDropHandler : IHandleUpdates
    {
        private readonly Property<PapersEntity> DraggedEntity = new Property<PapersEntity>();
        private readonly UpdateHandlerAggregator updateHandler;
        private DateTime cooldown;

        public DragAndDropHandler(
            InputRepository Inputs
            , Func<bool> FacingRightDirection
            , Func<bool> IsGrounded
            , Func<Entity> RightEntity
            , Func<Entity> BotRightEntity
            , Func<Entity> LeftEnity
            , Func<Entity> BotLeftEntity
            , Func<Entity> BotEntity
            )
        {
            Func<bool> CooldownEnded = () => cooldown <= DateTime.Now;
            Action SetOnCooldown = () => cooldown = DateTime.Now.AddMilliseconds(250);

            updateHandler = new UpdateHandlerAggregator(
                new DragBotPaper(
                    DraggedEntity.Set
                    , SetOnCooldown
                    , () => Inputs.Action1
                    , DraggedEntity.IsNull
                    , CooldownEnded
                    , () => Inputs.Down
                    , BotEntity)
                , new DragMidPaper(
                    DraggedEntity.Set
                    , SetOnCooldown
                    , () => Inputs.Action1
                    , DraggedEntity.IsNull
                    , CooldownEnded
                    , FacingRightDirection
                    , RightEntity
                    , LeftEnity)
                , new DragMidBotPaper(
                    DraggedEntity.Set
                    , SetOnCooldown
                    , () => Inputs.Action1
                    , DraggedEntity.IsNull
                    , CooldownEnded
                    , IsGrounded
                    , FacingRightDirection
                    , BotRightEntity
                    , BotLeftEntity
                )
                , new DropThePapers(
                    DraggedEntity.Get,
                    () => Inputs.Action1,
                    CooldownEnded,
                    SetOnCooldown,
                    DraggedEntity.SetDefaut,
                     () => RightEntity == null,
                    () => Inputs.Down,
                    () => FacingRightDirection() == false,
                    50)
                , new DropThePapers(
                    DraggedEntity.Get,
                    () => Inputs.Action1,
                    CooldownEnded,
                    SetOnCooldown,
                    DraggedEntity.SetDefaut,
                    () => LeftEnity() == null,
                    () => Inputs.Down,
                    FacingRightDirection,
                    -40));

        }

        public void Update(Entity entity)
        {
            updateHandler.Update(entity);
        }
    }
}
