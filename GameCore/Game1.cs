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




            var btnWidth = 1000;
            var btnHeight = 900;
            var space = 1;
            var yAnchor = 6600;
            var xAnchor = 300;

            World.Add(new TouchButton(xAnchor, yAnchor, btnWidth - space, btnHeight - space, f => World.PlayerInputs.Up = World.PlayerInputs.Left = f));
            World.Add(new TouchButton(xAnchor + btnWidth, yAnchor, btnWidth - space, btnHeight - space, f => World.PlayerInputs.Up = f));
            World.Add(new TouchButton(xAnchor + btnWidth * 2, yAnchor, btnWidth - space, btnHeight - space, f => World.PlayerInputs.Up = World.PlayerInputs.Right = f));
            
            World.Add(new TouchButton(xAnchor, yAnchor + btnHeight, btnWidth + btnWidth / 2 - space, btnHeight - space, f => World.PlayerInputs.Left = f));
            World.Add(new TouchButton(xAnchor + btnWidth + btnWidth / 2, yAnchor + btnHeight, btnWidth + btnWidth / 2 - space, btnHeight - space, f => World.PlayerInputs.Right = f));
            
            World.Add(new TouchButton(xAnchor, yAnchor + btnHeight * 2, btnWidth - space, btnHeight - space, f => World.PlayerInputs.Down = World.PlayerInputs.Left = f));
            World.Add(new TouchButton(xAnchor + btnWidth, yAnchor + btnHeight * 2, btnWidth - space, btnHeight - space, f => World.PlayerInputs.Down = f));
            World.Add(new TouchButton(xAnchor + btnWidth * 2, yAnchor + btnHeight * 2, btnWidth - space, btnHeight - space, f => World.PlayerInputs.Down = World.PlayerInputs.Right = f));

            xAnchor = 9600;
            btnWidth = 2000;
            World.Add(new TouchButton(
                xAnchor
                , yAnchor
                , btnWidth - space
                , (int)(btnHeight * 3f) - space
                , f => World.PlayerInputs.Action1 = f));
            World.Add(new TouchButton(
                xAnchor + btnWidth
                , yAnchor
                , btnWidth - space
                , (int)(btnHeight * 3f) - space
                , f => World.PlayerInputs.Up = f));
        }

        private void CreateBlocks(int rowNumber, int colNumber)
        {
            //remove left wall? let the player fall?
            var cellsize = 1000 + World.SPACE_BETWEEN_THINGS;
            for (int i = 1; i < rowNumber; i++)
            {
                World.Add(new Block
                {
                    X = i * cellsize,
                    Y = cellsize
                });
            }

            for (int i = 1; i < colNumber; i++)
            {
                World.Add(new Block
                {
                    X = 0,
                    Y = i * cellsize
                });
            }

            for (int i = 1; i < rowNumber; i++)
            {
                World.Add(new Block
                {
                    X = i * cellsize,
                    Y = cellsize * colNumber
                });
            }

            for (int i = 1; i < colNumber; i++)
            {
                if (i == 2)
                    continue;
                World.Add(new Block
                {
                    X = cellsize * rowNumber,
                    Y = i * cellsize
                });
            }
        }
    }
}
