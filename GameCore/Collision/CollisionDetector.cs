using GameCore.Extensions;
using System.Collections.Generic;
using System;

namespace GameCore.Collision
{
    public class CollisionDetector
    {
        public void DetectCollisions(IList<Entity> entities)
        {
            foreach (var entity in entities)
            {
                foreach (var collider in entity.GetColliders())
                {
                    collider.TopCollisions.Clear();
                    collider.BotCollisions.Clear();
                    collider.LeftCollisions.Clear();
                    collider.RightCollisions.Clear();
                }
            }

            entities.ForEachCombination(CollideTwoGameParts);
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

            if (a.IsTrigger && b.IsTrigger)
                return;

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
                    if (b.IsTrigger == false)
                        a.BotCollisions.Add(new Collision(b, bot_a__top_b__difference));
                    if (a.IsTrigger == false)
                        b.TopCollisions.Add(new Collision(a, bot_a__top_b__difference));
                }

                if (bot_b__top_a__difference < bot_a__top_b__difference
                    && bot_b__top_a__difference < right_a__left_b__difference
                    && bot_b__top_a__difference < right_b__left_a__difference)
                {
                    if (b.IsTrigger == false)
                        a.TopCollisions.Add(new Collision(b, bot_b__top_a__difference));
                    if (a.IsTrigger == false)
                        b.BotCollisions.Add(new Collision(a, bot_b__top_a__difference));
                }

                if (right_a__left_b__difference < right_b__left_a__difference
                    && right_a__left_b__difference < bot_a__top_b__difference
                    && right_a__left_b__difference < bot_b__top_a__difference)
                {
                    if (b.IsTrigger == false)
                        a.RightCollisions.Add(new Collision(b, right_a__left_b__difference));
                    if (a.IsTrigger == false)
                        b.LeftCollisions.Add(new Collision(a, right_a__left_b__difference));
                }

                if (right_b__left_a__difference < right_a__left_b__difference
                    && right_b__left_a__difference < bot_a__top_b__difference
                    && right_b__left_a__difference < bot_b__top_a__difference)
                {
                    if (b.IsTrigger == false)
                        a.LeftCollisions.Add(new Collision(b, right_b__left_a__difference));
                    if (a.IsTrigger == false)
                        b.RightCollisions.Add(new Collision(a, right_b__left_a__difference));
                }
            }
        }
    }
}
