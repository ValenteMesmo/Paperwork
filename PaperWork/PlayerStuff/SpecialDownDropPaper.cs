using GameCore;
using System.Linq;

namespace PaperWork
{
    public class SpecialDownDropPaper : IUpdateHandler
    {
        private readonly Player Player;

        public SpecialDownDropPaper(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper != null
                && Player.Inputs.Action1
                && Player.Inputs.Down
                && Player.TimeUntilDragDropEnable == 0)
            {
                Player.GrabbedPaper.Disabled = false;
                Player.GrabbedPaper.X = MathHelper.RoundUp(
                    Player.X,
                    Player.GrabbedPaper.Width + World.SPACE_BETWEEN_THINGS);
                Player.GrabbedPaper.Y = Player.Bottom() - Player.GrabbedPaper.Height;
                Player.Y -= Player.GrabbedPaper.Height + World.SPACE_BETWEEN_THINGS;

                Player.GrabbedPaper = null;

                Player.TimeUntilDragDropEnable = 50;
            }
        }
    }
}
