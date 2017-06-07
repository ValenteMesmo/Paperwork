using GameCore;
using System.Linq;

namespace PaperWork
{
    public class GrabPaperNearPlayersFeetAsLastOption_Left : IUpdateHandler
    {
        private readonly Player Player;

        public GrabPaperNearPlayersFeetAsLastOption_Left(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper == null
                && Player.Inputs.Action1
                && Player.FacingRight ==false
                && Player.TimeUntilDragDropEnable == 0)
            {
                var papers = Player.Left_FeetPaperDetector.GetDetectedItems();
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
