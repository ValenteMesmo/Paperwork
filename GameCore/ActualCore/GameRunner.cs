﻿using System;
using System.Threading.Tasks;

namespace GameCore
{
    public class GameRunner : IDisposable
    {
        private bool running = false;
        private bool disposed = false;
        private readonly Action OnUpdate;
        public string ErrorMessage = "";

        public GameRunner(Action OnUpdate)
        {
            this.OnUpdate = OnUpdate;
            Task.Factory.StartNew(StartGameLoop);
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
            var gameloop = new GameLoop(OnUpdate);

            while (disposed == false)
            {
                DateTime previous = DateTime.Now;
                while (running)
                {
                    DateTime current = DateTime.Now;
#if DEBUG
                    try
                    {
#endif
                        gameloop.DoIt(previous, current);
#if DEBUG
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = ex.ToString();
                    }
#endif
                    previous = current;
                }
            }
        }
    }
}
