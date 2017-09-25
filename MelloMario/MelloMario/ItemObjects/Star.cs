using MelloMario.StarState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObject
{
    public class Star
    {
        public Interfaces.ItemState starState;
        public Star()
        {
            starState = new StarNormalState(this);
        }

        public void transNormal()
        {
            starState.transNormal();
        }
        public void transDefeated()
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
