using System;
using GameCore;
using Microsoft.Xna.Framework;

namespace PaperWork
{
    public class Paper :
        Collider
        , ICollisionHandler
        , IUpdateHandler
        , IPlayerMovementBlocker
        , Texture
    {
        private ICollisionHandler CollisionHandler;
        private IUpdateHandler UpdateHandler;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        public int TextureOffSetX { get; }
        public int TextureOffSetY { get => -Height; }
        public int TextureWidth { get => Width; }
        public int TextureHeight { get => Height * 2; }
        public string TextureName { get => "papers"; }
        public Color Color { get; set; }
        public bool Disabled { get; set; }

        public Paper()
        {
            Width = 100;
            Height = 100;
            UpdateHandler = new UpdateGroup(
               new AffectedByGravity(this)
               //, new SlowPaperDown(this)
               , new LimitSpeed(this, 10, 8)
            );
            
            //handle bot collision with player e ou inverso
            CollisionHandler =
                new CollisionHandlerGroup(
                    new StopsWhenBotCollidingWith<IPlayerMovementBlocker>(this)
                    //,new StopsWhenBotCollidingWith<Player>(this)
                    ,new StopsWhenRightCollidingWith<IPlayerMovementBlocker>(this)
                    //,new StopsWhenLeftCollidingWith<IPlayerMovementBlocker>(this)
            );
        }

        public void Update()
        {
            if (X >= (100 * 11) + 12
                && Y <= 300 + World.SPACE_BETWEEN_THINGS
                && VerticalSpeed == 0)
            {
                HorizontalSpeed = -2;

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
                    Y > 300+World.SPACE_BETWEEN_THINGS
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
    }
}
