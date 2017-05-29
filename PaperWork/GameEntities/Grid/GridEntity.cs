using GameCore;
using GameCore.Collision;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace PaperWork.GameEntities.Grid
{
    public class GridEntity : Entity
    {
        private IHandleUpdates Update;

        public GridEntity(int rowsCount, int ColumnsCount, int cellSize)
        {
            var Rows = new List<Trigger>();
            var Columns = new List<Trigger>();

            for (int i = 1; i < rowsCount; i++)
            {
                var collider = new Trigger(
                    this,
                    cellSize * (ColumnsCount - 1) - 30,
                    cellSize - 30
                    , cellSize + 15
                    , (i * cellSize) + 15
                );

                Rows.Add(collider);
                Colliders.Add(collider);
            }

            for (int i = 1; i < ColumnsCount; i++)
            {
                var collider = new Trigger(
                    this,
                    cellSize - 30,
                    cellSize * (rowsCount - 1) - 30
                    , (i * cellSize) + 15
                    , cellSize + 15
                );

                Columns.Add(collider);
                Colliders.Add(collider);
            }

            Update = new UpdateHandlerAggregator(new MyClass(
                    () => Rows,
                    () => Columns));
        }

        protected override void OnUpdate()
        {
            Update.Update(this);
        }
    }

    public class MyClass : IHandleUpdates
    {
        private readonly Func<IEnumerable<Trigger>> GetRows;
        private readonly Func<IEnumerable<Trigger>> GetColumns;

        public MyClass(
            Func<IEnumerable<Trigger>> GetRows
            , Func<IEnumerable<Trigger>> GetColumns)
        {
            this.GetRows = GetRows;
            this.GetColumns = GetColumns;
        }

        public void Update(Entity entity)
        {
            ComputeHorizontalCombos();
            ComputeVerticalCombos();
        }

        private void ComputeVerticalCombos()
        {
            var columns = GetColumns();
            int index = 0;
            foreach (var row in columns)
            {
                index++;
                var entities = row.GetEtities().OrderByDescending(f => f.Position.Y);
                var count = entities.Count();

                var currentColor = Color.GhostWhite;
                var combo = new List<PapersEntity>();
                var previousY = 0f;
                var previousX = 0f;

                foreach (var other in entities)
                {
                    if (other is PapersEntity == false)
                        continue;

                    var paper = other.As<PapersEntity>();
                    if (currentColor != paper.Color
                        || paper.Position.Y - previousY < -50
                        || paper.Position.X != previousX)
                    {
                        currentColor = paper.Color;
                        if (combo.Count >= 3)
                        {
                            foreach (var comboItem in combo)
                            {
                                comboItem.Destroy();
                            }
                        }
                        combo.Clear();
                        combo.Add(paper);
                    }
                    else
                    {
                        combo.Add(paper);
                    }
                    previousY = paper.Position.Y;
                    previousX = paper.Position.X;
                }

                if (combo.Count >= 3)
                {
                    foreach (var comboItem in combo)
                    {
                        comboItem.Destroy();
                    }
                }
            }
        }

        private void ComputeHorizontalCombos()
        {
            var rows = GetRows();
            int index = 0;
            foreach (var row in rows)
            {
                index++;
                var entities = row.GetEtities().OrderByDescending(f => f.Position.X);
                var count = entities.Count();

                var currentColor = Color.GhostWhite;
                var combo = new List<PapersEntity>();
                var previousX = 0f;
                var previousY = 0f;

                foreach (var other in entities)
                {
                    if (other is PapersEntity == false)
                        continue;

                    var paper = other.As<PapersEntity>();
                    if (currentColor != paper.Color
                        || paper.Position.X - previousX < -50
                        || paper.Position.Y != previousY)
                    {
                        currentColor = paper.Color;
                        if (combo.Count >= 3)
                        {
                            foreach (var comboItem in combo)
                            {
                                comboItem.Destroy();
                            }
                        }
                        combo.Clear();
                        combo.Add(paper);
                    }
                    else
                    {
                        combo.Add(paper);
                    }
                    previousX = paper.Position.X;
                    previousY = paper.Position.Y;
                }

                if (combo.Count >= 3)
                {
                    foreach (var comboItem in combo)
                    {
                        comboItem.Destroy();
                    }
                }
            }
        }
    }
}
