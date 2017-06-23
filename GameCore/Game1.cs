﻿using GameCore;

namespace PaperWork
{
    public class Game1 : BaseGame, IGame
    {
        protected override void OnStart()
        {
            var rowNumber = 13;
            var colNumber = 6;

            var player = new Player(World.PlayerInputs, World, this)
            {
                X = 2000,
                Y = 2000
            };

            World.Add(new PaperFactory(World.Add));

            CreateBlocks(rowNumber, colNumber);

            new Grid(World);

            CreateTouchInputs();

            World.Add(World.Camera2d);
        }

        private void CreateTouchInputs()
        {
            var btnWidth = 1000;
            var btnHeight = 1000;
            var space = 1;
            var yAnchor = 6100;
            var xAnchor = 1000;

            var extraWidth = 1000;
            var extraHeight = 500;

            //TODO: increase height of toucharea too

            //top left
            World.Add(new TouchArea(
                xAnchor - extraWidth
                , yAnchor - extraHeight + (int)(btnHeight * 0.2f)
                , btnWidth + extraWidth - space
                , btnHeight + extraHeight - space
                , f => World.PlayerInputs.UpDown = World.PlayerInputs.LeftDown = f));
            World.Add(new ButtonAnimation(
                xAnchor
                , yAnchor + (int)(btnHeight * 0.2f)
                , btnWidth - space
                , btnHeight - space
                , () => World.PlayerInputs.UpDown && World.PlayerInputs.LeftDown));

            //top
            World.Add(new TouchArea(
                xAnchor + btnWidth
                , yAnchor - extraHeight + (int)(btnHeight * 0.2f)
                , btnWidth - space
                , btnHeight + extraHeight - space
                , f => World.PlayerInputs.UpDown = f));
            World.Add(new ButtonAnimation(
                xAnchor + btnWidth
                , yAnchor + (int)(btnHeight * 0.2f)
                , btnWidth - space
                , btnHeight - space
                , () => World.PlayerInputs.UpDown && !World.PlayerInputs.LeftDown && !World.PlayerInputs.RightDown));

            //top right
            World.Add(new TouchArea(
                xAnchor + btnWidth * 2
                , yAnchor - extraHeight + (int)(btnHeight * 0.2f)
                , btnWidth + extraWidth - space
                , btnHeight + extraHeight - space
                , f => World.PlayerInputs.UpDown = World.PlayerInputs.RightDown = f));
            World.Add(new ButtonAnimation(
                xAnchor + btnWidth * 2
                , yAnchor + (int)(btnHeight * 0.2f)
                , btnWidth - space
                , btnHeight - space
                , () => World.PlayerInputs.UpDown && World.PlayerInputs.RightDown));

            //left
            World.Add(new TouchArea(
                xAnchor - extraWidth
                , yAnchor + btnHeight + (int)(btnHeight * 0.2f)
                , btnWidth + extraWidth + btnWidth / 2 - space
                , btnHeight - space - (int)(btnHeight * 0.2f)
                , f => World.PlayerInputs.LeftDown = f));
            World.Add(new ButtonAnimation(
                xAnchor
                , yAnchor + btnHeight + (int)(btnHeight * 0.2f)
                , btnWidth + btnWidth / 2 - space
                , btnHeight - space - (int)(btnHeight * 0.2f)
                , () => World.PlayerInputs.LeftDown && !World.PlayerInputs.DownDown && !World.PlayerInputs.UpDown));

            //right
            World.Add(new TouchArea(
                xAnchor + btnWidth + btnWidth / 2
                , yAnchor + btnHeight + (int)(btnHeight * 0.2f)
                , (btnWidth + btnWidth / 2 - space) + extraWidth
                , btnHeight - space - (int)(btnHeight * 0.2f)
                , f => World.PlayerInputs.RightDown = f));
            World.Add(new ButtonAnimation(
                xAnchor + btnWidth + btnWidth / 2
                , yAnchor + btnHeight + (int)(btnHeight * 0.2f)
                , btnWidth + btnWidth / 2 - space
                , btnHeight - space - (int)(btnHeight * 0.2f)
                , () => World.PlayerInputs.RightDown && !World.PlayerInputs.DownDown && !World.PlayerInputs.UpDown));

            //bot left
            World.Add(new TouchArea(xAnchor - extraWidth, yAnchor + btnHeight * 2, btnWidth + extraWidth - space, btnHeight + extraHeight - space, f => World.PlayerInputs.DownDown = World.PlayerInputs.LeftDown = f));
            World.Add(new ButtonAnimation(xAnchor, yAnchor + btnHeight * 2, btnWidth - space, btnHeight - space, () => World.PlayerInputs.DownDown && World.PlayerInputs.LeftDown));

            //bot
            World.Add(new TouchArea(xAnchor + btnWidth, yAnchor + btnHeight * 2, btnWidth - space, btnHeight + extraHeight - space, f => World.PlayerInputs.DownDown = f));
            World.Add(new ButtonAnimation(xAnchor + btnWidth, yAnchor + btnHeight * 2, btnWidth - space, btnHeight - space, () => World.PlayerInputs.DownDown && !World.PlayerInputs.LeftDown && !World.PlayerInputs.RightDown));

            //bot right
            World.Add(new TouchArea(xAnchor + btnWidth * 2, yAnchor + btnHeight * 2, btnWidth + extraWidth - space, btnHeight + extraHeight - space, f => World.PlayerInputs.DownDown = World.PlayerInputs.RightDown = f));
            World.Add(new ButtonAnimation(xAnchor + btnWidth * 2, yAnchor + btnHeight * 2, btnWidth - space, btnHeight - space, () => World.PlayerInputs.DownDown && World.PlayerInputs.RightDown));

            xAnchor = 9700;
            btnWidth = 1800;

            //grab
            World.Add(new TouchArea(
                xAnchor - extraWidth
                , yAnchor + 100 - extraHeight + (int)(btnHeight * 0.2f)
                , btnWidth - space + extraWidth
                , (int)(btnHeight * 2.9f) - space + extraHeight * 2 - (int)(btnHeight * 0.2f)
                , f => World.PlayerInputs.Action1Down = f));
            World.Add(new ButtonAnimation(
               xAnchor
               , yAnchor + 100 + (int)(btnHeight * 0.2f)
               , btnWidth - space
               , (int)(btnHeight * 2.9f) - space - (int)(btnHeight * 0.2f)
               , () => World.PlayerInputs.Action1Down));

            //jump
            World.Add(new TouchArea(
                xAnchor + btnWidth
                , yAnchor + 100 - extraHeight + (int)(btnHeight * 0.2f)
                , (int)(btnWidth - space) + extraWidth
                , (int)(btnHeight * 2.9f) - space + extraHeight - (int)(btnHeight * 0.2f)
                , f => World.PlayerInputs.JumpDown = f));
            World.Add(new ButtonAnimation(
               xAnchor + btnWidth
               , yAnchor + 100 + (int)(btnHeight * 0.2f)
               , (int)(btnWidth - space)
               , (int)(btnHeight * 2.9f) - space - (int)(btnHeight * 0.2f)
               , () => World.PlayerInputs.JumpDown));
        }

