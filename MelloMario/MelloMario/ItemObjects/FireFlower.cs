using MelloMario.fireFlowerState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects
{
    public class FireFlower
    {
        public ItemState flowerState;

        public FireFlower()
        {
            flowerState = new fireFlowerNormalState(this);
        }

        public void TransNormal()
        {
            flowerState.transNormal();
        }

        public void TransDefeated()
        {
            flowerState.transDefeated();
        }

        public void Update(GameTime gameTime)
        {
            flowerState.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            flowerState.Draw(spriteBatch, location);
        }
    }
}
