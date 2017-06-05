using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class World
    {
        public const int SPACE_BETWEEN_THINGS = 2;
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
            foreach (var item in Items.ToList())
            {
                if (item is IUpdateHandler)
                    item.As<IUpdateHandler>().Update();

                if (item is IAfterUpdateHandler)
                    item.As<IAfterUpdateHandler>().AfterUpdate();

                item.MoveHorizontally();
            }

            //TODO: QuadTree
            //https://github.com/ChevyRay/QuadTree
            //https://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374

            Items.ForEachCombination(
                IColliderExtensions
                    .HandleHorizontalCollision);

            foreach (var item in Items)
            {
                item.MoveVertically();
            }

            Items.ForEachCombination(
                IColliderExtensions
                    .HandleVerticalCollision);
        }
    }
}
