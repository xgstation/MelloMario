using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.MarioObjects;

namespace MelloMario.BlockObjects.QuestionStates
{
    class Hidden : BaseState<Question>, IBlockState
    {

        public Hidden(Question owner) : base(owner)
        {
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void Hide()
        {
            //do nothing
        }

        public void Bump(Mario mario)
        {
            Owner.State = new Bumped(Owner);
        }

        public override void Update(GameTime time)
        {
            //do nothing
        }
    }
}
