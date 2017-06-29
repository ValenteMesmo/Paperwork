using GameCore;
using System.Collections.Generic;

namespace PaperWork
{
    public class PlayerDestroyed : Animation, DimensionalThing, SomethingThatHandleUpdates
    {
        private readonly SimpleAnimation animation;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DrawableX { get; set; }
        public int DrawableY { get; set; }

        private int Duration = 100;
        private readonly IGame Game;
        public PlayerDestroyed(IGame Game, bool facingRight)
        {
            this.Game = Game;
            var walkingWidth = (int)(2000);

            animation = GeneratedContent.Create_recycle_mantis_Death(
                -500,
                -500,
                0,
                walkingWidth,
                walkingWidth,
                facingRight);
        }

        public IEnumerable<Texture> GetTextures()
        {
            return animation.GetTextures();
        }

        public void Update()
        {
            animation.Update();
            Duration--;
            if (Duration == 0)
            {
                Game.World.Remove(this);
                Game.World.Add(new FadeOut(Game.Restart));
            }
        }
    }
}
