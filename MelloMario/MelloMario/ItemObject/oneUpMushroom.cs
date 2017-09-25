using MelloMario.oneUpMushroomState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObject
{
    public class oneUpMushroom
    {

        public Interfaces.ItemState oneUpMushroomState;

        public oneUpMushroom()
        {
            oneUpMushroomState = new oneUpMushroomNormalState(this);
        }

        public void transNormal()
        {
            oneUpMushroomState.transNormal();
        }
        public void transDefeated()
        {
            oneUpMushroomState.transDefeated();
        }

        public void Update(GameTime gameTime)
        {
            oneUpMushroomState.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            oneUpMushroomState.Draw(spriteBatch, location);
        }
    }
}
