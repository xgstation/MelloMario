using MelloMario.ItemObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.fireFlowerState
{
    public class fireFlowerNormalState : Interfaces.ItemState
    {
        private ISprite flower;
        private fireFlower flowerItem;
        public fireFlowerNormalState(fireFlower flower1)
        {
            flowerItem = flower1;
            flower = SpriteFactory.Instance.CreateFlowerSprite();
        }
        public void transNormal()
        {

        }
        public void transDefeated()
        {
            flowerItem.flowerState = new fireFlowerDefeatedState(flowerItem);
        }

        public void Update(GameTime gameTime)
        {
            flower.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            flower.Draw(spriteBatch, location);
        }
    }
}
