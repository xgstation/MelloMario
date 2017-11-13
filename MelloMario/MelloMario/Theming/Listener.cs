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
        }

        private void OnPointGain(BaseCollidableObject m, PointEventArgs e)
        {
            model.Score += e.Points;
        }

        private void OnCoinCollect(Coin m, EventArgs e)
        {
            model.Coins += 1;
        }
    }
}
