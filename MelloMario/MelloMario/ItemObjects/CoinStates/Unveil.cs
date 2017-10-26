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
    class Unveil : BaseState<Coin>, IItemState
    {
        private int elapsed;
        private int move;

        public Unveil(Coin owner) : base(owner)
        {
            elapsed = 0;
            move = 0;
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void Collect()
        {
            Owner.Collect();
        }

        public override void Update(GameTime time)
        {
            if (elapsed >= 100)
            {
                move = 0;
            }
            elapsed += time.ElapsedGameTime.Milliseconds;
            move += 1;
            
            if (elapsed >= 300)
            {
                Collect();
                Owner.UnveilMove(0);
            }
            else
            {
                Owner.UnveilMove(-move);
            }
        }
    }
}
