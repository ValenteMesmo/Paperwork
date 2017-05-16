using GameCore;
using PaperWork.GameEntities;
using System.Runtime.InteropServices;

namespace PaperWork
{
    public class Game1 : BaseGame
    {
        public Game1() : base("char", "papers", "block")
        {
            var rowNumber = 13;
            var colNumber = 6;

            AddEntity(new PlayerEntity(PlayerInputs, RemoveEntity)
            {
                Position = new Coordinate2D(100, 100)
            });

            var paper1 = new PapersEntity(50, RemoveEntity)
            {
                Position = new Coordinate2D(
                    rowNumber * 50 - 50,
                    colNumber * 50 - 100)
            };

            var paper2 = new PapersEntity(50, RemoveEntity)
            {
                Position = new Coordinate2D(
                   rowNumber * 50 - 100,
                   colNumber * 50 - 100)
            };

            AddEntity(new PapersEntity(50, RemoveEntity)
            {
                Position = new Coordinate2D(
                  rowNumber * 50 - 200,
                  colNumber * 50 - 100)
            });

            AddEntity(new PapersEntity(50, RemoveEntity)
            {
                Position = new Coordinate2D(
                    rowNumber * 50 - 200,
                    colNumber * 50 - 200)
            });

            AddEntity(paper1);
            AddEntity(paper2);

            CreateBlocks(rowNumber, colNumber);
        }

        private void CreateBlocks(int rowNumber, int colNumber)
        {
            for (int i = 1; i < rowNumber; i++)
            {
                AddEntity(new SolidBlock(RemoveEntity)
                {
                    Position = new Coordinate2D(i * 50, 0)
                });
            }

            for (int i = 1; i < colNumber; i++)
            {
                AddEntity(new SolidBlock(RemoveEntity)
                {
                    Position = new Coordinate2D(0, i * 50)
                });
            }


            for (int i = 1; i < rowNumber; i++)
            {
                AddEntity(new SolidBlock(RemoveEntity)
                {
                    Position = new Coordinate2D(i * 50, 50 * colNumber)
                });
            }

            for (int i = 1; i < colNumber; i++)
            {
                AddEntity(new SolidBlock(RemoveEntity)
                {
                    Position = new Coordinate2D(50 * rowNumber, i * 50)
                });
            }
        }
    }
}
