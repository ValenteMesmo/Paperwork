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
        public Detector<Paper> Right_ChestPaperDetetor { get; }
        public Detector<Paper> Right_FeetPaperDetector { get; }
        public Detector<Paper> Left_ChestPaperDetetor { get; }
        public Detector<Paper> Left_FeetPaperDetector { get; }
        public  Detector<IPlayerMovementBlocker> GroundDetector { get; }

        public const int DRAG_AND_DROP_COOLDOWN = 30;

        private readonly ICollisionHandler CollisionHandler;
        private readonly IUpdateHandler UpdateHandler;
        public readonly InputRepository Inputs;

        public bool FacingRight { get; set; }

        public Player(
            InputRepository Inputs,
            World world)
        {
            this.Inputs = Inputs;

            Right_ChestPaperDetetor = new Detector<Paper>(80, -20, 50, 50) { Parent = this };
            Right_FeetPaperDetector = new Detector<Paper>(80, 80, 50, 50) { Parent = this };
            world.Add(Right_ChestPaperDetetor);
            world.Add(Right_FeetPaperDetector);

            Left_ChestPaperDetetor = new Detector<Paper>(-60, -20, 50, 50) { Parent = this };
            Left_FeetPaperDetector = new Detector<Paper>(-60, 80, 50, 50) { Parent = this };
            world.Add(Left_ChestPaperDetetor);
            world.Add(Left_FeetPaperDetector);
            
            GroundDetector = new Detector<IPlayerMovementBlocker>(10, 180, 50, 50) { Parent = this };
            world.Add(GroundDetector);

            Width = 70;
            Height = 150;

            UpdateHandler = new UpdateGroup(
                new MoveHorizontallyOnInput(this, Inputs)
                , new SetPlayerDirection(this)
                , new AffectedByGravity(this)
                , new PlayersJump(this, Inputs)
                , new GrabPaperNearPlayersFeetAsFirstOption_Right(this)
                , new GrabPaperNearPlayersFeetAsFirstOption_Left(this)
                , new GrabPaperThatThePlayerIsStandingOn(this)
                , new GrabPaperNearPlayersChest_Right(this)
                , new GrabPaperNearPlayersChest_Left(this)
                , new GrabPaperNearPlayersFeetAsLastOption_Right(this)
                , new GrabPaperNearPlayersFeetAsLastOption_Left(this)
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
