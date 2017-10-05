using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.FireFlowerStates
{
    class FireFlowerDefeated : IItemState
    {

        private FireFlower flowerItem;

        public FireFlowerDefeated(FireFlower flower1)
        {
            flowerItem = flower1;
        }

        public void Show()
        {
            flowerItem.State = new FireFlowerNormal(flowerItem);
        }

        public void Collect()
        {
        }

        public void Update(GameTime time)
        {
        }
    }
}
