using MelloMario.ItemObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.CoinStates
{
    class CoinNormal : IItemState
    {
        private Coin coinItem;
        private ISprite coin;
        public CoinNormal(Coin coin1)
        {

            coinItem = coin1;
            coin = SpriteFactory.Instance.CreatCoinSprite();
        }
        public void Show()
        {

        }

        public void Collect()
        {
            coinItem.State = new CoinDefeated(coinItem);
        }
        public void Update(GameTime gameTime)
        {
            coin.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            coin.Draw(spriteBatch, location);
        }
    }
}
