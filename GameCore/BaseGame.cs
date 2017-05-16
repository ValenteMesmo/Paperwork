using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameCore;
using GameCore.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GameCore
{
    public abstract class BaseGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameRunner gameRunner;
        private Dictionary<string, Texture2D> Textures;
        private string[] TextureNames;

        protected InputRepository PlayerInputs;

        public BaseGame(params string[] TextureNames)
        {
            this.TextureNames = TextureNames;
            PlayerInputs = new InputRepository();
            gameRunner = new GameRunner(PlayerInputs, new CollisionDetector());
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
        }

        protected void AddEntity(Entity Entity)
        {
            gameRunner.Entities.Add(Entity);
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

            gameRunner.Start();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
            gameRunner.Dispose();
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach (var item in gameRunner.Entities.ToList())
            {
                foreach (var texture in item.Textures)
                {
                    if (texture.Disabled == false)
                        spriteBatch.Draw(
                            Textures[texture.Name],
                            new Rectangle(
                                (int)(item.RenderPosition.X + texture.Offset.X),
                                (int)(item.RenderPosition.Y + texture.Offset.Y),
                                texture.Width,
                                texture.Height),
                            Color.White);
                }


            }

            foreach (var collider in gameRunner.Entities.ToList().SelectMany(f => f.Colliders))
            {
                DrawBorder(
                        new Rectangle(
                            (int)(collider.Position.X),
                            (int)(collider.Position.Y),
                            (int)collider.Width,
                            (int)collider.Height),
                        3, Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }
    }
}
