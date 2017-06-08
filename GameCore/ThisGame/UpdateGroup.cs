using GameCore;

namespace PaperWork
{
    public class UpdateGroup : IUpdateHandler
    {
        private readonly IUpdateHandler[] Updates;

        public UpdateGroup(params IUpdateHandler[] Updates)
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
