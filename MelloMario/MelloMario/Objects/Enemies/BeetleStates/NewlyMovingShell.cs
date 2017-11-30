namespace MelloMario.Objects.Enemies.BeetleStates
{
    #region

    using System;

    #endregion

    [Serializable]
    internal class NewlyMovingShell : BaseTimedState<Beetle>, IBeetleState
    {
        public NewlyMovingShell(Beetle owner) : base(owner, 500)
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

        public void Defeat()
        {
            Owner.State = new Defeated(Owner);
        }

        protected override void OnTimer(int time)
        {
            Owner.State = new MovingShell(Owner);
        }

        public void Pushed()
        {
        }
    }
}
