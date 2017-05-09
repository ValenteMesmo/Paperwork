using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameCore;
using GameCore.Collision;
using System;
using System.Collections.Generic;
using System.Linq;

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
            gameRunner.GameParts.Add(Entity);
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

            PlayerInputs.RightPressed = state.IsKeyDown(Keys.D);
            PlayerInputs.LeftPressed = state.IsKeyDown(Keys.A);
            PlayerInputs.JumpPressed = state.IsKeyDown(Keys.W);
            PlayerInputs.CrouchPressed = state.IsKeyDown(Keys.S);
            PlayerInputs.GrabbPressed = state.IsKeyDown(Keys.K);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach (var item in gameRunner.GameParts.ToList())
            {
                foreach (var texture in item.Textures)
                {
                    if (texture.Disabled == false)
                        spriteBatch.Draw(
                            Textures[texture.Name],
                            new Rectangle(
                                (int)item.Position.X,
                                (int)item.Position.Y,
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
