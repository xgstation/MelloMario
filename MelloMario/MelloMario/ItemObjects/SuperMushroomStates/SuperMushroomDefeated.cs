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
    class SuperMushroomDefeated : IItemState
    {
        private SuperMushroom mushroomItem;
        public SuperMushroomDefeated(SuperMushroom mushroomItem1)
        {
            mushroomItem = mushroomItem1;
        }

        public void Show()
        {
            mushroomItem.State = new SuperMushroomNormal(mushroomItem);
        }
        public void Collect()
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
