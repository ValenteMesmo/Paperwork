using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class World
    {
        public const int SPACE_BETWEEN_THINGS = 1;
        private List<Thing> Items = new List<Thing>();

        public void Add(Thing thing)
        {
            Items.Add(thing);
        }

        public void Remove(Thing thing)
        {
            Items.Remove(thing);
        }

        public IEnumerable<Thing> GetColliders()
        {
            lock (Items)
                return Items.ToList();
        }

        public void Update()
        {
            var currentItems = Items.ToList();

            foreach (var item in currentItems)
            {
                if (item is DimensionalThing)
                {
                    var dimensions = item as DimensionalThing;
                    dimensions.DrawableX = dimensions.X;
                    dimensions.DrawableY = dimensions.Y;
                }

                if (item is IUpdateHandler)
                    item.As<IUpdateHandler>().Update();
            }

            foreach (var item in currentItems)
            {
                if (item is IAfterUpdateHandler)
                    item.As<IAfterUpdateHandler>().AfterUpdate();

                if (item is Collider)
                {
                    var collider = item as Collider;
                    collider.MoveHorizontally();
                }
            }

            //TODO: QuadTree
            //https://github.com/ChevyRay/QuadTree
            //https://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374

            var colliders = currentItems.OfType<Collider>().ToList();
            colliders.ForEachCombination(
                IColliderExtensions
                    .HandleHorizontalCollision);

            foreach (var item in colliders)
            {
                item.MoveVertically();
            }

            colliders.ForEachCombination(
                IColliderExtensions
                    .HandleVerticalCollision);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}
