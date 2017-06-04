using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class World
    {
        public const float SPACE_BETWEEN_THINGS = 1;
        private List<ICollider> Items = new List<ICollider>();

        public void AddCollider(ICollider collider)
        {
            Items.Add(collider);
        }

        public IEnumerable<ICollider> GetColliders()
        {
            return Items.ToList();
        }

        public void Update()
        {
            foreach (var item in Items)
            {
                if (item is IUpdateHandler)
                    item.As<IUpdateHandler>().Update();

                if (item is IBeforeCollisionHandler)
                    item.As<IBeforeCollisionHandler>().BeforeCollision();

                item.MoveHorizontally();
            }

            //TODO: QuadTree
            //https://github.com/ChevyRay/QuadTree
            //https://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374

            Items.ForEachCombination(IColliderExtensions.HandleHorizontalCollision);

            foreach (var item in Items)
            {
                item.MoveVertically();
            }

            Items.ForEachCombination(IColliderExtensions.HandleVerticalCollision);
        }
    }
}
