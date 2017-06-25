using GameCore;
using Microsoft.Xna.Framework;

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
            var yAnchor = 6000;
            var xAnchor = 800;

            var extraWidth = 1000;
            var extraHeight = 500;

            //top left
            World.Add(new TouchArea(
                xAnchor - extraWidth
                , yAnchor - extraHeight
                , btnWidth + extraWidth - space
                , btnHeight + extraHeight - space
                , f => World.PlayerInputs.UpDown = World.PlayerInputs.LeftDown = f));
            World.Add(new ButtonAnimation(
                xAnchor
                , yAnchor
                , btnWidth - space
                , btnHeight - space
                , () => World.PlayerInputs.UpDown && World.PlayerInputs.LeftDown));

            //top
            World.Add(new TouchArea(
                xAnchor + btnWidth
                , yAnchor - extraHeight
                , btnWidth - space
                , btnHeight + extraHeight - space
                , f => World.PlayerInputs.UpDown = f));
            World.Add(new ButtonAnimation(
                xAnchor + btnWidth
                , yAnchor
                , btnWidth - space
                , btnHeight - space
                , () => World.PlayerInputs.UpDown && !World.PlayerInputs.LeftDown && !World.PlayerInputs.RightDown));

            //top right
            World.Add(new TouchArea(
                xAnchor + btnWidth * 2
                , yAnchor - extraHeight
                , btnWidth + extraWidth - space
                , btnHeight + extraHeight - space
                , f => World.PlayerInputs.UpDown = World.PlayerInputs.RightDown = f));
            World.Add(new ButtonAnimation(
                xAnchor + btnWidth * 2
                , yAnchor
                , btnWidth - space
                , btnHeight - space
                , () => World.PlayerInputs.UpDown && World.PlayerInputs.RightDown));

            //left
            World.Add(new TouchArea(
                xAnchor //- extraWidth
                , yAnchor + btnHeight
                , (int)(btnWidth * 1.5f) - space
                , btnHeight - space
                , f => World.PlayerInputs.LeftDown = f));
            World.Add(new ButtonAnimation(
                xAnchor //-extraWidth
                , yAnchor + btnHeight
                , (int)(btnWidth * 1.5f)
                , btnHeight - space
                , () => World.PlayerInputs.LeftDown && !World.PlayerInputs.DownDown && !World.PlayerInputs.UpDown));

            //right
            World.Add(new TouchArea(
                xAnchor + (int)(btnWidth * 1.5f) + space /// 3
                , yAnchor + btnHeight
                , (int)(btnWidth * 1.5f) - space + extraWidth
                , btnHeight - space
                , f => World.PlayerInputs.RightDown = f));
            World.Add(new ButtonAnimation(
                xAnchor + (int)(btnWidth * 1.5f) + space /// 3
                , yAnchor + btnHeight
                , (int)(btnWidth * 1.5f)  
                , btnHeight - space
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
                , yAnchor + 100 - extraHeight
                , btnWidth - space + extraWidth
                , (int)(btnHeight * 2.9f) - space + extraHeight * 2
                , f => World.PlayerInputs.Action1Down = f));
            World.Add(new ButtonAnimation(
               xAnchor
               , yAnchor + 100
               , btnWidth - space
               , (int)(btnHeight * 2.9f) - space
               , () => World.PlayerInputs.Action1Down));

            //jump
            World.Add(new TouchArea(
                xAnchor + btnWidth
                , yAnchor + 100 - extraHeight
                , (int)(btnWidth - space) + extraWidth
                , (int)(btnHeight * 2.9f) - space + extraHeight
                , f => World.PlayerInputs.JumpDown = f));
            World.Add(new ButtonAnimation(
               xAnchor + btnWidth
               , yAnchor + 100
               , (int)(btnWidth - space)
               , (int)(btnHeight * 2.9f) - space
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

            var backgrond = GeneratedContent.Create_background_wall_center(
                0
                , cellsize * 6
                , 1
                , cellsize * 14
                , cellsize * 4);
            backgrond.ChangeColor(new Color(158, 165, 178));
            World.Add(backgrond);

        }
    }
}
