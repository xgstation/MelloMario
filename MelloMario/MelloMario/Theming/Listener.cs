using MelloMario.BlockObjects;
using MelloMario.ItemObjects;
using System;
using MelloMario.SplashObjects;

namespace MelloMario.Theming
{
    class PointEventArgs : EventArgs
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

        public void Subscribe(Flag m)
        {
            m.HandlerTimeScore += new Flag.TimeScoreHandler(OnLevelWon);
        }

        public void Subscribe(BaseCollidableObject m)
        {
            m.HandlerPoints += new BaseCollidableObject.PointHandler(OnPointGain);
            m.HandlerLives += new BaseCollidableObject.LivesHandler(OnLivesChange);
        }

        private void OnLivesChange(BaseCollidableObject m, PointEventArgs e)
        {
            model.Lives += e.Points;
        }

        private void OnPointGain(BaseCollidableObject m, PointEventArgs e)
        {
            model.Score += e.Points;
        }

        private void OnLevelWon(Flag m, EventArgs e)
        {

            //TODO: this if should eventually be unneeded
            if (!won)
            {
                model.Score += GameConst.SCORE_TIME_MULT * model.Time;
                model.Time = 0;
                won = true;
            }
        }

        private void OnCoinCollect(Coin m, EventArgs e)
        {
            model.Coins += 1;
            if(model.Coins >= GameConst.COINS_FOR_LIVE)
            {
                //TODO: play a one up sound
                model.Coins = 0;
                ++model.Lives;
            }
        }
    }
}
