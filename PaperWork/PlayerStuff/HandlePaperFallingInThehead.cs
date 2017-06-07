﻿using GameCore;
using System.Linq;
using System;

namespace PaperWork
{
    public class HandlePaperFallingInThehead : ICollisionHandler
    {
        private readonly Player Player;

        public HandlePaperFallingInThehead(Player Player)
        {
            this.Player = Player;
        }

        public void BotCollision(Collider other)
        {
        }

        public void LeftCollision(Collider other)
        {
        }

        public void RightCollision(Collider other)
        {
        }

        public void TopCollision(Collider other)
        {
            if (other is Paper)
            {
                if (Player.Left_ChestPaperDetetor.GetDetectedItems().Any() == false
                    && Player.Left_FeetPaperDetector.GetDetectedItems().Any() == false)
                {
                    Player.HorizontalSpeed = -10;
                }
            }
        }

        //public void Update()
        //{
        //    if (Player.HeadDetector.GetDetectedItems().Any())
        //    {
        //        if (Player.Left_ChestPaperDetetor.GetDetectedItems().Any() == false
        //            && Player.Left_FeetPaperDetector.GetDetectedItems().Any() == false)
        //        {
        //            Player.HorizontalSpeed = -10;
        //        }
        //    }
        //}
    }
}