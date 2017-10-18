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
        private float elapsed;
        private float speedY = -7f;
        private float accelerationY = 1.5f;
        private float offset;

        public Bumped(Question owner) : base(owner)
        {
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

        public void UseUp()
        {
            Owner.State = new Used(Owner);
        }

        public override void Update(GameTime time)
        {
            // TODO: release items
            // block.State = new QuestionNormal(block);
            // block.State = new QuestionUsed(block);

            // TODO: control sprite via this.block
            elapsed += ((float)time.ElapsedGameTime.Milliseconds) / 20;
            offset = 0.5f * accelerationY * (float)Math.Pow(elapsed, 2.0f) + speedY * elapsed;

            if (offset >= 0)
            {
                UseUp();
            }
        }
    }
}
