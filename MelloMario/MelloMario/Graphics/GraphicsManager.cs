namespace MelloMario.Graphics
{
    #region

    using System;
    using MelloMario.Graphics.UserInterfaces;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class GraphicsManager
    {
        private readonly Game1 game;
        private UIManager uiManager;
        private SpriteBatch spriteBatchUI;
        private SpriteBatch spriteBatchGameObjects;
        private SpriteBatch spriteBatchBackgroundObjects;
        private IPlayer player;
        private IModel model;

        public GraphicsManager(Game1 game)
        {
            this.game = game;
        }

        public void Initialize()
        {
            uiManager = new UIManager(game);
        }

        public void BindGraphicsDevice(GraphicsDevice newGraphicsDevice)
        {
            spriteBatchUI = new SpriteBatch(newGraphicsDevice);
            spriteBatchGameObjects = new SpriteBatch(newGraphicsDevice);
            spriteBatchBackgroundObjects = new SpriteBatch(newGraphicsDevice);
        }

        public void BindPlyaer(IPlayer newPlayer)
        {
            player = newPlayer;
        }

        public void BindModel(IModel newModel)
        {
            model = newModel;
            uiManager.BindModel(model);
        }

        public void Update(int time)
        {
            uiManager.Update(time);
        }

        public void Draw(int time)
        {
            DrawUI(time);
            if (model == null)
            {
                return;
            }
            switch (model.State)
            {

                case GameState.onProgress:
                    DrawGameObjects(time);
                    break;
                case GameState.pause:
                    DrawGameObjects(0);
                    break;
                case GameState.gameOver:
                    break;
                case GameState.gameWon:
                    break;
                case GameState.transist:
                    break;
            }
        }

        private void DrawUI(int time)
        {
            spriteBatchUI.Begin(SpriteSortMode.BackToFront);
            uiManager.Draw(time, spriteBatchUI);
            spriteBatchUI.End();
        }

        private void DrawGameObjects(int time)
        {
            spriteBatchGameObjects.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, player.Camera.GetViewMatrix(new Vector2(1f)));

            foreach (IGameObject obj in player.Character.CurrentWorld.ScanNearby(player.Camera.Viewport))
            {

                obj.Draw(time, spriteBatchGameObjects);

            }

            spriteBatchGameObjects.End();
        }

        private void DrawBackgroundObjects(int time)
        {
            spriteBatchBackgroundObjects.Begin(
                SpriteSortMode.BackToFront,
                null,
                null,
                null,
                null,
                null,
                player.Camera.GetViewMatrix(new Vector2(0.5f)));
            //TODO: Add background objects
            spriteBatchBackgroundObjects.End();
        }
    }
}
