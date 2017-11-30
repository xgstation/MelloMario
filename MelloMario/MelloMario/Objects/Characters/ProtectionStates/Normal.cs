namespace MelloMario.Objects.Characters.ProtectionStates
{
    #region

    using System;
    using MelloMario.Objects.Enemies;

    #endregion

    internal class Normal : BaseState<Mario>, IMarioProtectionState
    {
        public Normal(Mario owner) : base(owner)
        {
        }

        public void Star()
        {
            Owner.ProtectionState = new Starred(Owner);
        }

        public void Protect()
        {
            Owner.ProtectionState = new Protected(Owner);
        }

        public override void Update(int time)
        {
        }

        public void Helmet()
        {
            Owner.ProtectionState = new Helmeted(Owner);
        }
    }
}
