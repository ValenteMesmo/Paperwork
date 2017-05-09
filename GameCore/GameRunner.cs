using GameCore.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameCore
{
    public class GameRunner : IDisposable
    {
        private bool running = false;
        private bool disposed = false;
        private Dictionary<string, Coordinate2D> PreviousPositions = new Dictionary<string, Coordinate2D>();
        protected InputRepository InputRepository;
        public IList<Entity> Entities = new List<Entity>();
        private CollisionDetector CollisionDetector;

        public GameRunner(InputRepository InputRepository, CollisionDetector CollisionDetector)
        {
            this.CollisionDetector = CollisionDetector;
            this.InputRepository = InputRepository;
            new Thread(StartGameLoop).Start();
        }

        public void Start()
        {
            running = true;
        }

        public void Stop()
        {
            running = false;
        }

        public void Dispose()
        {
            Stop();
            disposed = true;
        }

        //not idempotent
        private void StartGameLoop()
        {
            var gameloop = new GameLoop(
                BeforeUpdate,
                OnUpdate,
                AfterUpdate);

            while (disposed == false)
            {
                DateTime previous = DateTime.Now;
                while (running)
                {
                    DateTime current = DateTime.Now;
                    gameloop.DoIt(previous, current);
                    previous = current;
                }
            }
        }

        private void BeforeUpdate()
        {
            PreviousPositions = GetPositions(Entities);
        }

        private void OnUpdate()
        {
            foreach (var part in Entities)
            {
                foreach (var update in part.UpdateHandlers)
                {
                    update.Update();
                }
            }

            CollisionDetector.DetectCollisions(Entities);
        }

        public void AfterUpdate(float timeSinceLastUpdate)
        {
            foreach (var entity in Entities)
            {
                if (PreviousPositions.ContainsKey(entity.Id))
                {
                    var newPosition = Lerp(
                        entity.Position,
                        PreviousPositions[entity.Id],
                        timeSinceLastUpdate);

                    entity.RenderPosition = newPosition;
                }
                else
                    entity.RenderPosition = entity.Position;
            }
        }

        private double GetTimeInSeconds()
        {
            return (DateTime.Now - DateTime.MinValue).TotalSeconds;
        }

        private Dictionary<string, Coordinate2D> GetPositions(IEnumerable<Entity> GameObjects)
        {
            return GameObjects.ToDictionary(entry =>
                entry.Id,
                entry => entry.RenderPosition);
        }

        //TODO: move
        static Coordinate2D Lerp(Coordinate2D a, Coordinate2D b, float time)
        {
            return (a + ((b - a) * time));
        }
    }
}
