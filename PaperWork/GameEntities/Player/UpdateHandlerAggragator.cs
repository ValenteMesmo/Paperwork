using GameCore;

namespace PaperWork
{
    public class UpdateHandlerAggragator : IHandleUpdates
    {
        IHandleUpdates[] updates;

        public UpdateHandlerAggragator(params IHandleUpdates[] updates)
        {
            this.updates = updates;
        }

        public void Update(Entity entity)
        {
            foreach (var item in updates)
            {
                item.Update(entity);
            }
        }
    }
}

