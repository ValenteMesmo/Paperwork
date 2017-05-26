using GameCore;
using PaperWork.GameEntities;
using PaperWork.GameEntities.Grid;

namespace PaperWork
{
    public class Game1 : BaseGame
    {
        public Game1() : base("char", "char_left", "papers", "block")
        {
            var rowNumber = 13;
            var colNumber = 6;

            AddEntity(new PlayerEntity(PlayerInputs)
            {
                Position = new Coordinate2D(100, 100)
            });

            CreateBlocks(rowNumber, colNumber);

            AddEntity(
                new PapersFactory(AddEntity)
                {
                    Position = new Coordinate2D(
                        0,//50*12, 
                        (50*1)+5)
                });

            AddEntity(new GridEntity(6,13,50));
        }

        private void CreateBlocks(int rowNumber, int colNumber)
        {
            for (int i = 1; i < rowNumber; i++)
            {
                AddEntity(new SolidBlock()
                {
                    Position = new Coordinate2D(i * 50, 0)
                });
            }

            for (int i = 1; i < colNumber; i++)
            {
                AddEntity(new SolidBlock()
                {
                    Position = new Coordinate2D(0, i * 50)
                });
            }


            for (int i = 1; i < rowNumber; i++)
            {
                AddEntity(new SolidBlock()
                {
                    Position = new Coordinate2D(i * 50, 50 * colNumber)
                });
            }

            for (int i = 1; i < colNumber; i++)
            {
                AddEntity(new SolidBlock()
                {
                    Position = new Coordinate2D(50 * rowNumber, i * 50)
                });
            }
        }
    }
}
