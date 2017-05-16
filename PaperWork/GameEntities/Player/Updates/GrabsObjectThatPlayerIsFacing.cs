//using GameCore;
//using System;

//namespace PaperWork.GameEntities.Player.Updates
//{
//    class GrabsObjectThatPlayerIsFacing : IHandleEntityUpdates
//    {
//        private readonly Func<bool> PlayerIsNotHoldingPapers;
//        private readonly Action<PapersEntity> GivePaperToPlayer;
//        private readonly Func<bool> GrabButtonPressed;
//        private readonly Func<bool> GrabCooldownEnded;
//        private readonly Action PutGrabActionOnCooldown;

//        public GrabsObjectThatPlayerIsFacing(
//             Func<bool> GrabButtonPressed
//            , Func<bool> PlayerIsNotHoldingPapers
//            , Action<PapersEntity> GivePaperToPlayer
//            , Func<bool> GrabCooldownEnded
//            , Action PutGrabActionOnCooldown)
//        {
//            this.GrabButtonPressed = GrabButtonPressed;
//            this.PlayerIsNotHoldingPapers = PlayerIsNotHoldingPapers;
//            this.GivePaperToPlayer = GivePaperToPlayer;
//            this.GrabCooldownEnded = GrabCooldownEnded;
//            this.PutGrabActionOnCooldown = PutGrabActionOnCooldown;
//        }

//        public void Update(Entity ParentEntity)
//        {
//            if (PlayerIsNotHoldingPapers()
//                && GrabButtonPressed()
//                && GrabCooldownEnded())
//            {
//                var paper = EntityThatThePlayerIsTryingToGrab(
//                    new Coordinate2D(
//                        ParentEntity.Position.X + 50,
//                        ParentEntity.Position.Y + 50));

//                if (paper == null && !(paper is PapersEntity))
//                    return;

//                PutGrabActionOnCooldown();

//                paper.As<PapersEntity>().Grab(ParentEntity);
//                GivePaperToPlayer(paper.As<PapersEntity>());
//            }
//        }
//    }
//}
