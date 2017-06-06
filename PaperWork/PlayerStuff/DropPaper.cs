using GameCore;
using System.Linq;

namespace PaperWork
{
    public class DropPaper : IUpdateHandler
    {
        private readonly Player Player;

        public DropPaper(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper != null
                && Player.Inputs.Action1
                && Player.ChestPaperDetetor.GetDetectedItems().Any() == false
                && Player.TimeUntilDragDropEnable == 0)
            {
                Player.GrabbedPaper.Disabled = false;
                Player.GrabbedPaper.X = MathHelper.RoundUp(
                    Player.Right(),
                    Player.GrabbedPaper.Width + World.SPACE_BETWEEN_THINGS);
                Player.GrabbedPaper = null;

                Player.TimeUntilDragDropEnable = Player.DRAG_AND_DROP_COOLDOWN;
            }
        }
    }
}
