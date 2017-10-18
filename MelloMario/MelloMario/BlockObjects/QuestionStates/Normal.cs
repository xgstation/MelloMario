using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Factories;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.QuestionStates
{
    class Normal : BaseState<Question>, IBlockState
    {
        public Normal(Question owner) : base(owner)
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
            if (Owner.ReleaseNextItem())
            {
                Owner.State = new Bumped(Owner);
            }
            else
            {
                Owner.State = new Used(Owner);
            }
        }

        public override void Update(GameTime time)
        {
        }
    }
}
