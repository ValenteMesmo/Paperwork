using GameCore;
using System;
using System.Linq;

namespace PaperWork
{
    public class GrabPaperNearPlayersChest : IUpdateHandler
    {
        private readonly Player Player;

        public GrabPaperNearPlayersChest(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper == null
                && Player.Inputs.Action1)
            {
                var papers = Player.ChestPaperDetetor.GetDetectedItems();
                if (papers.Any())
                {
                    Player.GrabbedPaper = papers.First();
                    Player.GrabbedPaper.Disabled = true;
                }
            }
        }
    }

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
        public bool Disabled { get; set; }

        public bool Grounded { get; set; }
        public Paper GrabbedPaper { get; set; }

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
                ,new GrabPaperNearPlayersChest(this)
                , new LimitSpeed(this, 8, 15)
            );
        }

        public void Update()
        {
            UpdateHandler.Update();
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {Y}");
            if (GrabbedPaper != null)
            {
                GrabbedPaper.X = X;
                GrabbedPaper.Y= Y;
            }
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
    }
}
