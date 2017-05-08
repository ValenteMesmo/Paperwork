using GameCore.Collision;
using GameCore.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    internal class GameLoop
    {
        public Dictionary<string, Entity> GameObjects { get; private set; }
        private double accumulator = 0.0;
        private InputRepository InputRepository;
        private ILog Log = new FenixLogger();
        private CollisionDetector CollisionService;
        private Action OnUpdate;

        public GameLoop(
            InputRepository InputRepository,
            CollisionDetector CollisionService,
            Action OnUpdate)
        {
            this.InputRepository = InputRepository;
            this.GameObjects = new Dictionary<string, Entity>();
            this.CollisionService = CollisionService;
            this.OnUpdate = OnUpdate;
        }

        public void DoIt(DateTime previous, DateTime current)
        {            
            var timeSinceLastUpdate =
                (float)(current - previous).TotalSeconds;

            if (timeSinceLastUpdate > 0.25f)
                timeSinceLastUpdate = 0.25f;

            accumulator += timeSinceLastUpdate;

            var previousGameObjects = CloneDictionary();

            while (accumulator >= 0.01)
            {
                OnUpdate();

                accumulator -= 0.01;
            }

            var currentGameObject = CloneDictionary();

            foreach (var key in currentGameObject.Keys)
            {
                if (previousGameObjects.ContainsKey(key))
                {
                    var newPosition = Lerp(
                        GameObjects[key].Position,
                        previousGameObjects[key].Position,
                        timeSinceLastUpdate);

                    currentGameObject[key].Position = newPosition;
                }
            }

            GameObjects = currentGameObject;
        }

        //TODO: move
        private Dictionary<string, Entity> CloneDictionary()
        {
            return GameObjects.ToDictionary(entry =>
                entry.Key,
                entry => entry.Value);
        }

        //TODO: move
        static Coordinate2D Lerp(Coordinate2D a, Coordinate2D b, float time)
        {
            return a * time + b * (1f - time);
        }
    }
}