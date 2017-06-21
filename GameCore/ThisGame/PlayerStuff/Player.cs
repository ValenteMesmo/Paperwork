﻿using GameCore;
using System.Collections.Generic;
using System.Linq;

namespace PaperWork
{
    //TODO: camera shake
    //keep score
    // jump animation
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
        private readonly Animator HandsAnimation;

        public bool FacingRight { get; set; }
        public bool AnimationFacingRight { get; set; }

        //remove this
        public bool Ended => false;

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
            HandsAnimation = CreateHandsAnimator();

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

            var headUp_right = GeneratedContent.Create_recycle_mantis_HeadUp(200, -100, 0.85f, (int)(540 * 1.4f), (int)(380 * 1.4f), true);
            var headUp_left = GeneratedContent.Create_recycle_mantis_HeadUp(leftOffsetX, -100, 0.85f, (int)(540 * 1.4f), (int)(380 * 1.4f));

            var Animator = new Animator(
                new AnimationTransitionOnCondition(new Animation[] { head_right, headUp_right, headUp_left }, head_left, () => AnimationFacingRight == false && Inputs.Up == false)
                , new AnimationTransitionOnCondition(new Animation[] { head_left, headUp_right, headUp_left }, head_right, () => AnimationFacingRight && Inputs.Up == false)
                , new AnimationTransitionOnCondition(new Animation[] { head_right, headUp_right, head_left }, headUp_left, () => AnimationFacingRight == false && Inputs.Up)
                , new AnimationTransitionOnCondition(new Animation[] { head_left, head_right, headUp_left }, headUp_right, () => AnimationFacingRight && Inputs.Up)
            );

            return Animator;
        }

        private Animator CreateHandsAnimator()
        {
            var hand_right = GeneratedContent.Create_recycle_mantis_HandsDown(-300, 250, 0.80f, (int)(390 * 1.4f), (int)(320 * 1.4f), true);
            var hand_left = GeneratedContent.Create_recycle_mantis_HandsDown(380, 250, 0.80f, (int)(390 * 1.4f), (int)(320 * 1.4f));

            var hand_air_right = GeneratedContent.Create_recycle_mantis_HandsUp(200, -100, 0.80f, (int)(390 * 1.4f), (int)(320 * 1.4f), true);
            var hand_air_left = GeneratedContent.Create_recycle_mantis_HandsUp(-100, -100, 0.80f, (int)(390 * 1.4f), (int)(320 * 1.4f));
            
            var Animator = new Animator(new AnimationTransitionOnCondition(
                    new Animation[] {
                         hand_right
                        ,hand_air_left
                        ,hand_air_right
                    }
                    , hand_left
                    , () =>
                        AnimationFacingRight == false
                        && Inputs.Action1 == false
                        && GrabbedPaper == null)
                , new AnimationTransitionOnCondition(
                    new Animation[] {
                        hand_left
                        ,hand_air_left
                        ,hand_air_right
                    }
                    , hand_right, () => AnimationFacingRight && (Inputs.Action1 == false && GrabbedPaper == null))
                , new AnimationTransitionOnCondition(
                    new Animation[] {
                         hand_right
                        , hand_left
                        ,hand_air_right
                    }
                    , hand_air_left, () => AnimationFacingRight == false && (Inputs.Action1 || GrabbedPaper != null))
                , new AnimationTransitionOnCondition(
                    new Animation[] {
                         hand_right
                        , hand_left
                        ,hand_air_left
                    }
                    , hand_air_right, () => AnimationFacingRight && (Inputs.Action1 || GrabbedPaper != null))
            );

            return Animator;
        }

        const int leftOffsetX = -300;
        private Animator CreateBodyAnimator()
        {
            var walkingWidth = (int)(700 * 1.4f);

            var walkAnimation_right = GeneratedContent.Create_recycle_mantis_Walk(0, 100, 0.86f, walkingWidth, walkingWidth, true);
            var walkAnimation_left = GeneratedContent.Create_recycle_mantis_Walk(leftOffsetX, 100, 0.86f, walkingWidth, walkingWidth);

            var stand_right = GeneratedContent.Create_recycle_mantis_Stand(0, 100, 0.86f, walkingWidth, walkingWidth, true);
            var stand_left = GeneratedContent.Create_recycle_mantis_Stand(leftOffsetX, 100, 0.86f, walkingWidth, walkingWidth);

            var Animator = new Animator(
                new AnimationTransitionOnCondition(
                    new[] {
                        walkAnimation_left
                        ,stand_left
                        ,stand_right
                    },
                    walkAnimation_right,
                    () => FacingRight && HorizontalSpeed > 0
                    , () => AnimationFacingRight = true
                )
                , new AnimationTransitionOnCondition(
                    new[] {
                        walkAnimation_right
                        ,stand_left
                        ,stand_right
                    },
                    walkAnimation_left,
                    () => FacingRight == false && HorizontalSpeed < 0
                    , () => AnimationFacingRight = false
                )
                , new AnimationTransitionOnCondition(
                    new[] {
                        walkAnimation_left
                        ,walkAnimation_right
                        ,stand_right
                    },
                    stand_left,
                    () => FacingRight == false && HorizontalSpeed == 0
                    , () => AnimationFacingRight = false
                )
                , new AnimationTransitionOnCondition(
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
            HandsAnimation.Update();

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
            return BodyAnimation.GetTextures()
                .Concat(HeadAnimation.GetTextures())
                .Concat(HandsAnimation.GetTextures());
        }
    }
}
