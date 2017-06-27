using GameCore;

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
                && Player.Inputs.Action1Down
                && Player.Inputs.DownDown
                && Player.TimeUntilDragDropEnable == 0
                && Player.Y > (Paper.SIZE + World.SPACE_BETWEEN_THINGS) *2.2f)
            {
                Player.HorizontalSpeed = 0;
                Player.VerticalSpeed = 0;

                Player.GrabbedPaper.Disabled = false;
                Player.GrabbedPaper.X = MathHelper.RoundUp(
                    Player.X,
                    Player.GrabbedPaper.Width + World.SPACE_BETWEEN_THINGS);
                Player.GrabbedPaper.Y = Player.Bottom() - Player.GrabbedPaper.Height;
                Player.Y -= Player.GrabbedPaper.Height + World.SPACE_BETWEEN_THINGS;

                Player.GrabbedPaper = null;

                Player.TimeUntilDragDropEnable = Player.DRAG_AND_DROP_COOLDOWN;
                Player.AudiosToPlay.Add("grab");
            }
        }
    }
}
