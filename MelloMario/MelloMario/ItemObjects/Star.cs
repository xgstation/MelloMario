using MelloMario.StarState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects
{
    public class Star
    {
        public ItemState starState;

        public Star()
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

        public void Update(GameTime gameTime)
        {
            starState.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            starState.Draw(spriteBatch, location);
        }
    }
}
