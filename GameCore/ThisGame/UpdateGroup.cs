using GameCore;

namespace PaperWork
{
    public class UpdateGroup : SomethingThatHandleUpdates
    {
        private readonly SomethingThatHandleUpdates[] Updates;

        public UpdateGroup(params SomethingThatHandleUpdates[] Updates)
        {
            this.Updates = Updates;
        }

        public void Update()
        {
            foreach (var item in Updates)
            {
                item.Update();
            }
        }
    }
}
