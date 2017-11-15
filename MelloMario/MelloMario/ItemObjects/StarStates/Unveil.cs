using Microsoft.Xna.Framework.Audio;
using MelloMario.Sounds;

namespace MelloMario.ItemObjects.StarStates
{
    class Unveil : BaseTimedState<Star>, IItemState
    {
        private float elapsed;
        private float realOffset;
        private SoundEffectInstance starUnveilSound;

        protected override void OnTimer(int time)
        {
            Owner.State = new Normal(Owner);
        }

        public Unveil(Star owner) : base(owner, 1000)
        {
            elapsed = 0f;
            starUnveilSound = SoundController.SizeUpAppear.CreateInstance();
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void Collect()
        {
        }

        public override void Update(int time)
        {
            starUnveilSound.Play();
            base.Update(time);

            elapsed += time;
            realOffset += 32 * time / 1000f;

            while (realOffset > 1)
            {
                Owner.UnveilMove(-1);
                --realOffset;
            }
        }
    }
}

