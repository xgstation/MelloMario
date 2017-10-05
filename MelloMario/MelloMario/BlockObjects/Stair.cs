﻿using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.BlockObjects.StairStates;

namespace MelloMario.BlockObjects
{
    class Stair : BaseGameObject
    {
        private IItemState state;

        private void OnStateChanged()
        {
            // if () ...
            // ShowResized(SpriteFactory.Instance.CreateMarioSprite("FireIdleRight", false), ResizeModeX.Center, ResizeModeY.Bottom);
        }

        protected override void OnSimulation(GameTime time)
        {
        }

        protected override void OnCollision(IGameObject target)
        {
        }

        public IItemState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                OnStateChanged();
            }
        }

        public Stair(Point location) : base(location, new Point(32, 32))
        {
            state = new StairSilent(this);
            OnStateChanged();
        }

        public void Show()
        {
            State.Show();
        }
        public void Collect()
        {
            State.Collect();
        }
    }
}