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
    class Normal : BaseState<SuperMushroom>, IItemState
    {
        private SuperMushroom mushroomItem;

        public Normal(SuperMushroom mushroomItem1)
        {
            mushroomItem = mushroomItem1;
        }

        public void Show()
        {
        }

        public void Collect()
        {
        }

        public override void Update(GameTime time)
        {
        }
    }
}
