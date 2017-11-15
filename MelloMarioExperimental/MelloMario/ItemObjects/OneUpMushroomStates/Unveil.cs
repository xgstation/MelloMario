using Microsoft.Xna.Framework.Audio;
using MelloMario.Sounds;

namespace MelloMario.ItemObjects.OneUpMushroomStates
{
    class Unveil : BaseTimedState<OneUpMushroom>, IItemState
    {
        private float elapsed;
        private float realOffset;
        private SoundEffectInstance oneupMushUnveilSound;

        protected override void OnTimer(int time)
        {
            Owner.State = new Normal(Owner);
        }

        public Unveil(OneUpMushroom owner) : base(owner, 1000)
        {
            elapsed = 0f;
            oneupMushUnveilSound = SoundController.SizeUpAppear.CreateInstance();
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
            oneupMushUnveilSound.Play();
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

