using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.ItemObjects.FireFlowerStates
{
    class FireFlowerUnveilState : IItemState
    {
        private ISprite flower;
        private FireFlower flowerItem;
        private float elapsed;
        private float offset;
        public FireFlowerUnveilState(FireFlower newFlower)
        {
            flowerItem = newFlower;
            flower = SpriteFactory.Instance.CreateFlowerSprite();
        }
        public void ChangeToDefeated()
        {
            flowerItem.State = new FireFlowerDefeatedState(flowerItem);
        }

        public void ChangeToNormal()
        {
            flowerItem.State = new FireFlowerNormalState(flowerItem);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            flower.Draw(spriteBatch, location);
        }

        public void Update(GameTime gameTime)
        {
            elapsed += ((float)gameTime.ElapsedGameTime.Milliseconds) / 20;
            offset =  1f * elapsed;
            if (offset >= 16)
            {
                ChangeToNormal();
            }
        }
    }
}
