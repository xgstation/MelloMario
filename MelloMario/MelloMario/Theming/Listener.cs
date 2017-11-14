using MelloMario.ItemObjects;
using System;

namespace MelloMario.Theming
{
    class PointEventArgs : EventArgs
    {
        public int Points { get; set; }
    }

    class Listener
    {
        private GameModel model;

        public Listener(GameModel model)
        {
            this.model = model;
        }

        public void Subscribe(Coin m)
        {
            m.HandlerCoins += new Coin.CoinHandler(OnCoinCollect);
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

        private void OnCoinCollect(Coin m, EventArgs e)
        {
            model.Coins += 1;
            if(model.Coins >= 99)
            {
                model.Coins = 0;
                ++model.Lives;
            }
        }
    }
}
