using MelloMario.ItemObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.CoinStates
{
    public class coinNormalState : Interfaces.ItemState
    {
        private coin coinItem;
        private ISprite coin;
        public coinNormalState(coin coin1)
        {

            coinItem = coin1;
            coin = SpriteFactory.Instance.CreatCoinSprite();
        }
        public void transNormal()
        {

        }

        public void transDefeated()
        {
            coinItem.coinState = new coinDefeatedState(coinItem);
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
