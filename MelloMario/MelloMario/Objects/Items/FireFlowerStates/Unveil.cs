namespace MelloMario.Objects.Items.FireFlowerStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class Unveil : BaseTimedState<FireFlower>, IItemState
    {
        private float elapsed;
        private float realOffset;

        public Unveil(FireFlower owner) : base(owner, 1000)
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
            //TODO:Move this into soundcontroller
            //SoundManager.SizeUpAppear.Play();
            base.Update(time);

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
