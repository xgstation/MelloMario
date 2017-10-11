using MelloMario.ItemObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.StarStates
{
    class Normal : IItemState
    {
        private Star starItem;

        public Normal(Star star1)
        {
            starItem = star1;
        }

        public void Show()
        {
        }

        public void Collect()
        {
        }

        public void Update(GameTime time)
        {
        }
    }
}
