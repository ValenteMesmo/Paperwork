using GameCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaperWork
{
    class MakePapersFall : IHandleEntityUpdates
    {
        private readonly Func<IEnumerable<Entity>> GetPapers;
        private readonly Func<Coordinate2D, bool> PositionBelowAvailable;
        private readonly Action<Entity> MoveToCellBelow;

        public MakePapersFall(
            Func<IEnumerable<Entity>> GetPapers
            , Func<Coordinate2D, bool> PositionBelowAvailable
            , Action<Entity> MoveToCellBelow)
        {
            this.GetPapers = GetPapers;
            this.PositionBelowAvailable = PositionBelowAvailable;
            this.MoveToCellBelow = MoveToCellBelow;
        }

        public void Update(Entity entity)
        {
            foreach (var item in GetPapers().ToList())
            {
                if (PositionBelowAvailable(item.Position))
                {
                    MoveToCellBelow(item);
                }
            }
        }
    }

    public class GridPositions : Entity
    {
        Dictionary<string, Entity> entities;
        string[,] matrix;
        public readonly int cellSize;
        int rows;
        int columns;

        public GridPositions(int rows, int columns, int cellSize)
        {
            this.rows = rows;
            this.columns = columns;
            this.cellSize = cellSize;
            matrix = new string[rows, columns];
            entities = new Dictionary<string, Entity>();
            UpdateHandlers.Add(new MakePapersFall(() => entities.Values.Distinct(), c => PositionAvailable(new Coordinate2D(c.X, c.Y + 50)), MoveToCellBelow));
        }

        private void MoveToCellBelow(Entity obj)
        {
            RemoveFromTheGrid(obj);
            obj.Position = new Coordinate2D(obj.Position.X, obj.Position.Y + 50);
            Set(obj, obj.Position);
        }

        public bool PositionAvailable(Coordinate2D position)
        {
            var gridPosition = new Coordinate2D(
                RoundUp(position.X, cellSize) / cellSize,
                RoundUp(position.Y, cellSize) / cellSize);

            if (gridPosition.X >= rows || gridPosition.Y >= columns)
                return false;

            return matrix[(int)gridPosition.X, (int)gridPosition.Y] == null;
        }

        public void RemoveFromTheGrid(Entity entity)
        {
            if (entities.ContainsKey(entity.Id))
            {
                //var previousPositioin = GetFixedPosition(entities[entity.Id].Position);

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (matrix[i, j] == entity.Id)
                            matrix[i, j] = null;
                    }
                }
            }
        }

        public Coordinate2D Set(Entity entity, Coordinate2D position)
        {
            var newPosition = new Coordinate2D(
                RoundUp(position.X, cellSize),
                RoundUp(position.Y, cellSize));

            var gridPosition = new Coordinate2D(
            newPosition.X / cellSize,
            newPosition.Y / cellSize);

            //if (entitiesPositions.ContainsKey(entity.Id))
            //{
            //    var previousPositioin = entitiesPositions[entity.Id];
            //    matrix[(int)previousPositioin.X, (int)previousPositioin.Y] = null;
            //}

            entity.Position = newPosition;
            entities[entity.Id] = entity;
            matrix[(int)gridPosition.X, (int)gridPosition.Y] = entity.Id;

            return newPosition;
        }

        private Coordinate2D GetFixedPosition(Coordinate2D position)
        {
            return new Coordinate2D(
               RoundUp(position.X, cellSize) / cellSize,
               RoundUp(position.Y, cellSize) / cellSize);
        }

        public Entity GetNearObject(Coordinate2D position)
        {
            var newPosition = new Coordinate2D(
               RoundUp(position.X + 50, cellSize),
               RoundUp(position.Y + 50, cellSize));

            var gridPosition = new Coordinate2D(
                newPosition.X / cellSize,
                newPosition.Y / cellSize);

            if (gridPosition.X >= rows || gridPosition.Y >= columns)
                return null;

            var id = matrix[(int)gridPosition.X, (int)gridPosition.Y] ?? "";
            return
                entities.ContainsKey(id) ? entities[id] : null;
        }

        int RoundUp(float numToRound, int multiple)
        {
            return (int)Math.Round(numToRound / multiple) * multiple;
        }
    }
}
