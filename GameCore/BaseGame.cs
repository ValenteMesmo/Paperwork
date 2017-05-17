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

        protected InputRepository PlayerInputs;

        public BaseGame(params string[] TextureNames)
        {
            this.TextureNames = TextureNames;
            PlayerInputs = new InputRepository();
            Entities = new List<Entity>();

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = false;
            IsFixedTimeStep = true;
            graphics.SynchronizeWithVerticalRetrace = true;
        }

        protected void AddEntity(Entity Entity)
        {
            Entities.Add(Entity);
        }

        protected void RemoveEntity(Entity Entity)
        {
            Entities.Remove(Entity);
        }

        private Texture2D pixel;
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

        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            PlayerInputs.Right.Set(state.IsKeyDown(Keys.D));
            PlayerInputs.Left.Set(state.IsKeyDown(Keys.A));
            PlayerInputs.Jump.Set(state.IsKeyDown(Keys.W));
            PlayerInputs.Crouch.Set(state.IsKeyDown(Keys.S));
            PlayerInputs.Grab.Set(state.IsKeyDown(Keys.K));

            var currentEntities = Entities.ToList();
            foreach (var item in currentEntities)
            {
                item.Update();
            }

            CollisionDetector.DetectCollisions(currentEntities);
            foreach (var item in currentEntities)

                item.AfterCollisions();

            base.Update(gameTime);
        }
        CollisionDetector CollisionDetector = new CollisionDetector();
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach (var item in Entities)
            {
                foreach (var texture in item.Textures)
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
                foreach (var collider in item.Colliders)
                {
                    DrawBorder(
                            new Rectangle(
                                (int)collider.Position.X,
                                (int)collider.Position.Y,
                                (int)collider.Width,
                                (int)collider.Height),
                            3,
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
    }
}
