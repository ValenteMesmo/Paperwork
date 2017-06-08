using System;

namespace GameCore
{
    internal class GameLoop
    {
        private double accumulator = 0.0;
        private Action OnUpdate;

        public GameLoop(Action OnUpdate)
        {
            this.OnUpdate = OnUpdate;
        }

        public void DoIt(DateTime previous, DateTime current)
        {
            var timeSinceLastUpdate =
                (float)(current - previous).TotalSeconds;

            if (timeSinceLastUpdate > 0.25f)
                timeSinceLastUpdate = 0.25f;

            accumulator += timeSinceLastUpdate;

            while (accumulator >= 0.01)
            {
                OnUpdate();

                accumulator -= 0.01;
            }
        }
    }
}
