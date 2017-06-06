using GameCore;
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
            var cellSize = 100;

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

            World.Add(this);
        }

        public void Update()
        {
            ComputeHorizontalCombos();
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

/*public class GridEntity : Entity		
-    {		
-        private IHandleUpdates Update;		
-		
-        public GridEntity(		
-            int rowsCount,
-            int ColumnsCount,
-            int cellSize)
-        {		
-            var Rows = new List<Trigger>();		
-            var Columns = new List<Trigger>();		
-		
-            for (int i = 1; i<rowsCount; i++)		
-            {		
-                var trigger = new Trigger(
-this
-                    , cellSize + 15
-                    , (i * cellSize) + 15
-                    , cellSize * (ColumnsCount - 1) - 30
-                    , cellSize - 30
-
-                );		
-                Colliders.Add(trigger);		
-                Rows.Add(trigger);		
-            }		
-		
-            for (int i = 1; i<ColumnsCount; i++)		
-            {		
-                var trigger = new Trigger(
-this
-                    , (i * cellSize) + 15
-                    , cellSize + 15
                 , cellSize - 30
                 , cellSize * (rowsCount - 1) - 30
             );		
             Colliders.Add(trigger);		

             Columns.Add(trigger);		
         }		

         Update = new UpdateHandlerAggregator(		
             new MyClass(		
                 () => Rows,
                 () => Columns));		
     }		

     protected override void OnUpdate()
     {		
         Update.Update();		
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

     public void Update()
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
                     || paper.Position.Y - previousY< -50		
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
                     || paper.Position.X - previousX< -50		
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
 }*/
