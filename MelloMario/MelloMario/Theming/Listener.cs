using MelloMario.ItemObjects;
using System;

namespace MelloMario.Theming
{
    class Listener
    {
        private GameModel model;

        public Listener(GameModel model)
        {
            this.model = model;
        }

        public void Subscribe(Coin m)
        {
            m.Handler += new Coin.CoinHandler(OnCollect);
        }

        private void OnCollect(Coin m, EventArgs e)
        {
            model.Coins += 1;
            model.Score += 200;
        }
    }
}
