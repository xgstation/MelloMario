using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.OneUpMushroomStates
{
    class OneUpMushroomDefeated : IItemState
    {
        private OneUpMushroom mushroomItem;

        public OneUpMushroomDefeated(OneUpMushroom mushroomItem1)
        {
            mushroomItem = mushroomItem1;
        }

        public void Show()
        {
            mushroomItem.State = new OneUpMushroomNormal(mushroomItem);
        }

        public void Collect()
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
