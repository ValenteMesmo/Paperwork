using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private World world;

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

        protected void AddEntity(ICollider Entity)
        {
            //Entity.Destroy = () =>
            //{
            //    Entities.Remove(Entity);
            //};
            world.AddCollider(Entity);
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
            gameloop.Dispose();
            Content.Unload();
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

                if (item is ITexture)
                {
                    var texture = item as ITexture;
                    spriteBatch.Draw(
                            Textures[texture.TextureName],
                            new Rectangle(
                                item.DrawableX + texture.TextureOffSetX,
                                item.DrawableY + texture.TextureOffSetY,
                                texture.TextureWidth,
                                texture.TextureHeight),
                            texture.TextureColor);
                }

                DrawBorder(
                        new Rectangle(
                            item.DrawableX,
                            item.DrawableY,
                            item.Width,
                            item.Height),
                        2,
                        Color.Red);
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

        protected void Restart()
        {
            //TODO: world...
            gameloop.Dispose();
            StartGame();
        }

    }
}
