using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        protected InputRepository PlayerInputs;
        protected World world;

        public BaseGame(params string[] TextureNames)
        {
            Camera = new Camera2d();
            Camera.Pos = new Vector2(700f, 380f);
            Camera.Zoom = 0.5f;

            world = new World();
            this.TextureNames = TextureNames;
            PlayerInputs = new InputRepository();

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
            //graphics.IsFullScreen = true;
            graphics.SynchronizeWithVerticalRetrace = false;
        }

        ~BaseGame()
        {
            gameloop.Dispose();
        }

        public bool FullScreen { get { return graphics.IsFullScreen; } set { graphics.IsFullScreen = value; } }

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


            //spriteFont = Content.Load<SpriteFont>("File");

            StartGame();
        }

        private void StartGame()
        {
            OnStart();
            gameloop = new GameRunner(world.Update);
            gameloop.Start();
        }

        protected override void UnloadContent()
        {
            //gameloop.Dispose();
            //Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            PlayerInputs.Update(state);

            base.Update(gameTime);
        }

        //SpriteFont spriteFont;
        private GameRunner gameloop;
        private Camera2d Camera;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        Camera.get_transformation(GraphicsDevice /*Send the variable that has your graphic device here*/));

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

                    DrawBorder(
                            new Rectangle(
                                dimensions.X,
                                dimensions.Y,
                                dimensions.Width,
                                dimensions.Height),
                            2,
                            Color.Red);
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
            world = new World();
            gameloop.Dispose();
            StartGame();
        }

    }
}
