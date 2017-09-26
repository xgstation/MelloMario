using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.CoinStates
{
    public class CoinDefeatedState : IItemState
    {
        private Coin coinItem;
        public CoinDefeatedState(Coin coin1)
        {

            coinItem = coin1;

        }

        public void transNormal()
        {
            coinItem.State = new CoinNormalState(coinItem);
        }

        public void transDefeated()
        {

        }
        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

        }
    }
}
