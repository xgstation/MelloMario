﻿using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;
using MelloMario.Theming;

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
            if (GameDataBase.HasItemEnclosed(Owner))
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
            if (elapsed >= 70)
            {
                move = 0;
            }
            elapsed += time.ElapsedGameTime.Milliseconds;
            move += 3;
            if (elapsed >= 170)
            {
                Owner.BumpMove(0);
                Owner.ReleaseNextItem();
                Show();
            }
            else if (elapsed >= 70)
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
