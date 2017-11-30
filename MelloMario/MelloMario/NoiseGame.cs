namespace MelloMario
{
    #region

    using MelloMario.LevelGen.NoiseGenerators;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    // TODO: Merge this file with normal game and use a setting to switch between modes.

    /// <summary>
    ///     This is the main type for Mello Mario game.
    /// </summary>
    internal class NoiseGame : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private NoiseConverter noiseInterpreter;

        public NoiseGame()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 2560,
                PreferredBackBufferHeight = 1500
            };
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            noiseInterpreter = new NoiseConverter(spriteBatch);
            noiseInterpreter.SetSize(80, 18);
            noiseInterpreter.GetData();
            noiseInterpreter.DebugFill();
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
            //spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, model.GetActiveViewMatrix);
            spriteBatch.Begin(SpriteSortMode.BackToFront);
#endif
            noiseInterpreter.Draw();
            //model.Draw(time.ElapsedGameTime.Milliseconds, spriteBatch);
            spriteBatch.End();
        }
    }
}
