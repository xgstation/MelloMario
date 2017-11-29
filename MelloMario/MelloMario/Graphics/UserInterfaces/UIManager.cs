namespace MelloMario.Graphics.UserInterfaces
{
    #region

    using System;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class UIManager
    {
        private readonly Game1 game;
        private readonly IPlayer player;
        private IUserInterface hud;
        private IUserInterface splash;

        private string worldName;
        private IModel model;

        public UIManager(Game1 game)
        {
            this.game = game;
            player = game.ActivePlayer;
            splash = new GameStart(game);
        }
        
        public void Update(int time)
        {
            if (player != null && hud != null)
            {
                worldName = player.Character.CurrentWorld.ID;
                (hud as HUD)?.OnHUDInfoChange(player.Lifes, player.Score, player.Coins, player.TimeRemain, worldName);
                hud.Update(time);
            }
            splash?.Update(time);
        }

        public void Draw(int time, SpriteBatch spriteBatch)
        {
            hud?.Draw(time, spriteBatch);
            if (hud != null)
            {
                ((HUD) hud).IsSplashing = splash is GameWon || splash is GameOver;
            }
            splash?.Draw(time, spriteBatch);
        }

        public void ToggleHUD()
        {
            hud = hud == null ? new HUD() : null;
        }

        public void BindModel(IModel newModel)
        {
            model = newModel;
            model.StateChanged += GameModelStateChanged;
        }

        private void GameModelStateChanged(object sender, GameState state)
        {
            switch (model?.State)
            {
                case GameState.gameOver:
                    splash = new GameOver(player.Lifes, player.Character.CurrentWorld.ID);
                    break;
                case GameState.gameWon:
                    splash = new GameWon();
                    break;
                case GameState.pause:
                    splash = new GamePause();
                    break;
                case GameState.onProgress:
                    splash = null;
                    hud = new HUD();
                    break;
                case GameState.transist:
                    break;
                case null:
                    splash = new GameStart(game);
                    hud = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
