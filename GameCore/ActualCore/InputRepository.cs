using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class InputRepository
    {
        private readonly Camera2d Camera2d;

        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Action1 { get; set; }
        public bool Jump { get; set; }

        public InputRepository(Camera2d Camera2d)
        {
            this.Camera2d = Camera2d;
        }

        internal void Update(KeyboardState state)
        {
            Left = state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left);
            Right = state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right);
            Jump = state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up);
            Down = state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down);
            Action1 = state.IsKeyDown(Keys.J) || state.IsKeyDown(Keys.LeftControl);
            Up = state.IsKeyDown(Keys.K) || state.IsKeyDown(Keys.Space);
        }

        List<Vector2> touches = new List<Vector2>();
        public void Update(TouchCollection touchCollection)
        {
            touches.Clear();
            foreach (TouchLocation tl in touchCollection)
            {
                if ((tl.State == TouchLocationState.Pressed)
                    || (tl.State == TouchLocationState.Moved))
                {
                    touches.Add(
                        Camera2d.ToWorldLocation(tl.Position));
                }
            }
        }

        public List<Vector2> GetTouches()
        {
            return touches.ToList();
        }
    }
}
