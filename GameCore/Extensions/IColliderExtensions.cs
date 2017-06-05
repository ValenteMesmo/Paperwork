using System;

namespace GameCore
{
    public static class IColliderExtensions
    {
        public static CollisionResult IsColliding(this ICollider A, ICollider B)
        {
            float w = 0.5f * (A.Width + B.Width);
            float h = 0.5f * (A.Height + B.Height);
            float dx = A.CenterX() - B.CenterX();
            float dy = A.CenterY() - B.CenterY();

            if (Math.Abs(dx) <= w && Math.Abs(dy) <= h)
            {
                /* collision! */
                float wy = w * dy;
                float hx = h * dx;

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

        public static float Left(this ICollider a)
        {
            return a.X;
        }

        public static float Right(this ICollider a)
        {
            return a.X + a.Width;
        }

        public static float Top(this ICollider a)
        {
            return a.Y;
        }

        public static float Bottom(this ICollider a)
        {
            return a.Y + a.Height;
        }

        public static float CenterX(this ICollider collider)
        {
            return (collider.Left() + collider.Right()) * 0.5f;
        }

        public static float CenterY(this ICollider collider)
        {
            return (collider.Top() + collider.Bottom()) * 0.5f;
        }

        public static void MoveHorizontally(this ICollider a)
        {
            a.X += a.HorizontalSpeed;
        }

        public static void MoveVertically(this ICollider a)
        {
            a.Y += a.VerticalSpeed;
        }

        public static void HandleHorizontalCollision(
            this ICollider a
            , ICollider b)
        {
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

        public static int RoundX(this ICollider collider)
        {
            if (collider.HorizontalSpeed > 0)
                return (int)Math.Floor(collider.X);

            if (collider.HorizontalSpeed < 0)
                return (int)Math.Ceiling(collider.X);

            return (int)collider.X;
        }

        public static void HandleVerticalCollision(
            this ICollider a
            , ICollider b)
        {
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
