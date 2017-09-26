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
    public class OneUpMushroomNormalState : IItemState
    {
        private OneUpMushroom mushroomItem;
        private ISprite mushroom;
        public OneUpMushroomNormalState(OneUpMushroom mushroomItem1)
        {

            mushroomItem = mushroomItem1;
            mushroom = SpriteFactory.Instance.CreateOneUpMushroomSprite();
        }
        public void ChangeToNormal()
        {

        }

        public void ChangeToDefeated()
        {
            mushroomItem.State = new OneUpMushroomDefeatedState(mushroomItem);
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
