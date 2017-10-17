using MelloMario.ItemObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.Factories;

namespace MelloMario.ItemObjects.CoinStates
{
    class Normal : BaseState<Coin>, IItemState
    {
        public Normal(Coin owner) : base(owner)
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
        }
    }
}
