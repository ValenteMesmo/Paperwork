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

        public bool ClickedLeft { get; private set; }
        public bool ClickedRight { get; private set; }
        public bool ClickedUp { get; private set; }
        public bool ClickedDown { get; private set; }
        public bool ClickedAction1 { get; private set; }
        public bool ClickedJump { get; private set; }

        private bool WasPressedLeft { get; set; }
        private bool WasPressedRight { get; set; }
        private bool WasPressedUp { get; set; }
        private bool WasPressedDown { get; set; }
        private bool WasPressedAction1 { get; set; }
        private bool WasPressedJump { get; set; }

        public bool LeftDown { get; set; }
        public bool RightDown { get; set; }
        public bool UpDown { get; set; }
        public bool DownDown { get; set; }
        public bool Action1Down { get; set; }
        public bool JumpDown { get; set; }

        public InputRepository(Camera2d Camera2d)
        {
            this.Camera2d = Camera2d;
        }

        public void Update()
        {
            ClickedLeft = !WasPressedLeft && LeftDown;
            ClickedRight = !WasPressedRight && RightDown;
            ClickedUp = !WasPressedUp && UpDown;
            ClickedDown = !WasPressedDown && DownDown;
            ClickedAction1 = !WasPressedAction1 && Action1Down;
            ClickedJump = !WasPressedJump && JumpDown;

            WasPressedLeft = LeftDown;
            WasPressedRight = RightDown;
            WasPressedUp = UpDown;
            WasPressedDown = DownDown;
            WasPressedAction1 = Action1Down;
            WasPressedJump = JumpDown;
        }

        internal void SetState(KeyboardState state)
        {
            if (touches.Count == 0)
            {
                LeftDown = state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left);
                RightDown = state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right);
                JumpDown = state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up);
                DownDown = state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down);
                Action1Down = state.IsKeyDown(Keys.J) || state.IsKeyDown(Keys.LeftControl);
                UpDown = state.IsKeyDown(Keys.K) || state.IsKeyDown(Keys.Space);
            }
        }

        List<Vector2> touches = new List<Vector2>();
        public void SetState(TouchCollection touchCollection)
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
