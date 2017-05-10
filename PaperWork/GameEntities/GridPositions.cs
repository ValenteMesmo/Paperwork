using GameCore;
using System;
using System.Collections.Generic;

namespace PaperWork
{
    public class GridPositions
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
        }

        public bool CanSet(Coordinate2D position)
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
                var previousPositioin = entities[entity.Id].Position;
                matrix[(int)previousPositioin.X, (int)previousPositioin.Y] = null;
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

            entities[entity.Id] = entity;
            matrix[(int)gridPosition.X, (int)gridPosition.Y] = entity.Id;

            return newPosition;
        }

        public Entity GetNearObject(Coordinate2D position)
        {
            var newPosition = new Coordinate2D(
               RoundUp(position.X+50, cellSize),
               RoundUp(position.Y, cellSize));

            var gridPosition = new Coordinate2D(
                newPosition.X / cellSize,
                newPosition.Y / cellSize);

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
