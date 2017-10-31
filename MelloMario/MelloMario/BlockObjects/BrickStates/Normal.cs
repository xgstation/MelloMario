using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Factories;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.BrickStates
{
    class Normal : BaseState<Brick>, IBlockState
    {
        public Normal(Brick owner) : base(owner)
        {
        }

        public void Show()
        {
            //do nothing
        }

        public void Hide()
        {
            Owner.State = new Hidden(Owner);
        }

        public void Bump(Mario mario)
        {
            if (Owner.HasItem)
            {
                Owner.ReleaseNextItem();
                Owner.State = new Bumped(Owner);
            }
            else
            {
                if (mario.PowerUpState is MarioObjects.PowerUpStates.Standard)
                {
                    Owner.State = new Bumped(Owner);
                }
                else
                {
                    Owner.State = new Destroyed(Owner);
                }
            }
        }

        public override void Update(GameTime time)
        {
        }
    }
}
