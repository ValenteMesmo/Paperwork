using GameCore.Extensions;
using System.Collections.Generic;

namespace GameCore.Collision
{
    public class CollisionDetector
    {
        public void DetectCollisions(IList<Entity> parts)
        {
            parts.ForEachCombination(CollideTwoGameParts);
        }

        private void CollideTwoGameParts(
            Entity a, 
            Entity b)
        {
            a.GetColliders().ForEachCombination(
                b.GetColliders(),
                CollideTwoColliders);
        }

        private static void CollideTwoColliders(
            BaseCollider a,
            BaseCollider b)
        {

            if (a == null || a.Disabled)
            {
                return;
            }

            if (b == null || b.Disabled)
            {
                return;
            }

            if (a is Trigger && b is Trigger)
                return;

            var rightPoint_a = a.Position.X + a.Width;
            var rightPoint_b = b.Position.X + b.Width;
            var topPoint_a = a.Position.Y + a.Height;
            var topPoint_b = b.Position.Y + b.Height;

            if (rightPoint_a < b.Position.X
            || rightPoint_b < a.Position.X
            || topPoint_a < b.Position.Y
            || topPoint_b < a.Position.Y)
                return;
            else
            {
                var top_b__bot_a__difference = topPoint_b - a.Position.Y;
                var top_a__bot_b__difference = topPoint_a - b.Position.Y;
                var right_a__left_b__difference = rightPoint_a - b.Position.X;
                var right_b__left_a__difference = rightPoint_b - a.Position.X;

                if (top_a__bot_b__difference < top_b__bot_a__difference
                    && top_a__bot_b__difference < right_a__left_b__difference
                    && top_a__bot_b__difference < right_b__left_a__difference)
                {
                    a.CollisionFromBelow(b);
                    b.CollisionFromAbove(a);
                    return;
                }

                if (top_b__bot_a__difference < top_a__bot_b__difference
                    && top_b__bot_a__difference < right_a__left_b__difference
                    && top_b__bot_a__difference < right_b__left_a__difference)
                {   
                    b.CollisionFromBelow(a);
                    a.CollisionFromAbove(b);
                    return;
                }

                if (right_a__left_b__difference < right_b__left_a__difference
                    && right_a__left_b__difference < top_a__bot_b__difference
                    && right_a__left_b__difference < top_b__bot_a__difference)
                {
                    a.CollisionFromTheRight(b);
                    b.CollisionFromTheLeft(a);
                    return;
                }

                if (right_b__left_a__difference < right_a__left_b__difference
                    && right_b__left_a__difference < top_a__bot_b__difference
                    && right_b__left_a__difference < top_b__bot_a__difference)
                {
                    b.CollisionFromTheRight(a);
                    a.CollisionFromTheLeft(b);
                    return;
                }
            }
        }
    }
}
