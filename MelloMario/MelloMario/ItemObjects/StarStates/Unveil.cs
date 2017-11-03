﻿using Microsoft.Xna.Framework;

namespace MelloMario.ItemObjects.StarStates
{
    class Unveil : BaseState<Star>, IItemState
    {
        private float elapsed;
        private float realOffset;

        public Unveil(Star owner) : base(owner)
        {
            elapsed = 0f;
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void Collect()
        {
        }

        public override void Update(GameTime time)
        {
            elapsed += time.ElapsedGameTime.Milliseconds;
            realOffset += 32 * time.ElapsedGameTime.Milliseconds / 1000f;
            if (elapsed >= 1000)
            {
                Show();
            }
            else
            {
                while (realOffset > 1)
                {
                    Owner.UnveilMove(-1);
                    --realOffset;
                }
            }
        }
    }
}
