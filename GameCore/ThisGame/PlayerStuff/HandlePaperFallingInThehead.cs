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
                DestroyPlayer(Player, Game);
            }
        }

        public static void DestroyPlayer(Player player, IGame Game)
        {
            if (player.DeathDetector.GetDetectedItems().Any() == false)
            {
                player.HorizontalSpeed = -80;
            }
            else
            {
                if (player.GrabbedPaper != null)
                {
                    player.GrabbedPaper.Disabled = false;
                }
                Game.World.Remove(player);
                Game.World.Add(new PlayerDestroyed(Game, player.AnimationFacingRight) { X = player.X, Y = player.Y });
                Game.World.Sleep();
                Game.World.Camera2d.Shake();
            }
        }
    }
}
