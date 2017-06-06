﻿using GameCore;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaperWork
{
    public class Grid : IUpdateHandler
    {
        private readonly List<Detector<Paper>> Rows;
        private readonly List<Detector<Paper>> Columns;
        private readonly World World;

        public Grid(World World)
        {
            this.World = World;

            var rowsCount = 6;
            var ColumnsCount = 12;
            var cellSize = 100 + World.SPACE_BETWEEN_THINGS;

            Rows = new List<Detector<Paper>>();
            Columns = new List<Detector<Paper>>();

            for (int i = 1; i < rowsCount; i++)
            {
                var trigger = new Detector<Paper>(
                    cellSize + cellSize / 4
                   , (i * cellSize) + cellSize / 4
                   , cellSize * (ColumnsCount) - cellSize / 2
                   , cellSize - cellSize / 2

                );
                World.Add(trigger);
                Rows.Add(trigger);
            }

            for (int i = 1; i <= ColumnsCount ; i++)
            {
                var trigger = new Detector<Paper>(
                     (i * cellSize) + cellSize/4
                    , cellSize + cellSize / 4
                     , cellSize - cellSize / 2
                     , cellSize * (rowsCount - 1) - cellSize / 2
             );

                World.Add(trigger);
                Columns.Add(trigger);
            }

            World.Add(this);
        }

        public void Update()
        {
            ComputeHorizontalCombos();
            ComputeVerticalCombos();
        }

        private void ComputeVerticalCombos()
        {
            int index = 0;
            foreach (var row in Columns)
            {
                index++;
                var entities = row.GetDetectedItems().OrderByDescending(f => f.Y);
                var count = entities.Count();

                var currentColor = Color.GhostWhite;
                var combo = new List<Paper>();
                var previousY = 0f;
                var previousX = 0f;

                foreach (var other in entities)
                {
                    if (other is Paper == false)
                        continue;

                    var paper = other.As<Paper>();
                    if (currentColor != paper.Color
                        || previousY - paper.Y >  paper.Height + World.SPACE_BETWEEN_THINGS
                        || paper.X != previousX)
                    {
                        currentColor = paper.Color;
                        if (combo.Count >= 3)
                        {
                            foreach (var comboItem in combo)
                            {
                                World.Remove(comboItem);
                            }
                        }
                        combo.Clear();
                        combo.Add(paper);
                    }
                    else
                    {
                        combo.Add(paper);
                    }
                    previousY = paper.Y;
                    previousX = paper.X;
                }

                if (combo.Count >= 3)
                {
                    foreach (var comboItem in combo)
                    {
                        World.Remove(comboItem);
                    }
                }
            }
        }

        private void ComputeHorizontalCombos()
        {
            int index = 0;
            foreach (var row in Rows)
            {
                index++;
                var entities = row.GetDetectedItems().OrderByDescending(f => f.X);
                var count = entities.Count();

                var currentColor = Color.GhostWhite;
                var combo = new List<Paper>();
                var previousX = 0f;
                var previousY = 0f;

                foreach (var other in entities)
                {
                    if (other is Paper == false)
                        continue;

                    var paper = other.As<Paper>();
                    if (currentColor != paper.Color
                        || previousX - paper.X > paper.Width + World.SPACE_BETWEEN_THINGS
                        || paper.Y != previousY)
                    {
                        currentColor = paper.Color;
                        if (combo.Count >= 3)
                        {
                            foreach (var comboItem in combo)
                            {
                                World.Remove(comboItem);
                            }
                        }
                        combo.Clear();
                        combo.Add(paper);
                    }
                    else
                    {
                        combo.Add(paper);
                    }
                    previousX = paper.X;
                    previousY = paper.Y;
                }

                if (combo.Count >= 3)
                {
                    foreach (var comboItem in combo)
                    {
                        World.Remove(comboItem);
                    }
                }
            }
        }
    }
}