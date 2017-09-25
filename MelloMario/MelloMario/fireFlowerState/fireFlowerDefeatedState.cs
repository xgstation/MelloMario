using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.fireFlowerState
{
    public class fireFlowerDefeatedState : Interfaces.ItemState
    {

        private ItemObject.fireFlower flowerItem;
        public fireFlowerDefeatedState(ItemObject.fireFlower flower1)
        {
            flowerItem = flower1;

        }
        public void transNormal()
        {
            flowerItem.flowerState = new fireFlowerNormalState(flowerItem);
        }
        public void transDefeated()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

        }
    }
}
