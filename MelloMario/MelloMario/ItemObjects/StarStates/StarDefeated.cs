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
    class StarDefeated : IItemState
    {
        private Star starItem;

        public StarDefeated(Star star1)
        {
            starItem = star1;
        }

        public void Show()
        {
            starItem.State = new StarNormal(starItem);
        }

        public void Collect()
        {
        }

        public void Update(GameTime time)
        {
        }
    }
}
