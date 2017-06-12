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
            
            _resolutionIndependence = new ResolutionIndependentRenderer(this);
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
            _camera = new Camera2D(_resolutionIndependence);
            _camera.Zoom = 0.06f;
            _camera.Position = new Vector2(
                _resolutionIndependence.VirtualWidth *5.15f, 
                _resolutionIndependence.VirtualHeight*7f);
            //Camera = new Camera2d();
            //Camera.Pos = new Vector2(7000f, 4380f);
            //Camera.Zoom = 0.06f;
            InitializeResolutionIndependence(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);


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

        private void InitializeResolutionIndependence(int realScreenWidth, int realScreenHeight)
        {
            _resolutionIndependence.VirtualWidth = 1366;
            _resolutionIndependence.VirtualHeight = 768;
            _resolutionIndependence.ScreenWidth = realScreenWidth;
            _resolutionIndependence.ScreenHeight = realScreenHeight;
            _resolutionIndependence.Initialize();

            _camera.RecalculateTransformationMatrices();
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
        private readonly ResolutionIndependentRenderer _resolutionIndependence;
        private Camera2D _camera;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(
                SpriteSortMode.Deferred, 
                BlendState.AlphaBlend, 
                SamplerState.LinearWrap, 
                DepthStencilState.None, 
                RasterizerState.CullNone, 
                null, 
                _camera.GetViewTransformationMatrix());

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
            world = new World();
            gameloop.Dispose();
            StartGame();
        }

    }
}
