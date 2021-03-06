﻿using System;

namespace GameCore
{
    public static class IColliderExtensions
    {
        public static CollisionResult IsColliding(this Collider A, Collider B)
        {
            var w = 0.5f * (A.Width + B.Width);
            var h = 0.5f * (A.Height + B.Height);
            var dx = A.CenterX() - B.CenterX();
            var dy = A.CenterY() - B.CenterY();

            if (Math.Abs(dx) <= w && Math.Abs(dy) <= h)
            {
                /* collision! */
                var wy = w * dy;
                var hx = h * dx;

                if (wy > hx)
                {
                    if (wy > -hx)
                    {
                        /* collision at the top */
                        return CollisionResult.Top;
                    }
                    else
                    {
                        /* on the left */
                        return CollisionResult.Right;
                    }
                }
                else if (wy > -hx)
                {
                    /* on the right */
                    return CollisionResult.Left;
                }
                else
                {
                    /* at the bottom */
                    return CollisionResult.Bottom;
                }
            }

            return CollisionResult.Nope;
        }

        public static int Left(this DimensionalThing a)
        {
            return a.X;
        }

        public static int Right(this DimensionalThing a)
        {
            return a.X + a.Width;
        }

        public static int Top(this DimensionalThing a)
        {
            return a.Y;
        }

        public static int Bottom(this DimensionalThing a)
        {
            return a.Y + a.Height;
        }

        public static float CenterX(this DimensionalThing collider)
        {
            return (collider.Left() + collider.Right()) * 0.5f;
        }

        public static float CenterY(this DimensionalThing collider)
        {
            return (collider.Top() + collider.Bottom()) * 0.5f;
        }

        public static void MoveHorizontally(this Collider a)
        {
            a.X += a.HorizontalSpeed;
        }

        public static void MoveVertically(this Collider a)
        {
            a.Y += a.VerticalSpeed;
        }

        public static void HandleHorizontalCollision(
            this Collider a
            , Collider b)
        {
            if (a.Disabled || b.Disabled)
                return;

            var collision = a.IsColliding(b);

            if (collision == CollisionResult.Nope)
                return;

            if (collision == CollisionResult.Left)
            {
                if (a is ICollisionHandler)
                    a.As<ICollisionHandler>().LeftCollision(b);

                if (b is ICollisionHandler)
                    b.As<ICollisionHandler>().RightCollision(a);
            }
            else if (collision == CollisionResult.Right)
            {
                if (a is ICollisionHandler)
                    a.As<ICollisionHandler>().RightCollision(b);

                if (b is ICollisionHandler)
                    b.As<ICollisionHandler>().LeftCollision(a);
            }
        }

        public static void HandleVerticalCollision(
            this Collider a
            , Collider b)
        {
            if (a.Disabled || b.Disabled)
                return;

            var collision = a.IsColliding(b);

            if (collision == CollisionResult.Nope)
                return;

            if (collision == CollisionResult.Top)
            {
                if (a is ICollisionHandler)
                    a.As<ICollisionHandler>().TopCollision(b);

                if (b is ICollisionHandler)
                    b.As<ICollisionHandler>().BotCollision(a);
            }
            else if (collision == CollisionResult.Bottom)
            {
                if (a is ICollisionHandler)
                    a.As<ICollisionHandler>().BotCollision(b);

                if (b is ICollisionHandler)
                    b.As<ICollisionHandler>().TopCollision(a);
            }
        }
    }
}
