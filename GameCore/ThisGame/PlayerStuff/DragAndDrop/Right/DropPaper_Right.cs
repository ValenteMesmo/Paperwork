﻿using GameCore;
using System.Linq;

namespace PaperWork
{
    public class DropPaper_Right : SomethingThatHandleUpdates
    {
        private readonly Player Player;

        public DropPaper_Right(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper != null
                && Player.Inputs.ClickedAction1
                && Player.FacingRight
                && Player.Right_ChestPaperDetetor.GetDetectedItems().Any() == false
                && Player.TimeUntilDragDropEnable == 0)
            {
                Player.HorizontalSpeed = 0;
                Player.VerticalSpeed = 0;
                Player.GrabbedPaper.Disabled = false;
                Player.GrabbedPaper.X = MathHelper.RoundUp(
                    Player.Right(),
                    Player.GrabbedPaper.Width + World.SPACE_BETWEEN_THINGS);
                Player.X = Player.GrabbedPaper.Left() - Player.Width -  World.SPACE_BETWEEN_THINGS;
                Player.GrabbedPaper = null;

                Player.TimeUntilDragDropEnable = Player.DRAG_AND_DROP_COOLDOWN;
                Player.AudiosToPlay.Add("grab");
            }
        }
    }
}
