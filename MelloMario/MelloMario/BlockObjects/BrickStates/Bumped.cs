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
    class Bumped : BaseState<Brick>, IBlockState
    {
        private int elapsed;
        public Bumped(Brick owner) : base(owner)
        {
            elapsed = 0;
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
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
            elapsed += time.ElapsedGameTime.Milliseconds;
            if (elapsed >= 100)
            {
                Owner.Move(new Point(0, 0));
                Show();
            }
            else if (elapsed >= 50)
            {
                Owner.Move(new Point(0, 7));
            }
            else
            {
                Owner.Move(new Point(0, -7));
            }
        }
    }
}
