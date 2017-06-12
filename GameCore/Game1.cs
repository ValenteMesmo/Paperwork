using GameCore;

namespace PaperWork
{
    public class Game1 : BaseGame, IGame
    {
        public Game1() : base("char", "Walk0001", "Walk0002", "Walk0003", "char_left", "papers", "block","Head")
        {
        }

        protected override void OnStart()
        {
            var rowNumber = 13;
            var colNumber = 6;

            var player = new Player(PlayerInputs, world, this)
            {
                X = 2000,
                Y = 2000
            };

            world.Add(new PaperFactory(world.Add));

            CreateBlocks(rowNumber, colNumber);

            new Grid(world);
        }

        private void CreateBlocks(int rowNumber, int colNumber)
        {
            //remove left wall? let the player fall?
            var cellsize = 1000 + World.SPACE_BETWEEN_THINGS;
            for (int i = 1; i < rowNumber; i++)
            {
                world.Add(new Block
                {
                    X = i * cellsize,
                    Y = cellsize
                });
            }

            for (int i = 1; i < colNumber; i++)
            {
                world.Add(new Block
                {
                    X = 0,
                    Y = i * cellsize
                });
            }

            for (int i = 1; i < rowNumber; i++)
            {
                world.Add(new Block
                {
                    X = i * cellsize,
                    Y = cellsize * colNumber
                });
            }

            for (int i = 1; i < colNumber; i++)
            {
                world.Add(new Block
                {
                    X = cellsize * rowNumber,
                    Y = i * cellsize
                });
            }
        }
    }
}
