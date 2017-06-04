using System;
using GameCore;

namespace PaperWork
{
    public class PlayersJump : IUpdateHandler
    {
        private readonly Player Player;
        private readonly InputRepository Input;

        public PlayersJump(Player Player, InputRepository Input)
        {
            this.Player = Player;
            this.Input = Input;
        }

        public void Update()
        {
            if (Player.Grounded && Input.Up)
            {
                Player.VerticalSpeed = -10;
            }
        }
    }

    public class Player :
        ICollider
        , ICollisionHandler
        , IUpdateHandler
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float HorizontalSpeed { get; set; }
        public float VerticalSpeed { get; set; }

        public bool Grounded { get; set; }

        private readonly ICollisionHandler CollisionHandler;
        private readonly IUpdateHandler UpdateHandler;

        public Player(InputRepository Inputs)
        {
            Width = 50;
            Height = 100;

            CollisionHandler = new StopsWhenCollidingWith<Block>(this);
            UpdateHandler = new UpdateGroup(
                new MoveHorizontallyOnInput(this, Inputs)
                , new AffectedByGravity(this)
                , new PlayersJump(this, Inputs)
                , new LimitSpeed(this, 3, 5)
            );
        }

        public void BotCollision(ICollider collider)
        {
            CollisionHandler.BotCollision(collider);
        }

        public void TopCollision(ICollider collider)
        {
            CollisionHandler.TopCollision(collider);
        }

        public void LeftCollision(ICollider collider)
        {
            CollisionHandler.LeftCollision(collider);
        }

        public void RightCollision(ICollider collider)
        {
            CollisionHandler.RightCollision(collider);
        }

        public void Update()
        {
            UpdateHandler.Update();
        }
    }
}
