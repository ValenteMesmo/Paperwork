using GameCore;

namespace PaperWork
{
    public class Game1 : BaseGame
    {
        public Game1() : base("char", "char_left", "papers", "block")
        {
        }

        protected override void OnStart()
        {
            var rowNumber = 13;
            var colNumber = 6;

            var nearChest = new Detector<Paper>(80, -20, 50, 50);
            var nearFeet = new Detector<Paper>(80, 80, 50, 50);
            var player = new Player(PlayerInputs, nearFeet, nearChest)
            {
                X = 200,
                Y = 200
            };
            AddEntity(player);

            AddEntity(nearChest);
            AddEntity(nearFeet);

            AddEntity(new GroundCheck(player));
            AddEntity(new PaperFactory(AddEntity));

            CreateBlocks(rowNumber, colNumber);

            //AddEntity(new GridEntity(6, 13, 50));
        }

        private void CreateBlocks(int rowNumber, int colNumber)
        {
            var cellsize = 100 + World.SPACE_BETWEEN_THINGS;
            for (int i = 1; i < rowNumber; i++)
            {
                AddEntity(new Block
                {
                    X = i * cellsize,
                    Y = 0
                });
            }

            for (int i = 1; i < colNumber; i++)
            {
                AddEntity(new Block
                {
                    X = 0,
                    Y = i * cellsize
                });
            }

            for (int i = 1; i < rowNumber; i++)
            {
                AddEntity(new Block
                {
                    X = i * cellsize,
                    Y = cellsize * colNumber
                });
            }

            for (int i = 1; i < colNumber; i++)
            {
                AddEntity(new Block
                {
                    X = cellsize * rowNumber,
                    Y = i * cellsize
                });
            }
        }
    }
}
