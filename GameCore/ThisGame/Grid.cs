using GameCore;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PaperWork
{
    public class Grid : IUpdateHandler, SomethingWithAudio
    {
        private readonly List<Detector<Paper>> Rows;
        private readonly List<Detector<Paper>> Columns;
        private readonly World World;

        public Grid(World World)
        {
            this.World = World;

            var rowsCount = 4;
            var ColumnsCount = 12;
            var cellSize = 1000 + World.SPACE_BETWEEN_THINGS;

            Rows = new List<Detector<Paper>>();
            Columns = new List<Detector<Paper>>();

            for (int i = 1; i <= rowsCount; i++)
            {
                var trigger = new Detector<Paper>(
                    (cellSize + cellSize / 4)
                   , ((i + 1) * cellSize) + cellSize / 4
                   , cellSize * (ColumnsCount) - cellSize / 2
                   , cellSize - cellSize / 2

                );
                World.Add(trigger);
                Rows.Add(trigger);
            }

            for (int i = 1; i <= ColumnsCount; i++)
            {
                var trigger = new Detector<Paper>(
                    (i * cellSize) + cellSize / 4
                    , (cellSize * 2) + cellSize / 4
                    , cellSize - cellSize / 2
                    , cellSize * rowsCount - cellSize / 2
             );

                World.Add(trigger);
                Columns.Add(trigger);
            }

            World.Add(this);
        }

        public void Update()
        {
            if (comboTime > 0)
                comboTime--;
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

                    var current = other.As<Paper>();
                    if (currentColor != current.Color
                        || previousY - current.Y > current.Height + World.SPACE_BETWEEN_THINGS
                        || previousX - +World.SPACE_BETWEEN_THINGS > current.X && current.X < previousX + World.SPACE_BETWEEN_THINGS)
                    {
                        currentColor = current.Color;
                        if (combo.Count >= 3)
                        {
                            ComputeCombo(combo);
                        }
                        combo.Clear();
                        combo.Add(current);
                    }
                    else
                    {
                        combo.Add(current);
                    }
                    previousY = current.Y;
                    previousX = current.X;
                }

                if (combo.Count >= 3)
                {
                    ComputeCombo(combo);
                }
            }
        }

        private int comboTime = 0;
        private int comboMultiplyer = 1;

        private void ComputeCombo(IEnumerable<Paper> comboItems)
        {
            if (comboTime <= 0)
                comboMultiplyer = 1;
            else
                comboMultiplyer *= 2;

            World.Score += comboItems.Count() * comboMultiplyer;

            World.Sleep();
            World.Camera2d.Shake();

            foreach (var comboItem in comboItems)
            {
                World.Add(new PaperDestroyed(World, comboItem.Color)
                {
                    X = comboItem.X,
                    Y = comboItem.Y
                });
                World.Remove(comboItem);
            }

            comboTime = 300;
            AudiosToPlay.Add("explosion");
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
                        || previousX - paper.X > paper.Width + World.SPACE_BETWEEN_THINGS * 5
                        || paper.Y != previousY)
                    {
                        currentColor = paper.Color;
                        if (combo.Count >= 3)
                        {
                            ComputeCombo(combo);
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
                    ComputeCombo(combo);
                }
            }
        }

        private List<string> AudiosToPlay = new List<string>();

        public IEnumerable<string> GetAudiosToPlay()
        {
            var result = AudiosToPlay.ToList();
            AudiosToPlay.Clear();
            return result;
        }
    }
}