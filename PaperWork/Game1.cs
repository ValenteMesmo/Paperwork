using GameCore;

namespace PaperWork
{
    public class Game1 : BaseGame
    {
        public Game1() : base("char", "papers")
        {
            var player = new Entity();
            player.Textures.Add(new EntityTexture("char", 50, 100));
            player.UpdateHandlers.Add(new MoveRightByInput(player, PlayerInputs));
            AddEntity(player);
        }
    }
}
