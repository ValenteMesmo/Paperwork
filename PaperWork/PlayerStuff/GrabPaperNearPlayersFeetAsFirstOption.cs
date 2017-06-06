using GameCore;
using System.Linq;

namespace PaperWork
{
    public class GrabPaperNearPlayersFeetAsFirstOption : IUpdateHandler
    {
        private readonly Player Player;

        public GrabPaperNearPlayersFeetAsFirstOption(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper == null
                && Player.Inputs.Action1
                && Player.Inputs.Right
                && Player.Inputs.Down
                && Player.TimeUntilDragDropEnable == 0)
            {
                var papers = Player.FeetPaperDetector.GetDetectedItems();
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
