namespace MelloMario.Objects.Enemies.BeetleStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class Worn : BaseState<Beetle>, IBeetleState
    {
        public Worn(Beetle owner) : base(owner)
        {
        }

        public void Show()
        {
            Owner.State = new Normal(Owner);
        }

        public void JumpOn()
        {
            Owner.State = new Defeated(Owner);
        }

        public override void Update(int time)
        {
        }

        public void Wear()
        {

        }

        public void Pushed()
        {
        }
    }
}
