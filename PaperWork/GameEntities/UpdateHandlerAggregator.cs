﻿using GameCore;

namespace PaperWork
{
    public class UpdateHandlerAggregator : IHandleUpdates
    {
        IHandleUpdates[] updates;

        public UpdateHandlerAggregator(params IHandleUpdates[] updates)
        {
            this.updates = updates;
        }

        public void Update()
        {
            foreach (var item in updates)
            {
                item.Update();
            }
        }
    }
}

