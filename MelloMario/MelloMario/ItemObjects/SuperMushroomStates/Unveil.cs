using Microsoft.Xna.Framework.Audio;
using MelloMario.Sounds;

namespace MelloMario.ItemObjects.SuperMushroomStates
{
    class Unveil : BaseTimedState<SuperMushroom>, IItemState
    {
        private float elapsed;
        private float realOffset;

        protected override void OnTimer(int time)
        {
            Owner.State = new Normal(Owner);
        }

        public Unveil(SuperMushroom owner) : base(owner, 1000)
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

        public override void Update(int time)
        {
            SoundController.sizeUpAppear.CreateInstance().Play();
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
