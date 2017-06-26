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
        private readonly Action<string> PlayAudio;

        public HandlePaperFallingInThehead(Player Player, IGame World, Action<string> PlayAudio)
        {
            this.Player = Player;
            this.Game = World;
            this.PlayAudio = PlayAudio;
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
                DestroyPlayer(Player, Game, PlayAudio);
            }
        }

        public static void DestroyPlayer(Player player, IGame Game, Action<string> PlayAudio)
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
                PlayAudio("death");
                Game.World.Sleep();
                Game.World.Camera2d.Shake();
            }
        }
    }
}
