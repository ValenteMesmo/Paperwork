using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GameCore
{
    public class World
    {
        public const int SPACE_BETWEEN_THINGS = 4;
        private List<Thing> Items = new List<Thing>();
        public readonly InputRepository PlayerInputs = new InputRepository();
        private readonly Camera2d Camera2d;

        public World(Camera2d Camera2d)
        {
            this.Camera2d = Camera2d;
            var btnWidth = 1000;
            var btnHeight = 900;
            var space = 1;
            var yAnchor = 6600;
            var xAnchor = 300;

            Add(new TouchButton(xAnchor, yAnchor, btnWidth - space, btnHeight - space, f => PlayerInputs.Up = PlayerInputs.Left = f));
            Add(new TouchButton(xAnchor + btnWidth, yAnchor, btnWidth - space, btnHeight - space, f => PlayerInputs.Up = f));
            Add(new TouchButton(xAnchor + btnWidth * 2, yAnchor, btnWidth - space, btnHeight - space, f => PlayerInputs.Up = PlayerInputs.Right = f));

            Add(new TouchButton(xAnchor, yAnchor + btnHeight, btnWidth + btnWidth / 2 - space, btnHeight - space, f => PlayerInputs.Left = f));
            Add(new TouchButton(xAnchor + btnWidth + btnWidth / 2, yAnchor + btnHeight, btnWidth + btnWidth / 2 - space, btnHeight - space, f => PlayerInputs.Right = f));

            Add(new TouchButton(xAnchor, yAnchor + btnHeight * 2, btnWidth - space, btnHeight - space, f => PlayerInputs.Down = PlayerInputs.Left = f));
            Add(new TouchButton(xAnchor + btnWidth, yAnchor + btnHeight * 2, btnWidth - space, btnHeight - space, f => PlayerInputs.Down = f));
            Add(new TouchButton(xAnchor + btnWidth * 2, yAnchor + btnHeight * 2, btnWidth - space, btnHeight - space, f => PlayerInputs.Down = PlayerInputs.Right = f));

            xAnchor = 9600;
            btnWidth = 2000;
            Add(new TouchButton(
                xAnchor
                , yAnchor
                , btnWidth - space
                , (int)(btnHeight * 3f) - space
                , f => PlayerInputs.Action1 = f));
            Add(new TouchButton(
                xAnchor + btnWidth
                , yAnchor
                , btnWidth - space
                , (int)(btnHeight * 3f) - space
                , f => PlayerInputs.Up = f));
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
            //todo: REMOVE THIS TRY CATCH
            try
            {
                return Items.ToList();
            }
            catch
            {
                return Enumerable.Empty<Thing>();
            }
        }

        private List<Touchable> PreviouslyTouched = new List<Touchable>();
        private List<Touchable> CurrentlyTouched = new List<Touchable>();

        public void Update()
        {
            //erro quando reseta o game
            try
            {
                var state = Keyboard.GetState();
                //if (state.IsKeyDown(Keys.Escape))
                //    Exit();

                PlayerInputs.Update(state);

            }
            catch (Exception ex)
            {
            }

            var currentItems = Items.ToList();
            //null reference when game closed
            try
            {
                //check if game is running
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

            }
            catch (NullReferenceException ex)
            {
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
