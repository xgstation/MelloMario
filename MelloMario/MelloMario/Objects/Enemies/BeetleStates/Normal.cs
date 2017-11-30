namespace MelloMario.Objects.Enemies.BeetleStates
{
    #region

    using System;

    #endregion

    internal class Normal : BaseState<Beetle>, IBeetleState
    {
        public Normal(Beetle owner) : base(owner)
        {
        }

        public void Show()
        {
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
            //can't push normal koopa
        }
    }
}
