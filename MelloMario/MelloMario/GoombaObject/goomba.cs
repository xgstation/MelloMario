using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.GoombaObject
{
    public class Goomba
    {
        public IGoombaState GoombaState;
        public Goomba()
        {
            GoombaState = new GoombaStates.GoombaNormalState(this);
        }

        public void transNormal()
        {
            GoombaState.transNormal();
        }
        public void transDefeated()
        {
            GoombaState.transDefeated();
        }

        public void Update(GameTime gameTime)
        {
            GoombaState.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            GoombaState.Draw(spriteBatch, location);
        }
    }
}
