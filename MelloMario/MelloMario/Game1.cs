using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Controllers;
using MelloMario.Factories;
using MelloMario.LevelGen;

namespace MelloMario
{
    /// <summary>
    /// This is the main type for Mello Mario game.
    /// </summary>
    class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private GameModel model;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private float timer = 400;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            model = new GameModel(this);
            IEnumerable<IController> controllers = new List<IController>
            {
                new GamepadController(),
                new KeyboardController()
            };

            model.LoadControllers(controllers);
            model.LoadLevel("Main", true); // Create the level for the first time
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            base.LoadContent();
            SpriteFactory.Instance.BindContentManager(Content);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteFactory.Instance.BindSpriteBatch(spriteBatch);
            font = Content.Load<SpriteFont>("font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="time">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime time)
        {
            base.Update(time);
            timer -= (float) time.ElapsedGameTime.TotalSeconds;

            model.Update(time.ElapsedGameTime.Milliseconds);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="time">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(time);
            spriteBatch.GraphicsDevice.Viewport = new Viewport(0, 0, 800, 600);
            spriteBatch.Begin();
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            //RasterizerState state = new RasterizerState();
            //state.FillMode = FillMode.WireFrame;
            spriteBatch.DrawString(font, "TIME", new Vector2(730, 0), Color.White);
            spriteBatch.DrawString(font, timer.ToString("0"), new Vector2(750, 25), Color.White);
            model.Draw(time.ElapsedGameTime.Milliseconds);
            spriteBatch.End();
        }

        // game controlling

        public void ToggleFullScreen()
        {
            graphics.ToggleFullScreen();
        }
    }
}