        private void CreateBlocks(int rowNumber, int colNumber)
        {
            //remove left wall? let the player fall?
            var cellsize = 1000 + World.SPACE_BETWEEN_THINGS;
            for (int i = 1; i < rowNumber; i++)
            {
                World.Add(new Block(Direction.Top)
                {
                    X = i * cellsize,
                    Y = cellsize
                });
            }

            for (int i = 1; i <= colNumber; i++)
            {

                World.Add(new Block(i == 1 || i == colNumber ? Direction.Center : Direction.Left)
                {
                    X = 0,
                    Y = i * cellsize
                });
            }

            for (int i = 1; i < rowNumber; i++)
            {
                World.Add(new Block(Direction.Bot)
                {
                    X = i * cellsize,
                    Y = cellsize * colNumber
                });
            }

            for (int i = 1; i <= colNumber; i++)
            {
                if (i == 2)
                    continue;
                World.Add(new Block(i == 1 || i == colNumber ? Direction.Center : Direction.Right)
                {
                    X = cellsize * rowNumber,
                    Y = i * cellsize
                });
            }

            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j < rowNumber; j++)
                {
                    World.Add(new Block(Direction.Center)
                    {
                        X = j * cellsize,
                        Y = cellsize * (colNumber + i),
                        Disabled = true
                    });
                }
            }
        }
    }
}
