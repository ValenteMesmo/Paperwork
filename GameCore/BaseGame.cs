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

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Textures = new Dictionary<string, Texture2D>();

            foreach (var name in TextureNames)
            {
                Textures.Add(name, Content.Load<Texture2D>(name));
            }

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
                                (int)(item.RenderPosition.X + texture.Bonus.X),
                                (int)(item.RenderPosition.Y + texture.Bonus.Y),
                                texture.Width,
                                texture.Height),
                            Color.White);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
