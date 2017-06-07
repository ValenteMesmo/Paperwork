using GameCore;
using System.Linq;

namespace PaperWork
{
    public class GrabPaperNearPlayersFeetAsLastOption_Right : IUpdateHandler
    {
        private readonly Player Player;

        public GrabPaperNearPlayersFeetAsLastOption_Right(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper == null
                && Player.Inputs.Action1
                && Player.FacingRight
                && Player.TimeUntilDragDropEnable == 0)
            {
                var papers = Player.Right_FeetPaperDetector.GetDetectedItems();
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
