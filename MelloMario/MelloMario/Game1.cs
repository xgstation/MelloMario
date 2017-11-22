namespace MelloMario
{
    #region

    using System.Collections.Generic;
    using MelloMario.Controls.Controllers;
    using MelloMario.Factories;
    using MelloMario.LevelGen;
    using MelloMario.Sounds;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    /// <summary>
    ///     This is the main type for Mello Mario game.
    /// </summary>
    internal class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private GameModel model;
        private SpriteBatch spriteBatch;
        private BGMManager sounds;
        private NoiseInterpreter noiseInterpreter;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Const.SCREEN_WIDTH,
                PreferredBackBufferHeight = Const.SCREEN_HEIGHT
            };
        }

        // game controlling
        public void ToggleFullScreen()
        {
            graphics.ToggleFullScreen();
        }

        public void ToggleMute()
        {
            sounds.ToggleMute();
        }

        public void Reset()
        {
            model = new GameModel(this);
            IEnumerable<IController> controllers = new List<IController>
            {
                new GamepadController(),
                new KeyboardController()
            };
            model.LoadControllers(controllers);
            model.Init();

            sounds = new BGMManager(model);
        }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            Reset();
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            base.LoadContent();

            SpriteFactory.Instance.BindLoader(Content);
            SoundFactory.Instance.BindLoader(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //noiseInterpreter = new NoiseInterpreter(spriteBatch);
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the World,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="time">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime time)
        {
            base.Update(time);
            model.Update(time.ElapsedGameTime.Milliseconds);
            sounds.Update();
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="time">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(time);

#if DEBUGSPRITE //Debug Code
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, null, null, null, null, model.GetActiveViewMatrix);
            RasterizerState state = new RasterizerState();
            state.FillMode = FillMode.WireFrame;
            spriteBatch.GraphicsDevice.RasterizerState = state;
#else
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, model.GetActiveViewMatrix);
            //spriteBatch.Begin(SpriteSortMode.BackToFront);
#endif
            //noiseInterpreter.Draw();
            model.Draw(time.ElapsedGameTime.Milliseconds, spriteBatch);
            spriteBatch.End();
        }
    }
}
