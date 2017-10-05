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
    class StarNormal : IItemState
    {
        private Star starItem;

        public StarNormal(Star star1)
        {
            starItem = star1;
        }

        public void Show()
        {
        }

        public void Collect()
        {
            starItem.State = new StarDefeated(starItem);
        }

        public void Update(GameTime time)
        {
        }
    }
}
