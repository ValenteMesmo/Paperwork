using GameCore;
using System;

namespace PaperWork
{
    public class Player :
        Collider
        , ICollisionHandler
        , IUpdateHandler
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }
        public bool Disabled { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        public bool Grounded { get; set; }
        public Paper GrabbedPaper { get; set; }
        public int TimeUntilDragDropEnable { get; set; }
        public const int DRAG_AND_DROP_COOLDOWN = 30;

        private readonly ICollisionHandler CollisionHandler;
        private readonly IUpdateHandler UpdateHandler;
        public readonly InputRepository Inputs;
        public readonly Detector<Paper> FeetPaperDetector;
        public readonly Detector<Paper> ChestPaperDetetor;

        public Player(
            InputRepository Inputs,
            Detector<Paper> FeetPaperDetector,
            Detector<Paper> ChestPaperDetetor)
        {
            this.Inputs = Inputs;
            ChestPaperDetetor.Parent =
                FeetPaperDetector.Parent = this;
            this.FeetPaperDetector = FeetPaperDetector;
            this.ChestPaperDetetor = ChestPaperDetetor;
            Width = 70;
            Height = 150;

            UpdateHandler = new UpdateGroup(
                new MoveHorizontallyOnInput(this, Inputs)
                , new AffectedByGravity(this)
                , new PlayersJump(this, Inputs)
                //TODO: Grab paper from bottonright 
                , new GrabPaperNearPlayersChest(this)
                , new GrabPaperNearPlayersFeet(this)
                , new SpecialDownDropPaper(this)
                , new DropPaper(this)
                , new LimitSpeed(this, 8, 15)
            );

            CollisionHandler = new CollisionHandlerGroup(
                new StopsWhenBotCollidingWith<IPlayerMovementBlocker>(this)
                , new StopsWhenTopCollidingWith<IPlayerMovementBlocker>(this)
                , new StopsWhenLeftCollidingWith<IPlayerMovementBlocker>(this)
                , new StopsWhenRightCollidingWith<IPlayerMovementBlocker>(this)
            );

        }

        public void Update()
        {
            UpdateHandler.Update();
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {Y}");
            if (GrabbedPaper != null)
            {
                GrabbedPaper.X = X;
                GrabbedPaper.Y = Y;
            }
            if (TimeUntilDragDropEnable > 0)
                TimeUntilDragDropEnable--;
        }

        public void BotCollision(Collider collider)
        {
            CollisionHandler.BotCollision(collider);
        }

        public void TopCollision(Collider collider)
        {
            CollisionHandler.TopCollision(collider);
        }

        public void LeftCollision(Collider collider)
        {
            CollisionHandler.LeftCollision(collider);
        }

        public void RightCollision(Collider collider)
        {
            CollisionHandler.RightCollision(collider);
        }
    }
}
