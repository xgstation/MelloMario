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
        private float elapsed;
        private float realOffset;

        public Unveil(Coin owner) : base(owner)
        {
            elapsed = 0f;
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
            elapsed += time.ElapsedGameTime.Milliseconds;
            realOffset += 256 * time.ElapsedGameTime.Milliseconds / 1000f;
            if (elapsed >= 500)
            {
                Collect();
            }
            else
            {
                while (realOffset > 1)
                {
                    Owner.UnveilMove(-1);
                    --realOffset;
                }

            }
        }
    }
}
