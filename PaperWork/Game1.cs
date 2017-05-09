using GameCore;
using PaperWork.GameEntities;

namespace PaperWork
{
    public class Game1 : BaseGame
    {
        public Game1() : base("char", "papers", "block")
        {
            AddEntity(new Player(PlayerInputs) { Position = new Coordinate2D(100, 100) });

            var rowNumber = 13;
            var colNumber = 6;

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
