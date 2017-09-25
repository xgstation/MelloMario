using MelloMario.ItemObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.OneUpMushroomStates
{
    public class oneUpMushroomNormalState : Interfaces.ItemState
    {
        private oneUpMushroom mushroomItem;
        private ISprite mushroom;
        public oneUpMushroomNormalState(oneUpMushroom mushroomItem1)
        {

            mushroomItem = mushroomItem1;
            mushroom = SpriteFactory.Instance.CreatoneUpMushroomSprite();
        }
        public void transNormal()
        {

        }

        public void transDefeated()
        {
            mushroomItem.oneUpMushroomState = new oneUpMushroomDefeatedState(mushroomItem);
        }
        public void Update(GameTime gameTime)
        {
            mushroom.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            mushroom.Draw(spriteBatch, location);
        }
    }
}
