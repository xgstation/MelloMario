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
    public class FireFlowerNormalState : IItemState
    {
        private ISprite flower;
        private FireFlower flowerItem;
        public FireFlowerNormalState(FireFlower flower1)
        {
            flowerItem = flower1;
            flower = SpriteFactory.Instance.CreateFlowerSprite();
        }
        public void transNormal()
        {

        }
        public void transDefeated()
        {
            flowerItem.State = new FireFlowerDefeatedState(flowerItem);
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
