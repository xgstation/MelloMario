using MelloMario.ItemObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.SuperMushroomStates
{
    public class SuperMushroomDefeatedState : IItemState
    {
        private SuperMushroom mushroomItem;
        public SuperMushroomDefeatedState(SuperMushroom mushroomItem1)
        {
            mushroomItem = mushroomItem1;
        }

        public void ChangeToNormal()
        {
            mushroomItem.State = new SuperMushroomNormalState(mushroomItem);
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
