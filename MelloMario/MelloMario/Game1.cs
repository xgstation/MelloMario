namespace MelloMario
{
    #region

    using System;
    using System.Collections.Generic;
    using MelloMario.Controls.Controllers;
    using MelloMario.Controls.Scripts;
    using MelloMario.Factories;
    using MelloMario.Graphics;
    using MelloMario.LevelGen.JsonConverters;
    using MelloMario.Sounds;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    /// <summary>
    ///     This is the main type for Mello Mario game.
    /// </summary>
    internal class Game1 : Game
    {
        //XNA Member
        private readonly GraphicsDeviceManager graphics;

        //MelloMario Members
        public enum Menu
        {
            Normal,
            Infinite,
            Quit
        }

        private readonly GraphicsManager graphicsManager;
        private readonly SoundManager soundManager;
        private readonly IEnumerable<IController> controllers;
        private readonly LevelIOJson levelIOJson;
        private GameModel gameModel;

        public Menu CurrentSelected { get; private set; }
        public IPlayer ActivePlayer { get; }

        public Game1()
        {
            //XNA Win Form Initialize
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Const.SCREEN_WIDTH,
                PreferredBackBufferHeight = Const.SCREEN_HEIGHT
            };
            //MelloMario initialize
            CurrentSelected = Menu.Normal;
            graphicsManager = new GraphicsManager(this);
            soundManager = new SoundManager();
            controllers = new List<IController>
            {
                new GamepadController(),
                new KeyboardController()
            };

            ActivePlayer = new Player(graphicsManager, soundManager, controllers);

            new StartScript().Bind(controllers, this);
        }

        // game controlling
        public void ToggleFullScreen()
        {
            graphics.ToggleFullScreen();
        }

        public void ToggleMute()
        {
            soundManager.ToggleMute();
        }

        public void Reset()
        {
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
            graphicsManager.Initialize();
            soundManager.Initialize();
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
            graphicsManager.BindGraphicsDevice(GraphicsDevice);
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the World,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="time">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime time)
        {
            base.Update(time);
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
            graphicsManager.Update(time.ElapsedGameTime.Milliseconds);
            soundManager.Update(time.ElapsedGameTime.Milliseconds);
            gameModel?.Update(time.ElapsedGameTime.Milliseconds);
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="time">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime time)
        {
            base.Draw(time);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            graphicsManager.Draw(time.ElapsedGameTime.Milliseconds);
        }

        public void Select()
        {
            switch (CurrentSelected)
            {
                case Menu.Normal:
                    gameModel = new GameModel(this, GameMode.normal);
                    break;
                case Menu.Infinite:
                    gameModel = new GameModel(this, GameMode.infinite);
                    break;
                case Menu.Quit:
                    Exit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void CursorUp()
        {
            CurrentSelected = CurrentSelected == Menu.Normal ? Menu.Quit : (Menu) ((int) CurrentSelected - 1);
        }

        public void CursorDown()
        {
            CurrentSelected = CurrentSelected == Menu.Quit ? Menu.Normal : (Menu) ((int) CurrentSelected + 1);
        }
    }
}
