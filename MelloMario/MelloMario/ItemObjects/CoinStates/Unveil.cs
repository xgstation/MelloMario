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
        private float realOffset;

        public Unveil(Coin owner) : base(owner)
        {
            elapsed = 0;
            move = 0;
            realOffset = 0f;
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
            //if (elapsed >= 100)
            //{
            //    move = 0;
            //}
            //elapsed += time.ElapsedGameTime.Milliseconds;
            //move += 1;

            //if (elapsed >= 300)
            //{
            //    Collect();
            //    Owner.UnveilMove(0);
            //}
            //else
            //{
            //    Owner.UnveilMove(-move);
            //}

            elapsed += time.ElapsedGameTime.Milliseconds;
            realOffset += 256 * time.ElapsedGameTime.Milliseconds / 1000f;
            if (elapsed >= 250)
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
