using GameCore;
using System;

namespace PaperWork
{
    public class ChangeStateAfterUpdateCount : IHandleUpdates
    {
        private int currentCount = 0;
        private readonly int updateCount;
        private readonly Action ChangeState;

        public ChangeStateAfterUpdateCount(
            Action ChangeState
            , int updateCount)
        {
            this.updateCount = updateCount;
            this.ChangeState = ChangeState;
        }

        public void Update()
        {
            currentCount++;
            if (currentCount >= updateCount)
            {
                currentCount = 0;
                ChangeState();
            }
        }
    }
}
