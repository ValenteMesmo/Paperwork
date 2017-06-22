using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace GameCore
{
    public abstract class BaseGame : Game
    {
        private const bool RENDER_COLLIDERS = false;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D pixel;
        public World World { get; }
        private GameRunner gameloop;
        private readonly Camera2d cam;

        public bool FullScreen { get { return graphics.IsFullScreen; } set { graphics.IsFullScreen = value; } }

        private SpriteFont font;

        public BaseGame()
        {
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



            World = new World(cam);
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
        GeneratedContent GeneratedContent;
        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Score");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GeneratedContent = new GeneratedContent();
            GeneratedContent.Load(Content);

            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            gameloop = new GameRunner(World.Update);
            gameloop.Start();
            StartGame();
        }

        private void StartGame()
        {
            OnStart();
        }

        protected override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            TouchCollection touchCollection = TouchPanel.GetState();
            World.PlayerInputs.SetState(state);
            World.PlayerInputs.SetState(touchCollection);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(158, 165, 178));
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                       BlendState.AlphaBlend,
                       null,
                       null,
                       null,
                       null,
                       cam.get_transformation(GraphicsDevice));

            spriteBatch.DrawString(
                font
                , World.Score.ToString("0000000")//"1.000.000"
                , new Vector2(4500, 7000)
                , new Color(158, 165, 178)// new Color(253, 205, 1)
                , 0
                , Vector2.Zero
                , 10
                , SpriteEffects.None
                , 0);

            var entiies = World.GetColliders();

            if (entiies == null)
                return;

            foreach (var item in entiies)
            {
                if (item == null)
                    continue;

                if (item is DimensionalThing)
                {
                    var dimensions = item.As<DimensionalThing>();

                    if (RENDER_COLLIDERS)
                    {
                        DrawBorder(
                                        new Rectangle(
                                            dimensions.X,
                                            dimensions.Y,
                                            dimensions.Width,
                                            dimensions.Height),
                                        20,
                                        Color.Red);
                    }
                }

                if (item is Animation)
                {
                    int bonusX = 0;
                    int bonusY = 0;

                    if (item is DimensionalThing)
                    {
                        var dimensions = item.As<DimensionalThing>();
                        bonusX = dimensions.DrawableX;
                        bonusY = dimensions.DrawableY;

                    }

                    var textures = item.As<Animation>().GetTextures();

                    foreach (var texture in textures)
                    {
                        spriteBatch.Draw(
                                GeneratedContent.Textures[texture.Name]
                                , new Rectangle(
                                    bonusX + texture.X,
                                    bonusY + texture.Y,
                                    texture.Width,
                                    texture.Height)
                                , texture.PositionOnSpriteSheet
                                , texture.Color
                                , 0
                                , Vector2.Zero
                                , texture.Flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None
                                , texture.ZIndex
                        );
                    }



                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder), rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder, rectangleToDraw.Width, thicknessOfBorder), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

        protected abstract void OnStart();

        public void Restart()
        {
            //gameloop.Dispose();
            World.Clear();
            //World = new World(cam);
            StartGame();
        }

        protected override void EndRun()
        {
            World.Stopped = true;
            base.EndRun();
        }
    }
}
