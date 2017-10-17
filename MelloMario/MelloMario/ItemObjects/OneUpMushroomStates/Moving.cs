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
    class Moving : BaseState<OneUpMushroom>, IItemState
    {
        public Moving(OneUpMushroom owner) : base(owner)
        {
        }

        public void Show()
        {
        }

        public void Collect()
        {
        }

        public override void Update(GameTime time)
        {
            //Moving over time to be done
        }
    }
}
