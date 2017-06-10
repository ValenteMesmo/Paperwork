using GameCore;

namespace PaperWork
{
    public class PlayersJump : IUpdateHandler
    {
        private readonly Player Player;
        private readonly InputRepository Input;

        public PlayersJump(Player Player, InputRepository Input)
        {
            this.Player = Player;
            this.Input = Input;
        }

        public void Update()
        {
            if (Player.Grounded && Input.Up)
            {
                Player.VerticalSpeed = -10;
            }
        }
    }
}
