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

        public OneUpMushroomNormal(OneUpMushroom mushroomItem1)
        {

            mushroomItem = mushroomItem1;
        }
        public void Show()
        {
        }

        public void Collect()
        {
            mushroomItem.State = new Defeated(mushroomItem);
        }

        public void Update(GameTime time)
        {
        }
    }
}
