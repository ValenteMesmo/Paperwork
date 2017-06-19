using GameCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace PaperWork
{
    public class PlayerDestroyed : Animation, DimensionalThing
    {
        private readonly SimpleAnimation animation;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        private int Duration = 100;
        private readonly IGame Game;
        public PlayerDestroyed(IGame Game, bool facingRight)
        {
            this.Game = Game;
            var walkingWidth = (int)(2000);

            animation = GeneratedContent.Create_recycle_mantis_Death(
                -500,
                -500,
                0,
                walkingWidth,
                walkingWidth,
                facingRight);
        }

        public IEnumerable<Texture> GetTextures()
        {
            return animation.GetTextures();
        }

        public void Update()
        {
            animation.Update();
            Duration--;
            if (Duration == 0)
                Game.Restart();
        }
    }

    //TODO: brilho dos olhos tem que ficar do mesmo lado!
    //TODO: prevent player from special drop down when near the roof
    //TODO: arregalar os olhos do player quando estiver correndo perigo
    //https://s-media-cache-ak0.pinimg.com/originals/f4/cc/41/f4cc41f0e3ecd673a6292ca150bcc32d.jpg
    //(fear in anime)
    //TODO: camera shake
    //sleep on death
    //sleep on combo
    //keep score
    public class Player :
        Collider
        , ICollisionHandler
        , IUpdateHandler
        , Animation
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

        public const int DRAG_AND_DROP_COOLDOWN = 30;

        private readonly ICollisionHandler CollisionHandler;
        private readonly IUpdateHandler UpdateHandler;
        public readonly InputRepository Inputs;
        private readonly Animator Animation;

        public bool FacingRight { get; set; }
        public bool AnimationFacingRight { get; set; }

        public Player(
            InputRepository Inputs,
            World world,
            //Remove this
            IGame Game1)
        {
            this.Inputs = Inputs;
            Width = 700;
            Height = 1100;

            var walkingWidth = (int)(700 * 1.4f);
            var head_right = new Texture("Head", 200, -100, (int)(540 * 1.4f), (int)(380 * 1.4f)) { ZIndex = 0, Flipped = true };
            var head_left = new Texture("Head", -300, -100, (int)(540 * 1.4f), (int)(380 * 1.4f)) { ZIndex = 0 };

            SimpleAnimation stand_right = new SimpleAnimation(
                 new AnimationFrame(10,
                 new Texture("Walk0001", 0, 100, walkingWidth, walkingWidth) { Flipped = true, ZIndex = 1 }
                 , head_right)

            );

            SimpleAnimation stand_left = new SimpleAnimation(
                new AnimationFrame(10, new Texture("Walk0001", -300, 100, walkingWidth, walkingWidth) { ZIndex = 1 }
                , head_left)
            );

            SimpleAnimation walkAnimation_right = new SimpleAnimation(
                  new AnimationFrame(10, new Texture("Walk0001", 0, 100, walkingWidth, walkingWidth) { Flipped = true, ZIndex = 1 }, head_right)
                , new AnimationFrame(10, new Texture("Walk0002", 0, 100, walkingWidth, walkingWidth) { Flipped = true, ZIndex = 1 }, head_right)
                , new AnimationFrame(10, new Texture("Walk0001", 0, 100, walkingWidth, walkingWidth) { Flipped = true, ZIndex = 1 }, head_right)
                , new AnimationFrame(10, new Texture("Walk0003", 0, 100, walkingWidth, walkingWidth) { Flipped = true, ZIndex = 1 }, head_right)
            );

            SimpleAnimation walkAnimation_left = new SimpleAnimation(
                 new AnimationFrame(10, new Texture("Walk0001", -300, 100, walkingWidth, walkingWidth) { ZIndex = 1 }, head_left)
                , new AnimationFrame(10, new Texture("Walk0002", -300, 100, walkingWidth, walkingWidth) { ZIndex = 1 }, head_left)
                , new AnimationFrame(10, new Texture("Walk0001", -300, 100, walkingWidth, walkingWidth) { ZIndex = 1 }, head_left)
                , new AnimationFrame(10, new Texture("Walk0003", -300, 100, walkingWidth, walkingWidth) { ZIndex = 1 }, head_left)
            );

            Animation = new Animator(
                new AnimationTransition(
                    new[] {
                        walkAnimation_left
                        ,stand_left
                        ,stand_right
                    },
                    walkAnimation_right,
                    () => FacingRight && HorizontalSpeed > 0
                    , () => AnimationFacingRight = true
                )
                , new AnimationTransition(
                    new[] {
                        walkAnimation_right
                        ,stand_left
                        ,stand_right
                    },
                    walkAnimation_left,
                    () => FacingRight == false && HorizontalSpeed < 0
                    , () => AnimationFacingRight = false
                )
                , new AnimationTransition(
                    new[] {
                        walkAnimation_left
                        ,walkAnimation_right
                        ,stand_right
                    },
                    stand_left,
                    () => FacingRight == false && HorizontalSpeed == 0
                    , () => AnimationFacingRight = false
                )
                , new AnimationTransition(
                    new[] {
                        walkAnimation_right
                        ,walkAnimation_left
                        ,stand_left
                    }
                    , stand_right
                    , () => FacingRight && HorizontalSpeed == 0
                    , () => AnimationFacingRight = true
                )
            );

            Right_ChestPaperDetetor = new Detector<IPlayerMovementBlocker>(1000, -500, 100, 100) { Parent = this };
            Right_FeetPaperDetector = new Detector<IPlayerMovementBlocker>(1000, 500, 100, 100) { Parent = this };
            world.Add(Right_ChestPaperDetetor);
            world.Add(Right_FeetPaperDetector);

            Left_ChestPaperDetetor = new Detector<IPlayerMovementBlocker>(-450, -500, 100, 100) { Parent = this };
            Left_FeetPaperDetector = new Detector<IPlayerMovementBlocker>(-450, 500, 100, 100) { Parent = this };
            world.Add(Left_ChestPaperDetetor);
            world.Add(Left_FeetPaperDetector);

            GroundDetector = new Detector<IPlayerMovementBlocker>(100, 1250, 500, 250) { Parent = this };
            world.Add(GroundDetector);

            //HeadDetector = new Detector<Paper>(200, -400, 250, 250) { Parent = this };
            //world.Add(HeadDetector);

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
                , new DropPaper_Right(this)
                , new DropPaper_Left(this)
                , new LimitSpeed(this, 80, 150)
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
            Animation.Update();
            Grounded = GroundDetector.GetDetectedItems().Any();

            UpdateHandler.Update();

            if (GrabbedPaper != null)
            {
                if (AnimationFacingRight)
                    GrabbedPaper.X = X;
                else
                    GrabbedPaper.X = X - Width / 2;

                GrabbedPaper.Y = Y - GrabbedPaper.Height + World.SPACE_BETWEEN_THINGS;
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

        public IEnumerable<Texture> GetTextures()
        {
            return Animation.GetTextures();
        }
    }
}
