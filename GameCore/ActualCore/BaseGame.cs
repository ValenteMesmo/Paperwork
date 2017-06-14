using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace GameCore
{
    public abstract class BaseGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Dictionary<string, Texture2D> Textures;
        private string[] TextureNames;
        private Texture2D pixel;
        protected World world;
        private GameRunner gameloop;
        private readonly Camera2d cam;

        public bool FullScreen { get { return graphics.IsFullScreen; } set { graphics.IsFullScreen = value; } }

        public BaseGame(params string[] TextureNames)
        {
            this.TextureNames = TextureNames;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            //TODO: fullscreen on alt+enter
            //TODO: close when esc pressed
            //graphics.IsFullScreen = true;

            cam = new Camera2d();
            cam.Pos = new Vector2(7000f, 5300f);
            cam.Zoom = 0.1f;


            world = new World(cam);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                gameloop.Dispose();
            }
            base.Dispose(disposing);
        }
        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Textures = new Dictionary<string, Texture2D>();

            foreach (var name in TextureNames)
            {
                Textures.Add(name, Content.Load<Texture2D>(name));
            }

            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            StartGame();
        }

        private void StartGame()
        {
            OnStart();
            gameloop = new GameRunner(world.Update);
            gameloop.Start();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                       BlendState.AlphaBlend,
                       null,
                       null,
                       null,
                       null,
                       cam.get_transformation(GraphicsDevice));

            var entiies = world.GetColliders();
            foreach (var item in entiies)
            {
                if (item == null)
                    continue;

                if (item is DimensionalThing)
                {
                    var dimensions = item.As<DimensionalThing>();
                    if (item is Animation)
                    {
                        var textures = item.As<Animation>().GetTextures();

                        foreach (var texture in textures)
                        {
                            spriteBatch.Draw(
                                    Textures[texture.Name]
                                    , new Rectangle(
                                        dimensions.DrawableX + texture.X,
                                        dimensions.DrawableY + texture.Y,
                                        texture.Width,
                                        texture.Height)
                                    , null
                                    , texture.Color
                                    , 0
                                    , Vector2.Zero
                                    , texture.Flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None
                                    , texture.ZIndex
                            );
                        }
                    }

                    //DrawBorder(
                    //        new Rectangle(
                    //            dimensions.X,
                    //            dimensions.Y,
                    //            dimensions.Width,
                    //            dimensions.Height),
                    //        20,
                    //        Color.Red);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder), rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder, rectangleToDraw.Width, thicknessOfBorder), borderColor);
        }

        protected abstract void OnStart();

        public void Restart()
        {
            world.Clear();
            world = new World(cam);
            gameloop.Dispose();
            StartGame();
        }

    }
}
