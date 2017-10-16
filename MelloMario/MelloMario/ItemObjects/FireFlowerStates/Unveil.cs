using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.ItemObjects.FireFlowerStates
{
    class Unveil : BaseState<FireFlower>, IItemState
    {
        private float elapsed;
        private float offset;
        private Vector2 origin;

        public Unveil(FireFlower owner) : base(owner)
        {
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
