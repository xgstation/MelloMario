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
        GameModel model;
        GameScript script;

        LevelReader reader;
        LevelIOJson jsonReader;
        bool jsonFlag = true;
        SpriteBatch spriteBatch;

        public Game1()
        {
            model = new GameModel();
            script = new GameScript();

            reader = new LevelReader("Content/ExampleLevel.txt");
            jsonReader = new LevelIOJson("Content/ExampleLevel.json", model);
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

            IEnumerable<IController> controllers = new List<IController>
            {
                new GamepadController(model),
                new KeyboardController(model)
            };

            model.LoadControllers(controllers);
            model.Bind(script);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            base.LoadContent();

            SpriteFactory.Instance.LoadAllTextures(Content);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteFactory.Instance.BindSpriteBatch(spriteBatch);
            if (jsonFlag)
            {
                model.LoadEntities(jsonReader);
            }
            else
            {
                model.LoadEntities(reader);
            }
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

            model.Update(time);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="time">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(time);

            spriteBatch.Begin();
            if (model.IsPaused)
            {
                ResetElapsedTime();
            }
            model.Draw(time, ZIndex.back);
            model.Draw(time, ZIndex.main);
            model.Draw(time, ZIndex.front);
            spriteBatch.End();
        }
    }
}
