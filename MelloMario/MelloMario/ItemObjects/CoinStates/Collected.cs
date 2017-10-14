using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.CoinStates
{
    class Collected : BaseState<Coin>, IItemState
    {
        private Coin coinItem;

        public Collected(Coin coin1)
        {
            coinItem = coin1;
        }

        public void Show()
        {
            coinItem.State = new Normal(coinItem);
        }

        public void Collect()
        {
        }

        public override void Update(GameTime time)
        {
        }
    }
}
