using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class World
    {
        public const int SPACE_BETWEEN_THINGS = 4;
        private List<Thing> Items = new List<Thing>();
        public readonly InputRepository PlayerInputs;
        public readonly Camera2d Camera2d;
        public bool Stopped { get; set; }
        public int Sleep { get; set; }
        public int Score { get; set; }

        public World(Camera2d Camera2d)
        {
            this.Camera2d = Camera2d;
            PlayerInputs = new InputRepository(Camera2d);
        }

        public void Add(Thing thing)
        {
            lock (Items)
                Items.Add(thing);
        }

        public void Remove(Thing thing)
        {
            lock (Items)
                Items.Remove(thing);
        }

        public IEnumerable<Thing> GetColliders()
        {
            return MainThreadItems;
        }

        private List<Touchable> PreviouslyTouched = new List<Touchable>();
        private List<Touchable> CurrentlyTouched = new List<Touchable>();
        private List<Thing> MainThreadItems;

        public void Update()
        {
            lock (Items)
            {
                //Destination array was not long enough. Check destIndex and length, and the array's lower bounds.'
                MainThreadItems = Items.ToList();
            }

            if (Stopped)
                return;

            PlayerInputs.Update();
            List<Vector2> touches = GetTouches();

            foreach (var item in MainThreadItems)
            {
                if (item is DimensionalThing)
                {
                    var dimensions = item as DimensionalThing;
                    dimensions.DrawableX = dimensions.X;
                    dimensions.DrawableY = dimensions.Y;

                    if (item is Touchable)
                        HandleTouchable(touches, dimensions);
                }

            }

            if (Sleep > 0)
            {
                Sleep--;
                return;
            }

            foreach (var item in MainThreadItems)
            {
                if (item is IUpdateHandler)
                    item.As<IUpdateHandler>().Update();
            }

            foreach (var item in MainThreadItems)
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

            IList<Collider> colliders;
            lock (Items)
                colliders = Items.OfType<Collider>().Where(f=> f.Disabled == false)
                    .ToList();

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

        private List<Vector2> GetTouches()
        {
            var touches = PlayerInputs.GetTouches();

            PreviouslyTouched.Clear();
            PreviouslyTouched.AddRange(CurrentlyTouched);
            CurrentlyTouched.Clear();
            return touches;
        }

        private void HandleTouchable(List<Vector2> touches, DimensionalThing item)
        {
            var touchable = item.As<Touchable>();
            foreach (var touch in touches)
            {
                if (item.Left() <= touch.X
                    && item.Right() >= touch.X
                    && item.Top() <= touch.Y
                    && item.Bottom() >= touch.Y)
                {
                    if (CurrentlyTouched.Contains(touchable) == false)
                    {
                        CurrentlyTouched.Add(touchable);
                    }
                }
            }

            if (PreviouslyTouched.Contains(touchable) == false
                && CurrentlyTouched.Contains(touchable) == true)
            {
                touchable.TouchBegin();
            }
            else if (PreviouslyTouched.Contains(touchable) == true
                && CurrentlyTouched.Contains(touchable) == true)
            {
                touchable.TouchContinue();
            }
            else if (PreviouslyTouched.Contains(touchable) == true
                && CurrentlyTouched.Contains(touchable) == false)
            {
                touchable.TouchEnded();
            }
        }

        public void Clear()
        {
            Items.Clear();
            Score = 0;
        }
    }
}
