using GameCore;
using System;
using System.Linq;

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
        public Detector<Paper> ChestPaperDetetor { get; }
        public Detector<Paper> FeetPaperDetector { get; }

        public const int DRAG_AND_DROP_COOLDOWN = 30;

        private readonly ICollisionHandler CollisionHandler;
        private readonly IUpdateHandler UpdateHandler;
        public readonly InputRepository Inputs;
        private readonly Detector<IPlayerMovementBlocker> GroundDetector;

        public Player(
            InputRepository Inputs,
            World world)
        {
            this.Inputs = Inputs;

            ChestPaperDetetor = new Detector<Paper>(80, -20, 50, 50) { Parent = this };
            FeetPaperDetector = new Detector<Paper>(80, 80, 50, 50) { Parent = this };
            GroundDetector = new Detector<IPlayerMovementBlocker>(10, 180, 50, 50) { Parent = this };
            
            world.Add(GroundDetector);
            world.Add(ChestPaperDetetor);
            world.Add(FeetPaperDetector);

            Width = 70;
            Height = 150;

            UpdateHandler = new UpdateGroup(
                new MoveHorizontallyOnInput(this, Inputs)
                , new AffectedByGravity(this)
                , new PlayersJump(this, Inputs)
                //TODO: Grab paper from bottonright 
                , new GrabPaperThatThePlayerIsStandingOn(this)
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
            world.Add(this);
        }

        public void Update()
        {
            Grounded = GroundDetector.GetDetectedItems().Any();

            UpdateHandler.Update();

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
