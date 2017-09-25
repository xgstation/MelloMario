using MelloMario.ItemObjects.CoinStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects
{
    public class Coin : BaseItem
    {
        public IItemState coinState;

        public Coin(Vector2 initLocation): base(initLocation)
        {
            coinState = new CoinNormalState(this);
        }

        public void TransNormal()
        {
            coinState.transNormal();
        }

        public void TransDefeated()
        {
            coinState.transDefeated();
        }

        public override void Update(GameTime gameTime)
        {
            coinState.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            coinState.Draw(spriteBatch, Location);
        }
    }
}
