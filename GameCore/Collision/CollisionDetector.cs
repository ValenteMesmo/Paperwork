using GameCore.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.Collision
{
    public class CollisionDetector
    {
        public void DetectCollisions(IList<Entity> entities)
        {
            foreach (var item in entities)
            {
                item.Position = new Coordinate2D(
                    item.Position.X + item.Speed.X
                    , item.Position.Y);
            }

            entities.ForEachCombination(CollideEntitiesHorizontally);

            foreach (var item in entities)
            {
                item.Position = new Coordinate2D(
                    item.Position.X
                    , item.Position.Y + item.Speed.Y);
            }

            entities.ForEachCombination(CollideEntitiesVertically);

            foreach (var item in entities.SelectMany(f => f.GetColliders()))
            {
                item.Update();
            }
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
                return;

            if (b == null || b.Disabled)
                return;

            if (a is Trigger && b is Trigger)
                return;

            var a_max_x = a.ColliderPosition.X + a.Width;
            var a_min_x = a.ColliderPosition.X;
            var a_max_y = a.ColliderPosition.Y + a.Height;
            var a_min_y = a.ColliderPosition.Y;

            var b_max_x = b.ColliderPosition.X + b.Width;
            var b_min_x = b.ColliderPosition.X;
            var b_max_y = b.ColliderPosition.Y + b.Height;
            var b_min_y = b.ColliderPosition.Y;

            if (a_max_x <= b_min_x || a_min_x >= b_max_x)
                return;

            if (a_max_y <= b_min_y || a_min_y >= b_max_y)
                return;

            if (a_min_y > b_min_y)
            {
                if (b is Trigger == false)
                    a.HandleTopCollision(b, 0);
                if (a is Trigger == false)
                    b.HandleBotCollision(a, 0);
            }
            else
            {
                if (b is Trigger == false)
                    a.HandleBotCollision(b, 0);
                if (a is Trigger == false)
                    b.HandleTopCollision(a, 0);
            }

        }

        private static void CollideCollidersHorizontally(
            Collider a,
            Collider b)
        {

            if (a == null || a.Disabled)
                return;

            if (b == null || b.Disabled)
                return;

            if (a is Trigger && b is Trigger)
                return;

            var a_max_x = a.ColliderPosition.X + a.Width;
            var a_min_x = a.ColliderPosition.X;
            var a_max_y = a.ColliderPosition.Y + a.Height;
            var a_min_y = a.ColliderPosition.Y;

            var b_max_x = b.ColliderPosition.X + b.Width;
            var b_min_x = b.ColliderPosition.X;
            var b_max_y = b.ColliderPosition.Y + b.Height;
            var b_min_y = b.ColliderPosition.Y;

            if (a_max_x <= b_min_x || a_min_x >= b_max_x)
                return;

            if (a_max_y <= b_min_y || a_min_y >= b_max_y)
                return;

            if (a_min_x < b_min_x )
            {
                a.HandleRightCollision(b, 0);
                b.HandleLeftCollision(a, 0);
            }
            else
            {
                a.HandleLeftCollision(b, 0);
                b.HandleRightCollision(a, 0);
            }
        }
    }
}
