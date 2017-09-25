using MelloMario.superMushroomState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects
{
    public class SuperMushroom
    {
        public ItemState mushroomState;

        public SuperMushroom()
        {
            mushroomState = new superMushroomNormalState(this);
        }

        public void TransNormal()
        {
            mushroomState.transNormal();
        }

        public void TransDefeated()
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
