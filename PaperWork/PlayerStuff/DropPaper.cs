using GameCore;
using System;
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
                && Player.TimeUntilDragDropEnable == 0)
            {
                Player.GrabbedPaper.Disabled = false;
                Player.GrabbedPaper.X = MathHelper.RoundUp(
                    Player.Right() ,
                    Player.GrabbedPaper.Width + World.SPACE_BETWEEN_THINGS);
                Player.GrabbedPaper = null;

                Player.TimeUntilDragDropEnable = 50;
            }
        }
    }

    static class MathHelper
 {		
     public static int RoundUp(float numToRound, int multiple)
     {		
         return (int) Math.Round(numToRound / multiple) * multiple;		
     }		
 }
}
