using MelloMario.ItemObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.FireFlowerStates
{
    class FireFlowerNormal : IItemState
    {
        private ISprite flower;
        private FireFlower flowerItem;
        public FireFlowerNormal(FireFlower flower1)
        {
            flowerItem = flower1;
            flower = SpriteFactory.Instance.CreateFlowerSprite();
        }
        public void ChangeToNormal()
        {

        }
        public void ChangeToDefeated()
        {
            flowerItem.State = new FireFlowerDefeated(flowerItem);
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
