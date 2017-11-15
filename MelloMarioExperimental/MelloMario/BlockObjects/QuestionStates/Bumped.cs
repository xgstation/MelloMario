﻿using Microsoft.Xna.Framework.Audio;
using MelloMario.MarioObjects;
using MelloMario.Theming;
using MelloMario.Sounds;

namespace MelloMario.BlockObjects.QuestionStates
{
    class Bumped : BaseState<Question>, IBlockState
    {
        private int elapsed;
        private int move;
        private SoundEffectInstance bumpSound;

        public Bumped(Question owner) : base(owner)
        {
            elapsed = 0;
            move = 0;
            bumpSound = SoundController.BumpBlock.CreateInstance();
        }

        public void Show()
        {
            if (GameDatabase.HasItemEnclosed(Owner))
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

        public override void Update(int time)
        {
            bumpSound.Play();
            // TODO: use BaseTimedState
            if (elapsed >= 70)
            {
                move = 0;
            }
            elapsed += time;
            move += 3;
            if (elapsed >= 180)
            {
                Show();
            }
            if (elapsed >= 170)
            {
                Owner.BumpMove(0);
                Owner.ReleaseNextItem();
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