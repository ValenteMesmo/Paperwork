using System.Collections.Generic;
using GameCore;
using Microsoft.Xna.Framework;

namespace PaperWork
{
    public class Paper :
        Collider
        , ICollisionHandler
        , IUpdateHandler
        , IPlayerMovementBlocker
        , Animation
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

        public bool Disabled { get; set; }

        private const int SIZE = 1000;

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
                    new StopsWhenBotCollidingWith<IPlayerMovementBlocker>(this)
                    , new StopsWhenRightCollidingWith<IPlayerMovementBlocker>(this)
            );
        }

        public void Update()
        {
            if (
                X >= (1000 * 11) + 12
                && (Y <= 3000 + World.SPACE_BETWEEN_THINGS)
                && VerticalSpeed == 0)
            {
                HorizontalSpeed = -20;
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

        public Color Color { get { return Texture.Color; } set { Texture.Color = value; } }
        private Texture Texture = new Texture("papers", 0, -SIZE, SIZE, SIZE * 2);

        public IEnumerable<Texture> GetTextures()
        {
            yield return Texture;
        }
    }
}
