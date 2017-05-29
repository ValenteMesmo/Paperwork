using GameCore.Extensions;
using System.Collections.Generic;

namespace GameCore.Collision
{
    public class CollisionDetector
    {
        public void DetectCollisions(IList<Entity> entities)
        {
            entities.ForEachCombination(CollideEntitiesHorizontally);
            entities.ForEachCombination(CollideEntitiesVertically);
        }

        private void CollideEntitiesHorizontally(
            Entity a,
            Entity b)
        {
            a.GetColliders().ForEachCombination(
                b.GetColliders(),
                CollideCollidersHorizontally);
        }

        private void CollideEntitiesVertically(
            Entity a,
            Entity b)
        {
            a.GetColliders().ForEachCombination(
                b.GetColliders(),
                CollideCollidersVertically);
        }

        private static void CollideCollidersVertically(
            Collider a,
            Collider b)
        {

            if (a == null || a.Disabled)
            {
                return;
            }

            if (b == null || b.Disabled)
            {
                return;
            }

            var rightPoint_a = a.ColliderPosition.X + a.Width;
            var rightPoint_b = b.ColliderPosition.X + b.Width;
            var botPoint_a = a.ColliderPosition.Y + a.Height;
            var botPoint_b = b.ColliderPosition.Y + b.Height;

            if (rightPoint_a < b.ColliderPosition.X
            || rightPoint_b < a.ColliderPosition.X
            || botPoint_a < b.ColliderPosition.Y
            || botPoint_b < a.ColliderPosition.Y)
            {
                return;
            }
            else
            {
                var bot_b__top_a__difference = botPoint_b - a.ColliderPosition.Y;
                var bot_a__top_b__difference = botPoint_a - b.ColliderPosition.Y;
                var right_a__left_b__difference = rightPoint_a - b.ColliderPosition.X;
                var right_b__left_a__difference = rightPoint_b - a.ColliderPosition.X;

                if (bot_a__top_b__difference < bot_b__top_a__difference
                    && bot_a__top_b__difference < right_a__left_b__difference
                    && bot_a__top_b__difference < right_b__left_a__difference)
                {
                    a.HandleBotCollision(b, bot_a__top_b__difference);
                    b.HandleTopCollision(a, bot_a__top_b__difference);
                }

                if (bot_b__top_a__difference < bot_a__top_b__difference
                    && bot_b__top_a__difference < right_a__left_b__difference
                    && bot_b__top_a__difference < right_b__left_a__difference)
                {
                    a.HandleTopCollision(b, bot_b__top_a__difference);
                    b.HandleBotCollision(a, bot_b__top_a__difference);
                }
            }
        }

        private static void CollideCollidersHorizontally(
            Collider a,
            Collider b)
        {

            if (a == null || a.Disabled)
            {
                return;
            }

            if (b == null || b.Disabled)
            {
                return;
            }

            var rightPoint_a = a.ColliderPosition.X + a.Width;
            var rightPoint_b = b.ColliderPosition.X + b.Width;
            var botPoint_a = a.ColliderPosition.Y + a.Height;
            var botPoint_b = b.ColliderPosition.Y + b.Height;

            if (rightPoint_a < b.ColliderPosition.X
            || rightPoint_b < a.ColliderPosition.X
            || botPoint_a < b.ColliderPosition.Y
            || botPoint_b < a.ColliderPosition.Y)
            {
                return;
            }
            else
            {
                var bot_b__top_a__difference = botPoint_b - a.ColliderPosition.Y;
                var bot_a__top_b__difference = botPoint_a - b.ColliderPosition.Y;
                var right_a__left_b__difference = rightPoint_a - b.ColliderPosition.X;
                var right_b__left_a__difference = rightPoint_b - a.ColliderPosition.X;

                if (right_a__left_b__difference < right_b__left_a__difference
                    && right_a__left_b__difference < bot_a__top_b__difference
                    && right_a__left_b__difference < bot_b__top_a__difference)
                {
                    a.HandleRightCollision(b, right_a__left_b__difference);
                    b.HandleLeftCollision(a, right_a__left_b__difference);
                }

                if (right_b__left_a__difference < right_a__left_b__difference
                    && right_b__left_a__difference < bot_a__top_b__difference
                    && right_b__left_a__difference < bot_b__top_a__difference)
                {
                    a.HandleLeftCollision(b, right_b__left_a__difference);
                    b.HandleRightCollision(a, right_b__left_a__difference);
                }
            }
        }
    }
}
