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
    class SuperMushroomNormal : IItemState
    {
        private SuperMushroom mushroomItem;

        public SuperMushroomNormal(SuperMushroom mushroomItem1)
        {
            mushroomItem = mushroomItem1;
        }

        public void Show()
        {
        }

        public void Collect()
        {
            mushroomItem.State = new SuperMushroomDefeated(mushroomItem);
        }

        public void Update(GameTime time)
        {
        }
    }
}
