using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.ItemObjects.CoinStates
{
    class Collected : BaseState<Coin>, IItemState
    {
        public Collected(Coin owner) : base(owner)
        {
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void Collect()
        {
        }

        public override void Update(GameTime time)
        {
        }
    }
}
