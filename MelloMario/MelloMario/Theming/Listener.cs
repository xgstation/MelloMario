using MelloMario.BlockObjects;
using MelloMario.ItemObjects;
using MelloMario.MarioObjects;
using System;
using MelloMario.Sounds;

namespace MelloMario.Theming
{
    class GameEventArgs : EventArgs
    {
        public int Points { get; set; }
    }

    class Listener
    {
        private GameModel model;
        private IPlayer player;

        public Listener(GameModel model, IPlayer player)
        {
            this.model = model;
            this.player = player;
        }

        public void Subscribe(Coin m)
        {
            m.HandlerCoins += OnCoinCollect;
        }

        public void Subscribe(Mario m)
        {
            m.HandlerGameOver += OnGameOver;
        }

        public void Subscribe(Flag m)
        {

            m.HandlerTimeScore += OnLevelWon;
        }

        public void Subscribe(BaseCollidableObject m)
        {
            m.HandlerPoints += OnPointGain;
            m.HandlerLives += OnLivesChange;
        }

        private void OnLivesChange(BaseCollidableObject m, GameEventArgs e)
        {
            player.AddLife();
        }

        private void OnPointGain(BaseCollidableObject m, GameEventArgs e)
        {
            player.AddScore(e.Points);
        }

        private void OnLevelWon(Flag m, EventArgs e)
        {
            model.TransistGameWon();
        }

        private void OnGameOver(Mario m, EventArgs e)
        {
            model.Transist();
        }

        private void OnCoinCollect(Coin m, EventArgs e)
        {
            player.AddCoin();
            if (player.Coins == 0)
            {
                SoundController.OneUpCollect.CreateInstance().Play();
            }
        }
    }
}
