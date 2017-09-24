using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario
{
    public class Goomba
    {
        public IGoombaState GoombaState;
        public Goomba()
        {
            GoombaState = new GoombaNormalState(this);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            GoombaState.Draw(spriteBatch);
        }
    }
}
