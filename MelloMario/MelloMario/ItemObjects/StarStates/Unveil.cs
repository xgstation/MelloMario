using MelloMario.Sounds;

namespace MelloMario.ItemObjects.StarStates
{
    internal class Unveil : BaseTimedState<Star>, IItemState
    {
        private float elapsed;
        private float realOffset;

        public Unveil(Star owner) : base(owner, 1000)
        {
            elapsed = 0f;
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void Collect() { }

        public override void Update(int time)
        {
            base.Update(time);

            //TODO:Move this into soundcontroller
            //SoundController.SizeUpAppear.Play();

            elapsed += time;
            realOffset += 32 * time / 1000f;

            while (realOffset > 1)
            {
                Owner.UnveilMove(-1);
                --realOffset;
            }
        }

        protected override void OnTimer(int time)
        {
            Owner.State = new Normal(Owner);
        }
    }
}
