using GameCore;
using PaperWork.GameEntities.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaperWork.GameEntities.Player.Updates
{
    class DragAndDropHandler : IHandleUpdates
    {
        private readonly Property<PapersEntity> DraggedEntity = new Property<PapersEntity>();
        private readonly UpdateHandlerAggregator updateHandler;
        private DateTime cooldown;
        private readonly Entity entity;

        public DragAndDropHandler(
            Entity entity
            , InputRepository Inputs
            , Func<bool> FacingRightDirection
            , Func<bool> IsGrounded
            , Func<IEnumerable<Entity>> RightEntity
            , Func<IEnumerable<Entity>> BotRightEntity
            , Func<IEnumerable<Entity>> LeftEnity
            , Func<IEnumerable<Entity>> BotLeftEntity
            , Func<IEnumerable<Entity>> BotEntity
            , Action<float> SetHorizontalSpeed
            , Func<bool> TopPositionFree
            )
        {
            this.entity = entity;
            Func<bool> CooldownEnded = () => cooldown <= DateTime.Now;
            Action SetOnCooldown = () => cooldown = DateTime.Now.AddMilliseconds(250);

            updateHandler = new UpdateHandlerAggregator(
                new DragBotPaper(
                    entity
                    , DraggedEntity.Set
                    , SetOnCooldown
                    , () => Inputs.Action1
                    , DraggedEntity.IsNull
                    , CooldownEnded
                    , () => Inputs.Down
                    , BotEntity)
                , new DragMidPaper(
                    entity
                    , DraggedEntity.Set
                    , SetOnCooldown
                    , () => Inputs.Action1
                    , DraggedEntity.IsNull
                    , CooldownEnded
                    , FacingRightDirection
                    , RightEntity
                    , LeftEnity)
                , new DragMidBotPaper(
                    entity
                    , DraggedEntity.Set
                    , SetOnCooldown
                    , () => Inputs.Action1
                    , DraggedEntity.IsNull
                    , CooldownEnded
                    , IsGrounded
                    , FacingRightDirection
                    , BotRightEntity
                    , BotLeftEntity
                )
                , new DropThePapers(entity
                 , DraggedEntity.Get,
                    () => Inputs.Action1,
                    CooldownEnded,
                    SetOnCooldown,
                    DraggedEntity.SetDefaut,
                    () => RightEntity().Any() == false,
                    () => Inputs.Down,
                    () => FacingRightDirection() == false
                    , SetHorizontalSpeed
                    , TopPositionFree
                    , 50)
                , new DropThePapers(
                    entity
                 , DraggedEntity.Get,
                    () => Inputs.Action1,
                    CooldownEnded,
                    SetOnCooldown,
                    DraggedEntity.SetDefaut,
                    () => LeftEnity().Any() == false,
                    () => Inputs.Down,
                    FacingRightDirection
                    , SetHorizontalSpeed
                    , TopPositionFree
                    , -40));

        }

        public void Update()
        {
            updateHandler.Update();
        }
    }
}
