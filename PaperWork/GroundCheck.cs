using GameCore;

namespace PaperWork
{
    public class GroundCheck :
        IUpdateHandler
        , IBeforeCollisionHandler
        , ICollider
        , ICollisionHandler
    {
        private readonly Player Player;

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float HorizontalSpeed { get; set; }
        public float VerticalSpeed { get; set; }

        public GroundCheck(Player Player)
        {
            this.Player = Player;
            Width = Player.Width -10;
            Height = 10;
        }

        public void Update()
        {
            X = Player.X + 5;
            Y = Player.Bottom() + 10;
            Player.Grounded = false;
        }

        public void BeforeCollision()
        {
            Player.Grounded = false;
        }

        public void BotCollision(ICollider other)
        {
            if (other is Block)
                Player.Grounded = true;
        }

        public void LeftCollision(ICollider other)
        {
            if (other is Block)
                Player.Grounded = true;
        }

        public void RightCollision(ICollider other)
        {
            if (other is Block)
                Player.Grounded = true;
        }

        public void TopCollision(ICollider other)
        {
            if (other is Block)
                Player.Grounded = true;
        }
    }
}