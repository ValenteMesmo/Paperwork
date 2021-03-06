﻿using System;
using System.Collections.Generic;
using GameCore;
using Microsoft.Xna.Framework;
using System.Linq;

namespace PaperWork
{
    public class Paper :
        Collider
        , ICollisionHandler
        , SomethingThatHandleUpdates
        , IPlayerMovementBlocker
        , IPaperMovementBlocker
        , Animation
        , SomethingWithAudio
    {
        private ICollisionHandler CollisionHandler;
        private SomethingThatHandleUpdates UpdateHandler;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        public bool Disabled { get; set; }

        public const int SIZE = 1000;
        private SimpleAnimation Animation;

        public Paper()
        {
            Width = SIZE;
            Height = SIZE;
            UpdateHandler = new UpdateGroup(
               new AffectedByGravity(this)
               , new LimitSpeed(this, 100, 80)
            );

            CollisionHandler =
                new CollisionHandlerGroup(
                    new StopsWhenBotCollidingWith<IPaperMovementBlocker>(this, AudiosToPlay.Add)
                    , new StopsWhenRightCollidingWith<IPaperMovementBlocker>(this)
            );

            Animation = GeneratedContent.Create_trash_bag_Trash(
                -(int)(SIZE * 0.1f)
                , -(int)(SIZE * 0.2f)
                , 0.83f
                , (int)(SIZE * 1.2f)
                , (int)(SIZE * 1.2f));
        }

        public void Update()
        {
            Animation.Update();
            if (
                X > ((1000 + World.SPACE_BETWEEN_THINGS) * 11) + 100
                && Y <= 3000 + World.SPACE_BETWEEN_THINGS
                && VerticalSpeed == 0)
            {
                HorizontalSpeed = -250;
            }
            else
            {
                HorizontalSpeed = 0;
            }
            UpdateHandler.Update();
        }

        public void BotCollision(Collider collider)
        {
            if (collider is Paper || collider is Block)
            {
                if (
                    Y > 3000 + World.SPACE_BETWEEN_THINGS
                    && HorizontalSpeed == 0
                    && X % Width + World.SPACE_BETWEEN_THINGS != 0)
                    X = MathHelper.RoundUp(X, Width + World.SPACE_BETWEEN_THINGS);
            }
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

        public Color Color { get { return Animation.GetColor(); } set { Animation.ChangeColor(value); } }
        
        public IEnumerable<Texture> GetTextures()
        {
            return Animation.GetTextures();
        }

        private List<string> AudiosToPlay = new List<string>();

        public IEnumerable<string> GetAudiosToPlay()
        {
            var result = AudiosToPlay.ToList();
            AudiosToPlay.Clear();
            return result;
        }
    }
}
