namespace MelloMario.Objects.Characters.ProtectionStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class Protected : BaseTimedState<Mario>, IMarioProtectionState
    {
        public Protected(Mario owner) : base(owner, 1000)
        {
        }

        public void Protect()
        {
            //refresh protection
            Owner.ProtectionState = new Protected(Owner);
        }

        public void Star()
        {
            Owner.ProtectionState = new Starred(Owner);
        }

        protected override void OnTimer(int time)
        {
            Owner.ProtectionState = new Normal(Owner);
        }
    }
}
