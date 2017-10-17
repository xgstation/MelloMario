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
            if (Owner.Items.Count == 0 && !(mario.PowerUpState is MarioObjects.PowerUpStates.Standard))
            {
                Owner.State = new Destroyed(Owner);
            }
            else
            {
                Owner.State = new Bumped(Owner);
                Owner.ReleaseNextItem();
            }
        }

        public override void Update(GameTime time)
        {
        }
    }
}
