﻿using GameCore;
using System.Linq;

namespace PaperWork
{
    public class DropPaper_Left : SomethingThatHandleUpdates
    {
        private readonly Player Player;

        public DropPaper_Left(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.GrabbedPaper != null
                && Player.Inputs.ClickedAction1
                && Player.FacingRight == false
                && Player.Left_ChestPaperDetetor.GetDetectedItems().Any() == false
                && Player.TimeUntilDragDropEnable == 0)
            {
                Player.VerticalSpeed = 0;
                Player.HorizontalSpeed = 0;
                Player.GrabbedPaper.Disabled = false;
                Player.GrabbedPaper.X = MathHelper.RoundUp(
                    Player.Left() - Player.GrabbedPaper.Width - World.SPACE_BETWEEN_THINGS,
                    Player.GrabbedPaper.Width + World.SPACE_BETWEEN_THINGS);
                Player.X = Player.GrabbedPaper.Right() + World.SPACE_BETWEEN_THINGS;
                Player.GrabbedPaper = null;
                Player.TimeUntilDragDropEnable = Player.DRAG_AND_DROP_COOLDOWN;
                Player.AudiosToPlay.Add("grab");
            }
        }
    }
}
