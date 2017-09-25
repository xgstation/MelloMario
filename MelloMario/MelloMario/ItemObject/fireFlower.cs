using MelloMario.fireFlowerState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObject
{
    public class fireFlower
    {
        public Interfaces.ItemState flowerState;
        public fireFlower()
        {
            flowerState = new fireFlowerNormalState(this);
        }

        public void transNormal()
        {
            flowerState.transNormal();
        }
        public void transDefeated()
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
