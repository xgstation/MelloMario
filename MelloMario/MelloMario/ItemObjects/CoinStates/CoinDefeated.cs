using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.CoinStates
{
    class CoinDefeated : IItemState
    {
        private Coin coinItem;
        public CoinDefeated(Coin coin1)
        {

            coinItem = coin1;

        }

        public void Show()
        {
            coinItem.State = new CoinNormal(coinItem);
        }

        public void Collect()
        {

        }
        public void Update(GameTime time)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

        }
    }
}
