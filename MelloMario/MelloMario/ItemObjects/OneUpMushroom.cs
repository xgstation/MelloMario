using MelloMario.ItemObjects.OneUpMushroomStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects
{
    public class OneUpMushroom
    {
        public IItemState oneUpMushroomState;

        public OneUpMushroom()
        {
            oneUpMushroomState = new OneUpMushroomNormalState(this);
        }

        public void TransNormal()
        {
            oneUpMushroomState.transNormal();
        }

        public void TransDefeated()
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
