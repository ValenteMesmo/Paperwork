using GameCore;
using System.Linq;
using System;

namespace PaperWork
{
    public interface IGame
    {
        World World { get; }
        void Restart();
    }

    public class HandlePaperFallingInThehead : ICollisionHandler
    {
        private readonly Player Player;
        private readonly IGame Game;

        public HandlePaperFallingInThehead(Player Player, IGame World)
        {
            this.Player = Player;
            this.Game = World;
        }

        public void BotCollision(Collider other)
        {
        }

        public void LeftCollision(Collider other)
        {
        }

        public void RightCollision(Collider other)
        {
        }

        public void TopCollision(Collider other)
        {
            if (other is Paper)
            {
                if (Player.Left_FeetPaperDetector.GetDetectedItems().Any() == false)
                {
                    Player.HorizontalSpeed = -80;
                }
                else
                {
                    if (Player.GrabbedPaper != null)
                    {
                        Player.GrabbedPaper.Disabled = false;
                    }
                    Game.World.Remove(Player);
                    Game.World.Add(new PlayerDestroyed(Game, Player.AnimationFacingRight) { X = Player.X, Y = Player.Y });
                    Game.World.Sleep = 15;
                    Game.World.Camera2d.shakeDuration = 5;
                }
            }
        }
    }
}
