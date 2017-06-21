using GameCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace PaperWork
{
    //TODO: brilho dos olhos tem que ficar do mesmo lado!
    //TODO: prevent player from special drop down when near the roof
    //TODO: arregalar os olhos do player quando estiver correndo perigo
    //https://s-media-cache-ak0.pinimg.com/originals/f4/cc/41/f4cc41f0e3ecd673a6292ca150bcc32d.jpg
    //(fear in anime)
    //TODO: camera shake
    //keep score
    //close game when ESC pressed
    // jump animation
    // look up animation (open mouth)
    //antenas
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
        private readonly Animator BodyAnimation;
        private readonly Animator HeadAnimation;

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

            BodyAnimation = CreateBodyAnimator();
            HeadAnimation = CreateHeadAnimator();

            Right_ChestPaperDetetor = new Detector<IPlayerMovementBlocker>(1150, -500, 100, 100) { Parent = this };
            Right_FeetPaperDetector = new Detector<IPlayerMovementBlocker>(1150, 500, 100, 100) { Parent = this };
            world.Add(Right_ChestPaperDetetor);
            world.Add(Right_FeetPaperDetector);

            Left_ChestPaperDetetor = new Detector<IPlayerMovementBlocker>(-600, -500, 100, 100) { Parent = this };
            Left_FeetPaperDetector = new Detector<IPlayerMovementBlocker>(-600, 500, 100, 100) { Parent = this };
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

        private Animator CreateHeadAnimator()
        {
            var head_right = GeneratedContent.Create_recycle_mantis_Head(200, -100, 0.85f, (int)(540 * 1.4f), (int)(380 * 1.4f), true);
            var head_left = GeneratedContent.Create_recycle_mantis_Head(leftOffsetX, -100, 0.85f, (int)(540 * 1.4f), (int)(380 * 1.4f));

            var Animator = new Animator(
                new AnimationTransition(new Animation[] { head_right }, head_left, () => AnimationFacingRight == false)
                , new AnimationTransition(new Animation[] { head_left }, head_right, () => AnimationFacingRight)

            );

            return Animator;
        }

        const int leftOffsetX = -300;
        private Animator CreateBodyAnimator()
        {
            var walkingWidth = (int)(700 * 1.4f);

            var walkAnimation_right = GeneratedContent.Create_recycle_mantis_Walk(0, 100, 0.86f, walkingWidth, walkingWidth, true);
            var walkAnimation_left = GeneratedContent.Create_recycle_mantis_Walk(leftOffsetX, 100, 0.86f, walkingWidth, walkingWidth);

            var stand_right = GeneratedContent.Create_recycle_mantis_Walk(0, 100, 0.86f, walkingWidth, walkingWidth, true);
            var stand_left = GeneratedContent.Create_recycle_mantis_Walk(leftOffsetX, 100, 0.86f, walkingWidth, walkingWidth);

            var Animator = new Animator(
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

            return Animator;
        }

        public void Update()
        {
            BodyAnimation.Update();
            HeadAnimation.Update();
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
            return BodyAnimation.GetTextures().Concat(HeadAnimation.GetTextures());
        }
    }
}
