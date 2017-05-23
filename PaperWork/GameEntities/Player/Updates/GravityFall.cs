﻿using GameCore;
using System;

namespace PaperWork.PlayerHandlers.Updates
{
    public class GravityIncreasesVerticalSpeed : IHandleUpdates
    {
        public const float VELOCITY = .6f;
        public const float MAX_SPEED = 4f;

        private readonly Func<float> GetVerticalSpeed;
        private readonly Action<float> SetVerticalSpeed;
        private readonly Func<bool> Grounded;

        public GravityIncreasesVerticalSpeed(
            Func<float> GetVerticalSpeed
            , Action<float> SetVerticalSpeed
            ,Func<bool> Grounded) 
        {
            this.GetVerticalSpeed = GetVerticalSpeed;
            this.SetVerticalSpeed = SetVerticalSpeed;
            this.Grounded = Grounded;
        }

        public void Update(Entity entity)
        {
            if (Grounded())
                return;

            var verticalSpeed = GetVerticalSpeed() + VELOCITY;
            if (verticalSpeed > MAX_SPEED)
                verticalSpeed = MAX_SPEED;

            SetVerticalSpeed(verticalSpeed);
        }
    }
}
