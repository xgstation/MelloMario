using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.FireFlowerStates
{
    class FireFlowerDefeated : IItemState
    {

        private FireFlower flowerItem;
        public FireFlowerDefeated(FireFlower flower1)
        {
            flowerItem = flower1;

        }
        public void ChangeToNormal()
        {
            flowerItem.State = new FireFlowerNormal(flowerItem);
        }
        public void ChangeToDefeated()
        {

        }

        public void Update(GameTime time)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

        }
    }
}
