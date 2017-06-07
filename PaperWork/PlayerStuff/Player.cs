using GameCore;
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
        public Detector<IPlayerMovementBlocker> Right_ChestPaperDetetor { get; }
        public Detector<IPlayerMovementBlocker> Right_FeetPaperDetector { get; }
        public Detector<IPlayerMovementBlocker> Left_ChestPaperDetetor { get; }
        public Detector<IPlayerMovementBlocker> Left_FeetPaperDetector { get; }
        public Detector<IPlayerMovementBlocker> GroundDetector { get; }
        public Detector<Paper> HeadDetector { get; }

        public const int DRAG_AND_DROP_COOLDOWN = 30;

        private readonly ICollisionHandler CollisionHandler;
        private readonly IUpdateHandler UpdateHandler;
        public readonly InputRepository Inputs;

        public bool FacingRight { get; set; }

        public Player(
            InputRepository Inputs,
            World world,
            //Remove this
            Game1 Game1)
        {
            this.Inputs = Inputs;
            Width = 70;
            Height = 110;

            Right_ChestPaperDetetor = new Detector<IPlayerMovementBlocker>(100, -50, 25, 25) { Parent = this };
            Right_FeetPaperDetector = new Detector<IPlayerMovementBlocker>(100, 50, 25, 25) { Parent = this };
            world.Add(Right_ChestPaperDetetor);
            world.Add(Right_FeetPaperDetector);

            Left_ChestPaperDetetor = new Detector<IPlayerMovementBlocker>(-80, -50, 25, 25) { Parent = this };
            Left_FeetPaperDetector = new Detector<IPlayerMovementBlocker>(-80, 50, 25, 25) { Parent = this };
            world.Add(Left_ChestPaperDetetor);
            world.Add(Left_FeetPaperDetector);

            GroundDetector = new Detector<IPlayerMovementBlocker>(10, 150, 50, 50) { Parent = this };
            world.Add(GroundDetector);

            HeadDetector = new Detector<Paper>(20, -40, 25, 25) { Parent = this };
            world.Add(HeadDetector);
            
            UpdateHandler = new UpdateGroup(
                new MoveHorizontallyOnInput(this, Inputs)
                //, new HandlePaperFallingInThehead(this)
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
                , new DropPaper_Right(this)
                , new DropPaper_Left(this)
                , new LimitSpeed(this, 8, 15)
            );

            CollisionHandler = new CollisionHandlerGroup(
                new StopsWhenBotCollidingWith<IPlayerMovementBlocker>(this)
                , new StopsWhenTopCollidingWith<Block>(this)
                , new HandlePaperFallingInThehead(this, Game1)
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
