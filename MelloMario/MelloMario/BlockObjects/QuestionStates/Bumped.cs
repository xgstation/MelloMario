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
    class Bumped : BaseState<Question>, IBlockState
    {
        private int elapsed;
        private int move;

        public Bumped(Question owner) : base(owner)
        {
            elapsed = 0;
            move = 0;
        }

        public void Show()
        {
            if (Owner.HasItem)
            {
                Owner.State = new Normal(Owner);
            }
            else
            {
                Owner.State = new Used(Owner);
            }
        }

        public void Hide()
        {
            Owner.State = new Hidden(Owner);
        }

        public void Bump(Mario mario)
        {
            // do nothing
        }

        public override void Update(GameTime time)
        {
            if (elapsed >= 100)
            {
                move = 0;
            }
            elapsed += time.ElapsedGameTime.Milliseconds;
            move += 1;
            if (elapsed >= 350)
            {
                Owner.BumpMove(0);
                Show();
            }
            else if (elapsed >= 100)
            {
                Owner.BumpMove(move);
            }
            else
            {
                Owner.BumpMove(-move);
            }
        }
    }
}
