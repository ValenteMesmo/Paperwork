using GameCore;
using System.Linq;

namespace PaperWork
{
    public class DropPaper_Left : IUpdateHandler
    {
        private readonly Player Player;

        public DropPaper_Left(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper != null
                && Player.Inputs.Action1
                && Player.FacingRight == false
                && Player.Left_ChestPaperDetetor.GetDetectedItems().Any() == false
                && Player.TimeUntilDragDropEnable == 0)
            {
                Player.GrabbedPaper.Disabled = false;
                Player.GrabbedPaper.X = MathHelper.RoundUp(
                    Player.Left() - Player.Width,
                    Player.GrabbedPaper.Width + World.SPACE_BETWEEN_THINGS);
                Player.GrabbedPaper = null;

                Player.TimeUntilDragDropEnable = Player.DRAG_AND_DROP_COOLDOWN;
            }
        }
    }
}
