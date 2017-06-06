using System;
using GameCore;

namespace PaperWork
{
    public class GroundCheck :
        IUpdateHandler
        , IAfterUpdateHandler
        , Collider
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
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        public GroundCheck(Player Player)
        {
            this.Player = Player;
            Width = Player.Width -20;
            Height = 30;
        }

        public void Update()
        {
            X = Player.X + 10;
            Y = Player.Bottom() + 10;
            Player.Grounded = false;
        }

        public void AfterUpdate()
        {
            Player.Grounded = false;
        }

        public void BotCollision(Collider other)
        {
            if (other is IPlayerMovementBlocker)
                Player.Grounded = true;
        }

        public void LeftCollision(Collider other)
        {
            if (other is IPlayerMovementBlocker)
                Player.Grounded = true;
        }

        public void RightCollision(Collider other)
        {
            if (other is IPlayerMovementBlocker)
                Player.Grounded = true;
        }

        public void TopCollision(Collider other)
        {
            if (other is IPlayerMovementBlocker)
                Player.Grounded = true;
        }
    }
}