using GameCore;
using System;

namespace PaperWork
{
    public class PlayersJump : IUpdateHandler
    {
        private readonly Player Player;
        private readonly InputRepository Input;
        private readonly Action<string> PlayAudio;

        public PlayersJump(Player Player, InputRepository Input , Action<string> PlayAudio)
        {
            this.Player = Player;
            this.Input = Input;
            this.PlayAudio = PlayAudio;
        }

        public void Update()
        {
            if (Player.Grounded && Input.ClickedJump)
            {
                Player.VerticalSpeed = -100;
                PlayAudio("jump");
            }
        }
    }
}
