using MelloMario.ItemObjects.StarStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects
{
    public class Star : BaseItem
    {
        public IItemState starState;

        public Star(Vector2 initLocation) : base(initLocation)
        {
            starState = new StarNormalState(this);
        }

        public void TransNormal()
        {
            starState.transNormal();
        }
        public void TransDefeated()
        {
            starState.transDefeated();
        }

        public override void Update(GameTime gameTime)
        {
            starState.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            starState.Draw(spriteBatch, Location);
        }
    }
}
