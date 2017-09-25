using MelloMario.ItemObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.superMushroomState
{
    public class superMushroomDefeatedState : Interfaces.ItemState
    {
        private superMushroom mushroomItem;
        public superMushroomDefeatedState(superMushroom mushroomItem1)
        {
            mushroomItem = mushroomItem1;
        }

        public void transNormal()
        {
            mushroomItem.mushroomState = new superMushroomNormalState(mushroomItem);
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
