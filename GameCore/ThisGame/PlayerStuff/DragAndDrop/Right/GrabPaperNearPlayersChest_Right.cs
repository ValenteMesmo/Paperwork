using GameCore;
using System.Linq;

namespace PaperWork
{
    public class GrabPaperNearPlayersChest_Right : IUpdateHandler
    {
        private readonly Player Player;

        public GrabPaperNearPlayersChest_Right(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper == null
                && Player.Inputs.ClickedAction1
                && Player.FacingRight
                && Player.TimeUntilDragDropEnable == 0)
            {
                var papers = Player.Right_ChestPaperDetetor.GetDetectedItems().OfType<Paper>();
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
