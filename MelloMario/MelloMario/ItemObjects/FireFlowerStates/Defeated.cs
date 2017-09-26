using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.FireFlowerStates
{
    public class FireFlowerDefeatedState : IItemState
    {

        private FireFlower flowerItem;
        public FireFlowerDefeatedState(FireFlower flower1)
        {
            flowerItem = flower1;

        }
        public void ChangeToNormal()
        {
            flowerItem.State = new FireFlowerNormalState(flowerItem);
        }
        public void ChangeToDefeated()
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
