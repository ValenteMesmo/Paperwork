using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class World
    {
        public const int SPACE_BETWEEN_THINGS = 4;
        private List<Thing> Items = new List<Thing>();
        public readonly InputRepository PlayerInputs = new InputRepository();
        private readonly Camera2d Camera2d;
        public bool Stopped { get; set; }
        public int Sleep { get; set; }

        public World(Camera2d Camera2d)
        {
            this.Camera2d = Camera2d;
        }

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
            return currentItems;
        }

        private List<Touchable> PreviouslyTouched = new List<Touchable>();
        private List<Touchable> CurrentlyTouched = new List<Touchable>();
        private List<Thing> currentItems;

        public void Update()
        {
            if (Stopped)
                return;

            if (Sleep > 0)
            {
                Sleep--;
                return;
            }

            var state = Keyboard.GetState();
            //if (state.IsKeyDown(Keys.Escape))
            //    Exit();

            PlayerInputs.Update(state);

            currentItems = Items.ToList();

            if (Stopped)
                return;

            TouchCollection touchCollection = TouchPanel.GetState();
            var touches = new List<Vector2>();
            foreach (TouchLocation tl in touchCollection)
            {
                if ((tl.State == TouchLocationState.Pressed)
                    || (tl.State == TouchLocationState.Moved))
                {
                    touches.Add(
                        Camera2d.ToWorldLocation(tl.Position));
                }
            }

            PreviouslyTouched.Clear();
            PreviouslyTouched.AddRange(CurrentlyTouched);
            CurrentlyTouched.Clear();

            foreach (var item in currentItems)
            {
                if (item is DimensionalThing)
                {
                    var dimensions = item as DimensionalThing;
                    dimensions.DrawableX = dimensions.X;
                    dimensions.DrawableY = dimensions.Y;

                    if (item is Touchable)
                        HandleTouchable(touches, dimensions);
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
        }
    }
}
