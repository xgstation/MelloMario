using MelloMario.CoinStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObject
{
    public class coin
    {
        public Interfaces.ItemState coinState;
        public coin()
        {
            coinState = new coinNormalState(this);
        }

        public void transNormal()
        {
            coinState.transNormal();
        }
        public void transDefeated()
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
