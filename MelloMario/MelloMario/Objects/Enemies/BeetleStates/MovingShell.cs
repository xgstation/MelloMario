namespace MelloMario.Objects.Enemies.BeetleStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class MovingShell : BaseState<Beetle>, IBeetleState
    {
        public MovingShell(Beetle owner) : base(owner)
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

        public void Wear()
        {
            Owner.State = new Worn(Owner);
        }

        public override void Update(int time)
        {
        }

        public void Pushed()
        {
        }
    }
}
