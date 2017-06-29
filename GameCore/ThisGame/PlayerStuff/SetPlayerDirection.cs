using GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperWork
{
    public class SetPlayerDirection : SomethingThatHandleUpdates
    {
        private readonly Player Player;

        public SetPlayerDirection(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.Inputs.RightDown)
                Player.FacingRight = true;
            if (Player.Inputs.LeftDown)
                Player.FacingRight = false;
        }
    }
}
