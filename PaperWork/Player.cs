using GameCore;
using System;

namespace PaperWork
{
    public class Player :
        ICollider
        , ICollisionHandler
        , IUpdateHandler
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }

        public bool Grounded { get; set; }

        private readonly ICollisionHandler CollisionHandler;
        private readonly IUpdateHandler UpdateHandler;

        public Player(InputRepository Inputs)
        {
            Width = 100;
            Height = 200;

            CollisionHandler = new CollisionHandlerGroup(
                new StopsWhenBotCollidingWith<IPlayerMovementBlocker>(this)
                , new StopsWhenTopCollidingWith<IPlayerMovementBlocker>(this)
                , new StopsWhenLeftCollidingWith<IPlayerMovementBlocker>(this)
                , new StopsWhenRightCollidingWith<IPlayerMovementBlocker>(this)
            );
            UpdateHandler = new UpdateGroup(
                new MoveHorizontallyOnInput(this, Inputs)
                , new AffectedByGravity(this)
                , new PlayersJump(this, Inputs)
                , new LimitSpeed(this, 8, 15)
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
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {Y}");
        }
    }
}
