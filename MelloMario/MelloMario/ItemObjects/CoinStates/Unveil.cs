using MelloMario.Sounds;

namespace MelloMario.ItemObjects.CoinStates
{
    internal class Unveil : BaseTimedState<Coin>, IItemState
    {
        private float elapsed;
        private float realOffset;

        public Unveil(Coin owner) : base(owner, 250)
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
            //TODO:Move this into soundcontroller
            //SoundController.Coin.Play();
            elapsed += time;
            realOffset += 128 * time / 500f;

            while (realOffset > 1)
            {
                Owner.UnveilMove(-1);
                --realOffset;
            }

            base.Update(time);
        }

        protected override void OnTimer(int time)
        {
            Owner.Collect();
        }
    }
}
