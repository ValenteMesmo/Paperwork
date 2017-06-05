using System;
using GameCore;

namespace PaperWork
{
    public class GroundCheck :
        IUpdateHandler
        , IAfterUpdateHandler
        , ICollider
        , ICollisionHandler
    {
        private readonly Player Player;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }
        public bool Disabled { get; set; }

        public GroundCheck(Player Player)
        {
            this.Player = Player;
            Width = Player.Width -50;
            Height = 30;
        }

        public void Update()
        {
            X = Player.X + 25;
            Y = Player.Bottom() + 10;
            Player.Grounded = false;
        }

        public void AfterUpdate()
        {
            Player.Grounded = false;
        }

        public void BotCollision(ICollider other)
        {
            if (other is IPlayerMovementBlocker)
                Player.Grounded = true;
        }

        public void LeftCollision(ICollider other)
        {
            if (other is IPlayerMovementBlocker)
                Player.Grounded = true;
        }

        public void RightCollision(ICollider other)
        {
            if (other is IPlayerMovementBlocker)
                Player.Grounded = true;
        }

        public void TopCollision(ICollider other)
        {
            if (other is IPlayerMovementBlocker)
                Player.Grounded = true;
        }
    }
}