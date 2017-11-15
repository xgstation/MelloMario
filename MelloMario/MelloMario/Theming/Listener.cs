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
        private bool won;

        public Listener(GameModel model)
        {
            this.model = model;
            won = false;
        }

        public void Subscribe(Coin m)
        {
            m.HandlerCoins += new Coin.CoinHandler(OnCoinCollect);
        }

        public void Subscribe(Mario m)
        {
            m.HandlerGameOver += new Mario.GameOverHandler(OnGameOver);
        }

        public void Subscribe(Flag m)
        {

            m.HandlerTimeScore += new Flag.TimeScoreHandler(OnLevelWon);
        }

        public void Subscribe(BaseCollidableObject m)
        {
            m.HandlerPoints += new BaseCollidableObject.PointHandler(OnPointGain);
            m.HandlerLives += new BaseCollidableObject.LivesHandler(OnLivesChange);
        }

        private void OnLivesChange(BaseCollidableObject m, GameEventArgs e)
        {
            GameDatabase.Lifes += e.Points;
            if (GameDatabase.Lifes > 99)
                GameDatabase.Lifes = 99;
        }

        private void OnPointGain(BaseCollidableObject m, GameEventArgs e)
        {
            GameDatabase.Score += e.Points;
            if (GameDatabase.Score > 999999)
                GameDatabase.Score = 999999;
        }

        private void OnLevelWon(Flag m, EventArgs e)
        {
            //TODO: this if should eventually be unneeded
            if (!won)
            {
                GameDatabase.Score += GameConst.SCORE_TIME_MULT * GameDatabase.TimeRemain / 1000;
                GameDatabase.TimeRemain = 0;
                model.TransistGameWon();
                won = true;
            }
        }

        private void OnGameOver(Mario m, EventArgs e)
        {
            model.Transist();
        }

        private void OnCoinCollect(Coin m, EventArgs e)
        {
            GameDatabase.Coins += 1;
            if (GameDatabase.Coins >= GameConst.COINS_FOR_LIVE)
            {
                GameDatabase.Coins = 0;
                ++GameDatabase.Lifes;
                SoundController.OneUpCollect.CreateInstance().Play();
            }
        }
    }
}
