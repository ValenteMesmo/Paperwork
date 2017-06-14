using Microsoft.Xna.Framework.Input;

namespace GameCore
{
    public class InputRepository
    {
        public bool Left { get;  set; }
        public bool Right { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Action1 { get; set; }

        internal void Update(KeyboardState state)
        {
            Left =  state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left);
            Right = state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right);
            Up =  state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up);
            Down = state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down);
            Action1 = state.IsKeyDown(Keys.J) || state.IsKeyDown(Keys.LeftControl);
        }   
    }
}
