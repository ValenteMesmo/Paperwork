using GameCore;

namespace PaperWork
{
    public class Game1 : BaseGame, IWorld
    {
        public Game1() : base("char", "char_left", "papers", "block")
        {
        }

        protected override void OnStart()
        {
            var rowNumber = 13;
            var colNumber = 6;

            var player = new Player(PlayerInputs, world, this)
            {
                X = 200,
                Y = 200
            };

            world.Add(new PaperFactory(world.Add));

            CreateBlocks(rowNumber, colNumber);

            new Grid(world);
        }

        private void CreateBlocks(int rowNumber, int colNumber)
        {
            var cellsize = 100 + World.SPACE_BETWEEN_THINGS;
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
