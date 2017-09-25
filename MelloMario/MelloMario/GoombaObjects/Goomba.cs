using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.GoombaObjects.States;

namespace MelloMario.GoombaObjects
{
    public class Goomba: IGameObject
    {
        private Vector2 location;
        public IGoombaState GoombaState;

        public Goomba(Vector2 initLocation)
        {
            GoombaState = new Normal(this);
            this.location = initLocation;
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
            GoombaState.Draw(spriteBatch, location);
        }
    }
}
