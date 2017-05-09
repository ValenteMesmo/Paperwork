using System;

namespace GameCore
{
    internal class GameLoop
    {
        private double accumulator = 0.0;
        private Action BeforeUpdate;
        private Action OnUpdate;
        private Action<float> AfterUpdate;

        public GameLoop(
            Action BeforeUpdate,
            Action OnUpdate,
            Action<float> AfterUpdate)
        {
            this.BeforeUpdate = BeforeUpdate;
            this.OnUpdate = OnUpdate;
            this.AfterUpdate = AfterUpdate;
        }

        public void DoIt(DateTime previous, DateTime current)
        {
            var timeSinceLastUpdate =
                (float)(current - previous).TotalSeconds;

            if (timeSinceLastUpdate > 0.25f)
                timeSinceLastUpdate = 0.25f;

            accumulator += timeSinceLastUpdate;
            BeforeUpdate();

            while (accumulator >= 0.01)
            {
                OnUpdate();

                accumulator -= 0.01;
            }

            AfterUpdate(timeSinceLastUpdate);
        }
    }
}