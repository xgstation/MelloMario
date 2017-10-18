using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.ItemObjects.FireFlowerStates
{
    //TODO: Unveil Animation
    class Unveil : BaseState<FireFlower>, IItemState
    {
        private float elapsed;

        public Unveil(FireFlower owner) : base(owner)
        {
            elapsed = 0f;
        }

        public void Collect()
        {
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public override void Update(GameTime time)
        {
            elapsed += time.ElapsedGameTime.Milliseconds;
            if (elapsed >= 2000)
            {
                Show();
            }
            else
            {
                Owner.UnveilMove(-16);
            }
        }
    }
}
