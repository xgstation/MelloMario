using MelloMario.ItemObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.OneUpMushroomStates
{
    class OneUpMushroomNormal : IItemState
    {
        private OneUpMushroom mushroomItem;
        private ISprite mushroom;
        public OneUpMushroomNormal(OneUpMushroom mushroomItem1)
        {

            mushroomItem = mushroomItem1;
            mushroom = SpriteFactory.Instance.CreateOneUpMushroomSprite();
        }
        public void ChangeToNormal()
        {

        }

        public void ChangeToDefeated()
        {
            mushroomItem.State = new OneUpMushroomDefeated(mushroomItem);
        }
        public void Update(GameTime time)
        {
            mushroom.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            mushroom.Draw(spriteBatch, location);
        }
    }
}
