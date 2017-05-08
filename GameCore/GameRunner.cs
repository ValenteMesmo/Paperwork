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

        protected InputRepository InputRepository;

        public IList<Entity> GameParts = new List<Entity>();

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

        private void StartGameLoop()
        {
            var gameloop = new GameLoop(
                InputRepository,
                new Collision.CollisionDetector(),
                OnUpdate);

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

        private void OnUpdate()
        {
            foreach (var part in GameParts)
            {
                foreach (var update in part.UpdateHandlers)
                {
                    update.Update();
                }
            }

            CollisionDetector.DetectCollisions(GameParts);
        }

        private double GetTimeInSeconds()
        {
            return (DateTime.Now - DateTime.MinValue).TotalSeconds;
        }
    }
}
