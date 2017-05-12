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

        public bool PositionAvailable(Coordinate2D position)
        {
            var gridPosition = new Coordinate2D(
                RoundUp(position.X, cellSize) / cellSize,
                RoundUp(position.Y, cellSize) / cellSize);

            if (gridPosition.X >= rows || gridPosition.Y >= columns)
                return false;

            return matrix[(int)gridPosition.X, (int)gridPosition.Y] == null;
        }

        public Entity Pop(Coordinate2D fromPosition)
        {
            var gridPosition = GetGridPosition(RoundPosition(fromPosition));

            if (gridPosition.X >= rows || gridPosition.Y >= columns)
                return null;

            var id = matrix[(int)gridPosition.X, (int)gridPosition.Y] ?? "";

            if (entities.ContainsKey(id))
            {
                var entity = entities[id];
                matrix[(int)gridPosition.X, (int)gridPosition.Y] = null;
                entities.Remove(id);
                return entity;
            }

            return null;
        }

        public void Push(Entity entity)
        {
            var roundPosition = RoundPosition(entity.Position);
            entity.Position = roundPosition;
            var gridPosition = GetGridPosition(roundPosition);

            entities[entity.Id] = entity;
            matrix[(int)gridPosition.X, (int)gridPosition.Y] = entity.Id;
        }

        private void MoveToCellBelow(Entity entity)
        {
            Pop(entity.Position);
            entity.Position = new Coordinate2D(entity.Position.X, entity.Position.Y + 50);
            Push(entity);
        }

        private Coordinate2D RoundPosition(Coordinate2D position)
        {
            return new Coordinate2D(
               RoundUp(position.X, cellSize),
               RoundUp(position.Y, cellSize));
        }

        public Coordinate2D GetGridPosition(Coordinate2D roundPosition)
        {
            return new Coordinate2D(
               roundPosition.X / cellSize,
               roundPosition.Y / cellSize);
        }

        private int RoundUp(float numToRound, int multiple)
        {
            return (int)Math.Round(numToRound / multiple) * multiple;
        }
    }
}
