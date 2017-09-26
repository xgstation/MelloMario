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
    public class superMushroomDefeatedState : IItemState
    {
        private SuperMushroom mushroomItem;
        public superMushroomDefeatedState(SuperMushroom mushroomItem1)
        {
            mushroomItem = mushroomItem1;
        }

        public void transNormal()
        {
            mushroomItem.State = new SuperMushroomNormalState(mushroomItem);
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
