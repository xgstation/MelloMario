﻿namespace MelloMario.Graphics
{
    #region

    using System;
    using MelloMario.UserInterfaces;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class GraphicManager
    {
        private readonly Game1 game;
        private SpriteBatch spriteBatchUI;
        private SpriteBatch spriteBatchGameObjects;
        private readonly UIManager uiManager;
        private IPlayer player;
        private IModel model;

        public GraphicManager(Game1 game)
        {
            this.game = game;
            uiManager = new UIManager(game);
        }

        public void Initialize()
        {
            uiManager.ScreenState = UIManager.State.start;
        }

        public void BindGraphicsDevice(GraphicsDevice newGraphicsDevice)
        {
            spriteBatchUI = new SpriteBatch(newGraphicsDevice);
            spriteBatchGameObjects = new SpriteBatch(newGraphicsDevice);
        }

        public void BindPlyaer(IPlayer newPlayer)
        {
            player = newPlayer;
        }

        public void BindModel(IModel newModel)
        {
            model = newModel;
        }

        public void Update(int time)
        {
            uiManager.Update(time);
            if (model == null)
            {
                return;
            }
            switch (model.State)
            {
                case GameState.gameOver:
                    uiManager.ScreenState = UIManager.State.over;
                    break;
                case GameState.gameWon:
                    uiManager.ScreenState = UIManager.State.won;
                    break;
                case GameState.pause:
                    uiManager.ScreenState = UIManager.State.pause;
                    break;
                case GameState.onProgress:
                    uiManager.ScreenState = UIManager.State.inGame;
                    break;
                case GameState.transist:
                    uiManager.ScreenState = UIManager.State.inGame;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Draw(int time)
        {
            DrawUI(time);
            if (model != null)
            {
                DrawGameObjects(time);
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
            spriteBatchGameObjects.Begin(SpriteSortMode.BackToFront);
            foreach (IGameObject obj in player.Character.CurrentWorld.ScanNearby(player.Camera.Viewport))
            {
                obj.Draw(model?.State == GameState.pause ? 0 : time, spriteBatchGameObjects);
            }
            spriteBatchGameObjects.End();
        }
    }
}