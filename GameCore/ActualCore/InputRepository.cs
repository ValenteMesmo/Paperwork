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

        internal void SetState(KeyboardState keyboard, GamePadState controller)
        {
            if (touches.Count == 0)
            {
                LeftDown =
                    (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
                    ||
                    (controller.DPad.Left == ButtonState.Pressed || controller.ThumbSticks.Left.X < -0.5f)
                    ;
                RightDown =
                    (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
                    ||
                    (controller.DPad.Right == ButtonState.Pressed || controller.ThumbSticks.Left.X > 0.5f)
                    ;
                JumpDown =
                    (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
                    ||
                    (controller.Buttons.A == ButtonState.Pressed || controller.ThumbSticks.Left.Y > 0.5f);
                DownDown =
                    (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
                    ||
                    (controller.DPad.Down == ButtonState.Pressed || controller.ThumbSticks.Left.Y < -0.5f);
                Action1Down =
                    (keyboard.IsKeyDown(Keys.J) || keyboard.IsKeyDown(Keys.LeftControl))
                    ||
                    (controller.Buttons.X == ButtonState.Pressed || controller.Buttons.B == ButtonState.Pressed);
                UpDown = (keyboard.IsKeyDown(Keys.K) || keyboard.IsKeyDown(Keys.Space))
                    ||
                    (
                        controller.DPad.Up == ButtonState.Pressed 
                        || controller.ThumbSticks.Right.Y < -0.5f
                        || controller.ThumbSticks.Right.Y > 0.5f
                    );
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
