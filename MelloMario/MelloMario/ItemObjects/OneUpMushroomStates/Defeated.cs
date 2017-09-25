using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.OneUpMushroomStates
{
    public class oneUpMushroomDefeatedState : Interfaces.ItemState
    {
        private ItemObject.oneUpMushroom mushroomItem;
        public oneUpMushroomDefeatedState(ItemObject.oneUpMushroom mushroomItem1)
        {
            mushroomItem = mushroomItem1;
        }
        public void transNormal()
        {
            mushroomItem.oneUpMushroomState = new oneUpMushroomNormalState(mushroomItem);
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
