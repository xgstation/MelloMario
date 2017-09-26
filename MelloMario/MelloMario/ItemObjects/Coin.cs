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
        public IItemState State;

        public Coin(Vector2 initLocation): base(initLocation)
        {
            State = new CoinNormalState(this);
        }

        public void TransNormal()
        {
            State.ChangeToNormal();
        }

        public void TransDefeated()
        {
            State.ChangeToDefeated();
        }

        public override void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, Location);
        }
    }
}
