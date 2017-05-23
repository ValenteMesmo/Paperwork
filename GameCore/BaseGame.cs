using GameCore.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private readonly List<Entity> Entities;
        private Texture2D pixel;
        private CollisionDetector CollisionDetector = new CollisionDetector();
        protected InputRepository PlayerInputs;
        private FrameCounter _frameCounter = new FrameCounter();

        public BaseGame(params string[] TextureNames)
        {
            this.TextureNames = TextureNames;
            PlayerInputs = new InputRepository();
            Entities = new List<Entity>();

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
        }

        protected void AddEntity(Entity Entity)
        {
            Entity.Destroy = () =>
            {
                Entities.Remove(Entity);
            };
            Entities.Add(Entity);
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


            spriteFont = Content.Load<SpriteFont>("File");


            gameloop = new GameRunner(() =>
           {
               var currentEntities = Entities.ToList();
               foreach (var item in currentEntities)
               {
                   item.Update();
               }

               CollisionDetector.DetectCollisions(currentEntities);
               foreach (var item in currentEntities)
               {
                   item.AfterCollisions();
               }

           });
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

        SpriteFont spriteFont;
        private GameRunner gameloop;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            var entiies = Entities.ToArray();
            foreach (var item in entiies)
            {
                if (item == null)
                    return;

                foreach (var texture in item.GetTextures())
                {
                    if (texture.Disabled == false)
                        spriteBatch.Draw(
                            Textures[texture.Name],
                            new Rectangle(
                                (int)(item.Position.X + texture.Offset.X),
                                (int)(item.Position.Y + texture.Offset.Y),
                                texture.Width,
                                texture.Height),
                            texture.Color);
                }
                foreach (var collider in item.GetColliders())
                {
                    DrawBorder(
                            new Rectangle(
                                (int)collider.Position.X,
                                (int)collider.Position.Y,
                                (int)collider.Width,
                                (int)collider.Height),
                            2,
                            Color.Red);
                }
            }

            {
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                _frameCounter.Update(deltaTime);
                var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);
                spriteBatch.DrawString(spriteFont, fps, new Vector2(1, 1), Color.Black);
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
    }
}
