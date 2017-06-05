using System;
using GameCore;
using Microsoft.Xna.Framework;

namespace PaperWork
{

    public class CollisionHandlerGroup : ICollisionHandler
    {
        private readonly ICollisionHandler[] Handlers;

        public CollisionHandlerGroup(params ICollisionHandler[] Handlers)
        {
            this.Handlers = Handlers;
        }

        public void BotCollision(ICollider other)
        {
            foreach (var item in Handlers)
            {
                item.BotCollision(other);
            }
        }

        public void LeftCollision(ICollider other)
        {
            foreach (var item in Handlers)
            {
                item.LeftCollision(other);
            }
        }

        public void RightCollision(ICollider other)
        {
            foreach (var item in Handlers)
            {
                item.RightCollision(other);
            }
        }

        public void TopCollision(ICollider other)
        {
            foreach (var item in Handlers)
            {
                item.TopCollision(other);
            }
        }
    }

    public class Paper :
        ICollider
        , ICollisionHandler
        , IUpdateHandler
        , IPlayerMovementBlocker
        , ITexture
    {
        private ICollisionHandler CollisionHandler;
        private IUpdateHandler UpdateHandler;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }

        public int TextureOffSetX { get; }
        public int TextureOffSetY { get => -Height; }
        public int TextureWidth { get => Width ; }        
        public int TextureHeight { get => Height*2 ; }
        public string TextureName { get=> "papers"; }
        public Color TextureColor { get; set; }
        public bool Disabled { get; set; }

        public Paper()
        {
            Width = 100;
            Height = 100;

            CollisionHandler =
                new CollisionHandlerGroup(
                    new StopsWhenBotCollidingWith<IPlayerMovementBlocker>(this)
                );
            UpdateHandler = new UpdateGroup(
               new AffectedByGravity(this)
               , new LimitSpeed(this, 10, 8)
           );
        }

        public void Update()
        {
            UpdateHandler.Update();
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
