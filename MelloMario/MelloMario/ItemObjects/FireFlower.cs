﻿using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.ItemObjects.FireFlowerStates;

namespace MelloMario.ItemObjects
{
    class FireFlower : BaseGameObject
    {
        private IItemState state;

        private void OnStateChanged()
        {
            // if () ...
            // ShowSprite(SpriteFactory.Instance.CreateMarioSprite("FireIdleRight", false), ResizeModeX.Center, ResizeModeY.Bottom);
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

        public FireFlower(Point location) : base(location, new Point(32, 32))
        {
            state = new FireFlowerNormal(this);
            //I am not sure where you wanted the sprite attached, this seems like a logical place
            ISprite sprite = SpriteFactory.Instance.CreateFlowerSprite();
            ShowSprite(sprite);
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
