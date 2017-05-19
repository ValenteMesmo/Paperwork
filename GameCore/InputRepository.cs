using Microsoft.Xna.Framework.Input;

namespace GameCore
{
    public enum KeyState
    {
        Released,
        JustPressed,
        Pressed,
        JustReleased
    }

    public class InputRepository
    {
        private KeyState Left;
        private KeyState Right;
        private KeyState Up;
        private KeyState Down;
        private KeyState Action1;

        internal void Update(KeyboardState state)
        {
            Left = GetState(Left, state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left));
            Right = GetState(Right, state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right));
            Up = GetState(Up, state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up));
            Down = GetState(Down, state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down));
            Action1 = GetState(Action1, state.IsKeyDown(Keys.J) || state.IsKeyDown(Keys.LeftControl));
        }

        public bool Action1_JustPressed()
        {
            return Action1 == KeyState.JustPressed;
        }

        public bool Left_Pressed() { return Left == KeyState.Pressed || Left == KeyState.JustPressed; }
        public bool Right_Pressed() { return Right == KeyState.Pressed || Right == KeyState.JustPressed; }
        public bool Up_Pressed() { return Up == KeyState.Pressed || Up == KeyState.JustPressed; }
        public bool Down_Pressed() { return Down == KeyState.Pressed || Down == KeyState.JustPressed; }

        private KeyState GetState(KeyState previous, bool keyDown)
        {
            if (keyDown)
            {
                if (previous == KeyState.Released
                    || previous == KeyState.JustReleased)
                    return KeyState.JustPressed;

                if (previous == KeyState.JustPressed
                    || previous == KeyState.Pressed)
                    return KeyState.Pressed;
            }
            else
            {
                if (previous == KeyState.Released
                    || previous == KeyState.JustReleased)
                    return KeyState.Released;

                if (previous == KeyState.JustPressed
                    || previous == KeyState.Pressed)
                    return KeyState.JustReleased;
            }

            return KeyState.Released;
        }
    }
}
