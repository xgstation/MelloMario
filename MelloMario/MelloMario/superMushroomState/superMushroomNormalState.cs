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
    public class superMushroomNormalState : Interfaces.ItemState
    {
        private ISprite mushroom;
        private superMushroom mushroomItem;
        public superMushroomNormalState(superMushroom mushroomItem1)
        {
            mushroomItem = mushroomItem1;
            mushroom = SpriteFactory.Instance.CreatSuperMushroomSprite();

        }
        public void transNormal()
        {

        }

        public void transDefeated()
        {
            mushroomItem.mushroomState = new superMushroomDefeatedState(mushroomItem);
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
