using MelloMario.ItemObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.Factories;

namespace MelloMario.ItemObjects.CoinStates
{
    class Normal : BaseState<Coin>, IItemState
    {
        private Coin coinItem;

        public Normal(Coin coin1)
        {
            coinItem = coin1;
        }

        public void Show()
        {
        }

        public void Collect()
        {
            coinItem.State = new Collected(coinItem);
        }

        public override void Update(GameTime time)
        {
        }
    }
}
