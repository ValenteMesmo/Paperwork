using GameCore;
using System.Linq;

namespace PaperWork
{
    public class GrabPaperThatThePlayerIsStandingOn : IUpdateHandler
    {
        private readonly Player Player;

        public GrabPaperThatThePlayerIsStandingOn(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper == null
                && Player.Inputs.Action1
                && Player.Inputs.Down
                && Player.Inputs.Left == false
                && Player.Inputs.Right == false
                && Player.TimeUntilDragDropEnable == 0)
            {
                var papers = Player.GroundDetector.GetDetectedItems().OfType<Paper>();
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
