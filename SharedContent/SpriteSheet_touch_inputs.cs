using GameCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameAutoGeneratedContent{
    public static class SpriteSheet_touch_inputs
    {
        private static Texture2D Texture;
        
        public static SimpleAnimation Load_Down(ContentManager content, int X = 0, int Y = 0)
        {
            //if (Texture == null)
               //Texture = content.Load<Texture2D>("touch_inputs");
            var animation = new SimpleAnimation(
                
                new AnimationFrame(10, new GameCore.Texture("touch_inputs",0,0,43,28, new Rectangle(0, 0, 43, 28)))
            );

            return animation;
        }

        public static SimpleAnimation Load_DownPressed(ContentManager content, int X = 0, int Y = 0)
        {
            //if (Texture == null)
               //Texture = content.Load<Texture2D>("touch_inputs");
            var animation = new SimpleAnimation(
                
                new AnimationFrame(10, new GameCore.Texture("touch_inputs",0,0,42,25, new Rectangle(0, 28, 42, 25)))
            );

            return animation;
        }

        public static SimpleAnimation Load_Left(ContentManager content, int X = 0, int Y = 0)
        {
            //if (Texture == null)
               //Texture = content.Load<Texture2D>("touch_inputs");
            var animation = new SimpleAnimation(
                
                new AnimationFrame(10, new GameCore.Texture("touch_inputs",0,0,26,50, new Rectangle(0, 53, 26, 50)))
            );

            return animation;
        }

        public static SimpleAnimation Load_LeftPressed(ContentManager content, int X = 0, int Y = 0)
        {
            //if (Texture == null)
               //Texture = content.Load<Texture2D>("touch_inputs");
            var animation = new SimpleAnimation(
                
                new AnimationFrame(10, new GameCore.Texture("touch_inputs",0,0,26,46, new Rectangle(26, 53, 26, 46)))
            );

            return animation;
        }

        public static SimpleAnimation Load_Right(ContentManager content, int X = 0, int Y = 0)
        {
            //if (Texture == null)
               //Texture = content.Load<Texture2D>("touch_inputs");
            var animation = new SimpleAnimation(
                
                new AnimationFrame(10, new GameCore.Texture("touch_inputs",0,0,29,50, new Rectangle(0, 103, 29, 50)))
            );

            return animation;
        }

        public static SimpleAnimation Load_RightPressed(ContentManager content, int X = 0, int Y = 0)
        {
            //if (Texture == null)
               //Texture = content.Load<Texture2D>("touch_inputs");
            var animation = new SimpleAnimation(
                
                new AnimationFrame(10, new GameCore.Texture("touch_inputs",0,0,28,47, new Rectangle(29, 103, 28, 47)))
            );

            return animation;
        }

        public static SimpleAnimation Load_Up(ContentManager content, int X = 0, int Y = 0)
        {
            //if (Texture == null)
               //Texture = content.Load<Texture2D>("touch_inputs");
            var animation = new SimpleAnimation(
                
                new AnimationFrame(10, new GameCore.Texture("touch_inputs",0,0,47,32, new Rectangle(0, 153, 47, 32)))
            );

            return animation;
        }

        public static SimpleAnimation Load_UpPressed(ContentManager content, int X = 0, int Y = 0)
        {
            //if (Texture == null)
               //Texture = content.Load<Texture2D>("touch_inputs");
            var animation = new SimpleAnimation(
                
                new AnimationFrame(10, new GameCore.Texture("touch_inputs",0,0,45,29, new Rectangle(0, 185, 45, 29)))
            );

            return animation;
        }

    }
}