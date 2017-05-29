﻿using GameCore;
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
            var Rows = new List<Collider>();
            var Columns = new List<Collider>();

            //for (int i = 1; i < rowsCount; i++)
            //{
            //    var trigger = new BaseCollider(
            //        this,
            //        cellSize * (ColumnsCount - 1) - 30,
            //        cellSize - 30
            //        , cellSize + 15
            //        , (i * cellSize) + 15
            //        , true);

            //    Rows.Add(trigger);
            //    Colliders.Add(trigger);
            //}

            //for (int i = 1; i < ColumnsCount; i++)
            //{
            //    var trigger = new BaseCollider(
            //        this,
            //        cellSize - 30,
            //        cellSize * (rowsCount - 1) - 30
            //        , (i * cellSize) + 15
            //        , cellSize + 15
            //        , true);

            //    Columns.Add(trigger);
            //    Colliders.Add(trigger);
            //}

            Update = new UpdateHandlerAggregator(
                new MyClass(
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
        private readonly Func<IEnumerable<Collider>> GetRows;
        private readonly Func<IEnumerable<Collider>> GetColumns;

        public MyClass(
            Func<IEnumerable<Collider>> GetRows
            , Func<IEnumerable<Collider>> GetColumns)
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
                var entities = row.GetEntities().OrderByDescending(f => f.Position.Y);
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
                var entities = row.GetEntities().OrderByDescending(f => f.Position.X);
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
