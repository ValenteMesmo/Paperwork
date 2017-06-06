using GameCore;
using System.Linq;

namespace PaperWork
{
    public class GrabPaperNearPlayersFeet : IUpdateHandler
    {
        private readonly Player Player;

        public GrabPaperNearPlayersFeet(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper == null
                && Player.Inputs.Action1
                && Player.TimeUntilDragDropEnable == 0)
            {
                var papers = Player.FeetPaperDetector.GetDetectedItems();
                if (papers.Any())
                {
                    Player.GrabbedPaper = papers.First();
                    Player.GrabbedPaper.Disabled = true;
                    Player.TimeUntilDragDropEnable = 50;
                }
            }
        }
    }
}
