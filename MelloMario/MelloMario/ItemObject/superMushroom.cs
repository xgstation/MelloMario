using MelloMario.superMushroomState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObject
{
    public class superMushroom
    {
        public Interfaces.ItemState mushroomState;

        public superMushroom()
        {
            mushroomState = new superMushroomNormalState(this);
        }

        public void transNormal()
        {
            mushroomState.transNormal();
        }
        public void transDefeated()
        {
            mushroomState.transDefeated();
        }

        public void Update(GameTime gameTime)
        {
            mushroomState.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            mushroomState.Draw(spriteBatch, location);
        }
    }
}
