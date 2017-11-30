namespace MelloMario.Objects.Characters.ProtectionStates
{
    #region

    using System;
    using MelloMario.Objects.Enemies;

    #endregion

    [Serializable]
    internal class Helmeted : BaseState<Mario>, IMarioProtectionState
    {
        public Helmeted(Mario owner) : base(owner)
        {
        }

        public void Star()
        {
            Owner.LoseHelmet();
            Owner.ProtectionState = new Starred(Owner);
        }

        public void Protect()
        {
            Owner.LoseHelmet();
            Owner.ProtectionState = new Protected(Owner);
        }

        public override void Update(int time)
        {
        }

        public void Helmet()
        {
            //do nothing
        }
    }
}
