using GameCore;
using System.Linq;

namespace PaperWork
{
    public class GrabPaperNearPlayersChest : IUpdateHandler
    {
        private readonly Player Player;

        public GrabPaperNearPlayersChest(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper == null
                && Player.Inputs.Action1
                && Player.TimeUntilDragDropEnable == 0)
            {
                var papers = Player.ChestPaperDetetor.GetDetectedItems();
                if (papers.Any())
                {
                    Player.GrabbedPaper = papers.First();
                    Player.GrabbedPaper.Disabled = true;
                    Player.TimeUntilDragDropEnable = Player.DRAG_AND_DROP_COOLDOWN;
                }
            }
        }
    }
}
