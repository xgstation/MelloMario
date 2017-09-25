using MelloMario.CoinStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects
{
    public class Coin
    {
        public ItemState coinState;

        public Coin()
        {
            coinState = new coinNormalState(this);
        }

        public void TransNormal()
        {
            coinState.transNormal();
        }

        public void TransDefeated()
        {
            coinState.transDefeated();
        }

        public void Update(GameTime gameTime)
        {
            coinState.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            coinState.Draw(spriteBatch, location);
        }
    }
}
