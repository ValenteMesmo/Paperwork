using GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperWork
{
    public class SetPlayerDirection : IUpdateHandler
    {
        private readonly Player Player;

        public SetPlayerDirection(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.Inputs.Right)
                Player.FacingRight = true;
            if (Player.Inputs.Left)
                Player.FacingRight = false;
        }
    }
}
