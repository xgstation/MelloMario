using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.ItemObjects.FireFlowerStates
{
    class FireFlowerUnveil : IItemState
    {
        private FireFlower flowerItem;
        private float elapsed;
        private float offset;
        private Vector2 origin;

        public FireFlowerUnveil(FireFlower newFlower)
        {
            flowerItem = newFlower;
        }
        public void Collect()
        {
            flowerItem.State = new FireFlowerDefeated(flowerItem);
        }

        public void Show()
        {
            flowerItem.State = new FireFlowerNormal(flowerItem);
        }

        public void Update(GameTime time)
        {
            // elapsed += ((float)time.ElapsedGameTime.Milliseconds) / 40;
            // offset =  1f * elapsed;
            // if (offset >= 32f)
            // {
            //     flowerItem.Location = origin;
            //     Show();
            // }
        }
    }
}
