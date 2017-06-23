using GameCore;

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

            World.Add(new TouchButton(xAnchor, yAnchor, btnWidth - space, btnHeight - space, f => World.PlayerInputs.UpDown = World.PlayerInputs.LeftDown = f));
            World.Add(new TouchButton(xAnchor + btnWidth, yAnchor, btnWidth - space, btnHeight - space, f => World.PlayerInputs.UpDown = f));
            World.Add(new TouchButton(xAnchor + btnWidth * 2, yAnchor, btnWidth - space, btnHeight - space, f => World.PlayerInputs.UpDown = World.PlayerInputs.RightDown = f));

            World.Add(new TouchButton(xAnchor, yAnchor + btnHeight, btnWidth + btnWidth / 2 - space, btnHeight - space, f => World.PlayerInputs.LeftDown = f));
            World.Add(new TouchButton(xAnchor + btnWidth + btnWidth / 2, yAnchor + btnHeight, btnWidth + btnWidth / 2 - space, btnHeight - space, f => World.PlayerInputs.RightDown = f));

            World.Add(new TouchButton(xAnchor, yAnchor + btnHeight * 2, btnWidth - space, btnHeight - space, f => World.PlayerInputs.DownDown = World.PlayerInputs.LeftDown = f));
            World.Add(new TouchButton(xAnchor + btnWidth, yAnchor + btnHeight * 2, btnWidth - space, btnHeight - space, f => World.PlayerInputs.DownDown = f));
            World.Add(new TouchButton(xAnchor + btnWidth * 2, yAnchor + btnHeight * 2, btnWidth - space, btnHeight - space, f => World.PlayerInputs.DownDown = World.PlayerInputs.RightDown = f));

            xAnchor = 9700;
            btnWidth = 1500;
            World.Add(new TouchButton(
                xAnchor
                , yAnchor + 100
                , btnWidth - space
                , (int)(btnHeight * 2.9f) - space
                , f => World.PlayerInputs.Action1Down = f));
            World.Add(new TouchButton(
                xAnchor + btnWidth
                , yAnchor + 100
                , (int)(btnWidth * 1.25f - space)
                , (int)(btnHeight * 2.9f) - space
                , f => World.PlayerInputs.JumpDown = f));
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
