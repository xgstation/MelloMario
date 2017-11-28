namespace MelloMario.Objects.UserInterfaces
{
    #region

    using System;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class UIManager
    {
        public enum State
        {
            idle,
            start,
            pause,
            over,
            won,
            inGame
        }

        private readonly Game1 game;
        private IObject hud;
        private IObject splash;

        private string worldName;
        private IPlayer player;
        private State state;

        public State ScreenState
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                UpdateInterface();
            }
        }

        public UIManager(Game1 game)
        {
            this.game = game;
            ScreenState = State.start;
        }

        public void BindPlayer(IPlayer newPlayer)
        {
            player = newPlayer;
        }

        public void Initialize()
        {
            worldName = player.Character.CurrentWorld.ID;
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
                ((HUD) hud).IsSplashing = ScreenState == State.won || ScreenState == State.over;
            }
            splash?.Draw(time, spriteBatch);
        }

        public void ToggleHUD()
        {
            hud = hud == null ? new HUD() : null;
        }

        private void UpdateInterface()
        {
            switch (ScreenState)
            {
                case State.start:
                    splash = new GameStart(game);
                    hud = null;
                    break;
                case State.pause:
                    splash = new GamePause();
                    break;
                case State.over:
                    splash = new GameOver(player.Lifes, worldName);
                    break;
                case State.won:
                    splash = new GameWon();
                    break;
                case State.inGame:
                    hud = new HUD();
                    splash = null;
                    break;
                case State.idle:
                    hud = null;
                    splash = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
